using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CameraState
{
    NULL,
    MAIN_MENU,
    THIRD_PERSON,
    SIDE_SCROLLER,
    TRANSITION
}

public class CameraManager : MonoBehaviour
{
    public CameraState cameraState
    { get;  private set; }

    [Header("Camera Game Objects")]
    public CinemachineVirtualCameraBase camera2D;
    public CinemachineVirtualCameraBase cameraTransition;
    public CinemachineVirtualCameraBase camera3D;
    public CinemachineBrain cmBrain;
    public GameObject cmBrainGO;

    [Header("Camera Properties")]
    [SerializeField] private float deadZoneMaxHeight = .4f;
    [SerializeField] private float deadZoneMinHeight = .05f;
    [SerializeField] private float defaultScreenY = .5f;
    [SerializeField] private float defaultScreenX = .5f;
    public LayerMask originalCullingMask;



    [HideInInspector]
    private Dictionary<CinemachineVirtualCameraBase, int> originalCamPriorities;
    private CharacterMovement characterMovement;
    public static CameraManager Instance;
    public bool isTransitioning
    { get; private set; }

    #region Events
    public delegate void SwitchPerspectiveHandler(bool _is2D);
    public event SwitchPerspectiveHandler onPerspectiveSwitch;

    public delegate void CameraTransitioningHandler(bool _going2D);
    public event CameraTransitioningHandler onCameraTransition;
    #endregion


