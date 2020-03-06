using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CopyCamera : MonoBehaviour
{
    [SerializeField] private Transform followTransform;
    [SerializeField] private LayerMask myCullingMask;

    private Camera myCam;
    private LayerMask originalCullingMask;

    private void Start()
    {
        myCam = GetComponent<Camera>();
        CameraManager.Instance.onPerspectiveSwitch += PerspectiveSwitchHandler;
        CameraManager.Instance.onCameraTransition += CameraTransitionHandler;
    }

    private void OnDisable()
    {
        CameraManager.Instance.onPerspectiveSwitch -= PerspectiveSwitchHandler;
        CameraManager.Instance.onCameraTransition -= CameraTransitionHandler;
    }

    private void OnDestroy()
    {
        CameraManager.Instance.onPerspectiveSwitch -= PerspectiveSwitchHandler;
        CameraManager.Instance.onCameraTransition -= CameraTransitionHandler;
    }

    void Update()
    {
        this.transform.position = followTransform.position;
        this.transform.rotation = followTransform.rotation;
    }

    private void PerspectiveSwitchHandler(bool _is2D)
    {
        if(_is2D)
         {
             StartCoroutine(waitForCameraBlend(_is2D));
         }
         else
         {
             PerspectiveSwitch(_is2D);
         }
    }

    private void CameraTransitionHandler(bool _going2D)
    {
        if(!_going2D)
        {
            PerspectiveSwitch(false);
        }
    }

    private IEnumerator waitForCameraBlend(bool _is2D)
    {
        yield return new WaitForEndOfFrame();


        while (CameraManager.Instance.cmBrain.ActiveBlend != null && !CameraManager.Instance.cmBrain.ActiveBlend.IsComplete)
        {
            yield return null;
        }

        PerspectiveSwitch(_is2D);
    }

    private void PerspectiveSwitch(bool _is2D)
    {
        Camera cmBrainCam = CameraManager.Instance.cmBrainGO.GetComponent<Camera>();

        if (_is2D)
        {
            myCam.enabled = true;
            originalCullingMask = cmBrainCam.cullingMask;
            cmBrainCam.cullingMask = ~myCullingMask;
            myCam.cullingMask = myCullingMask;
        }
        else
        {
            myCam.enabled = false;
            cmBrainCam.cullingMask = CameraManager.Instance.originalCullingMask;

        }
    }
}
