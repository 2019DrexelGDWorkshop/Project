using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherIn3D : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    void Start()
    {
        CameraManager.Instance.onPerspectiveSwitch += PerspectiveChangeHandler;
    }

    void OnDestroy()
    {
        CameraManager.Instance.onPerspectiveSwitch -= PerspectiveChangeHandler;
    }

    void OnDisable()
    {
        CameraManager.Instance.onPerspectiveSwitch -= PerspectiveChangeHandler;
    }

    private void PerspectiveChangeHandler(bool _is2D)
    {
        if(!_is2D)
        {
            obj.SetActive(false);
        }
        else
        {
            obj.SetActive(true);
        }
    }
}