    #region Monobehaviour

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        cameraState = CameraState.THIRD_PERSON;
        originalCamPriorities = new Dictionary<CinemachineVirtualCameraBase, int>();
    }

    private void Update()
    {
        if(cameraState != CameraState.TRANSITION)
        {
            if(cameraTransition != null)
            {
                if ((Object)cmBrain.ActiveVirtualCamera == cameraTransition)
                {
                    cameraTransition.Priority = 5;
                }
            }
        }
        if(PauseManager.Instance.Paused)
        {
            ((CinemachineFreeLook)camera3D).m_XAxis.m_InputAxisValue = 0;
            ((CinemachineFreeLook)camera3D).m_YAxis.m_InputAxisValue = 0;
        }
    }

    #endregion

    private void OnPauseHandler()
    {

    }

    private void OnResumeHandler()
    {

    }

    public void Toggle3D2D()
    {
        switch(cameraState)
        {
            case CameraState.THIRD_PERSON:
                StartCoroutine(SwitchTo2D());
                onCameraTransition?.Invoke(true);
                break;
            case CameraState.SIDE_SCROLLER:
                StartCoroutine(SwitchTo3D());
                onCameraTransition?.Invoke(false);
                break;
            default:
                Debug.Log(cameraState + " is unsupported camera transition.");
                break;
        }
        return;
    }

    private IEnumerator SwitchTo2D()
    {
        isTransitioning = true;
        SetCamHighestPriority(cameraTransition);
        cameraState = CameraState.TRANSITION;
        yield return new WaitForEndOfFrame();

        while(cmBrain.ActiveBlend != null && !cmBrain.ActiveBlend.IsComplete)
        {
            yield return null;
        }

        SetCamHighestPriority(camera2D);
        yield return new WaitForEndOfFrame();

        while(cmBrain.ActiveBlend != null && !cmBrain.ActiveBlend.IsComplete)
        {
            yield return null;
        }
        cameraState = CameraState.SIDE_SCROLLER;
        isTransitioning = false;
        onPerspectiveSwitch?.Invoke(true);
    }

    private IEnumerator SwitchTo3D()
    {
        isTransitioning = true;
        SetCamHighestPriority(cameraTransition);
        cameraState = CameraState.TRANSITION;
        yield return new WaitForEndOfFrame();

        while (cmBrain.ActiveBlend != null && !cmBrain.ActiveBlend.IsComplete)
        {
            yield return null;
        }


        yield return new WaitForEndOfFrame();
        SetCamHighestPriority(camera3D);

        while (cmBrain.ActiveBlend != null && !cmBrain.ActiveBlend.IsComplete)
        {
            yield return null;
        }
        cameraState = CameraState.THIRD_PERSON;
        isTransitioning = false;
        onPerspectiveSwitch?.Invoke(false);
    }

    public void SetCamHighestPriority(CinemachineVirtualCameraBase camera)
    {
        if (camera == null)
        {
            Debug.LogError("camera Null");
            return;
        }

        if (cmBrain == null)
        {
            Debug.LogError("camera brain Null");
            return;
        }
        if (cmBrain.ActiveVirtualCamera == null)
        {
            Debug.LogError("camera brain active virtual camera Null");
            return;
        }

        if((Object)cmBrain.ActiveVirtualCamera == camera)
        {
            return;
        }

        originalCamPriorities[camera] = camera.Priority;
        int currPriority = cmBrain.ActiveVirtualCamera.Priority;
        int oldPriority = originalCamPriorities[(CinemachineVirtualCameraBase)cmBrain.ActiveVirtualCamera];

        cmBrain.ActiveVirtualCamera.Priority = oldPriority;
        camera.Priority = currPriority + 1;

    }

    public void StoreCameraPriority(CinemachineVirtualCameraBase _cam)
    {
        originalCamPriorities[_cam] = _cam.Priority;
    }

    public void SetCharacterMovement(CharacterMovement _char)
    {

        if(_char != null)
        {
            _char.OnGrounded += OnGroundedChangedHandler;
        }
        else
        {
            if(characterMovement != null)
            {
                characterMovement.OnGrounded -= OnGroundedChangedHandler;
            }
        }

        characterMovement = _char;
    }

    /// <summary>
    /// Shift will always work off of the default. A value of 0 will reset to normal shift ammounts.
    /// </summary>
    /// <param name="_shiftAmount">Shift ammount</param>
    public void ScreenShiftY(float _shiftAmount = 0f)
    {
        CinemachineFramingTransposer transposer = ((CinemachineVirtualCamera)camera2D).GetCinemachineComponent<CinemachineFramingTransposer>();

        float shiftAmount = Mathf.Clamp01(defaultScreenY + _shiftAmount);

        transposer.m_ScreenY = shiftAmount;

    }

    public void ScreenShiftX(float _shiftAmount = 0f)
    {
        CinemachineFramingTransposer transposer = ((CinemachineVirtualCamera)camera2D).GetCinemachineComponent<CinemachineFramingTransposer>();

        float shiftAmount = Mathf.Clamp01(defaultScreenX + _shiftAmount);

        transposer.m_ScreenX = shiftAmount;
    }

    private void OnGroundedChangedHandler(bool _isGrounded)
    {
        CinemachineFramingTransposer transposer = ((CinemachineVirtualCamera)camera2D).GetCinemachineComponent<CinemachineFramingTransposer>();

        if (!_isGrounded)
        {
            transposer.m_DeadZoneHeight = deadZoneMaxHeight;
        }
        else
        {
            transposer.m_DeadZoneHeight = deadZoneMinHeight;
        }
    }

    #region UpdateCameraReferences

    public void UpdateCameraReferences(GameObject _2dCam, GameObject _transitionCam, GameObject _3dCam, GameObject _cmBrain)
    {
        camera2D = _2dCam.GetComponent< CinemachineVirtualCameraBase>();
        camera3D = _3dCam.GetComponent<CinemachineVirtualCameraBase>();
        cameraTransition = _transitionCam.GetComponent<CinemachineVirtualCameraBase>();
        cmBrain = _cmBrain.GetComponent<CinemachineBrain>();
        cmBrainGO = _cmBrain;
        originalCullingMask = cmBrainGO.GetComponent<Camera>().cullingMask;

        StoreCameraPriority(camera2D);
        StoreCameraPriority(cameraTransition);
        StoreCameraPriority(camera3D);

        return;
    }

    public void UpdateCameraReferences(CinemachineVirtualCameraBase _2dCam, CinemachineVirtualCameraBase _transitionCam, CinemachineVirtualCameraBase _3dCam, CinemachineBrain _cmBrain)
    {
        camera2D = _2dCam;
        camera3D = _3dCam;
        cameraTransition = _transitionCam;
        cmBrain = _cmBrain;
        cmBrainGO = _cmBrain.gameObject;
        originalCullingMask = cmBrainGO.GetComponent<Camera>().cullingMask;

        StoreCameraPriority(camera2D);
        StoreCameraPriority(cameraTransition);
        StoreCameraPriority(camera3D);

        return;
    }
    
    #endregion
}
