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

    public CinemachineVirtualCameraBase camera2D;
    public CinemachineVirtualCameraBase cameraTransition;
    public CinemachineVirtualCameraBase camera3D;
    public CinemachineBrain cmBrain;

    public static CameraManager Instance;

    private Dictionary<CinemachineVirtualCameraBase, int> originalCamPriorities;

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

    #endregion

    public void Toggle3D2D()
    {
        switch(cameraState)
        {
            case CameraState.THIRD_PERSON:
                SetCamHighestPriority(camera2D);
                cameraState = CameraState.SIDE_SCROLLER;
                break;
            case CameraState.SIDE_SCROLLER:
                SetCamHighestPriority(camera3D);
                cameraState = CameraState.THIRD_PERSON;
                break;
            default:
                Debug.Log(cameraState + " is unsupported camera transition.");
                break;
        }
        return;
    }

    public void SetCamHighestPriority(CinemachineVirtualCameraBase camera)
    {
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

    #region UpdateCameraReferences
    public void UpdateCameraReferences(GameObject _2dCam, GameObject _transitionCam, GameObject _3dCam, GameObject _cmBrain)
    {
        camera2D = _2dCam.GetComponent< CinemachineVirtualCameraBase>();
        camera3D = _3dCam.GetComponent<CinemachineVirtualCameraBase>();
        cameraTransition = _transitionCam.GetComponent<CinemachineVirtualCameraBase>();
        cmBrain = _cmBrain.GetComponent<CinemachineBrain>();

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

        StoreCameraPriority(camera2D);
        StoreCameraPriority(cameraTransition);
        StoreCameraPriority(camera3D);

        return;
    }
    
    #endregion
}
