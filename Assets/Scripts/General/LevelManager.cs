using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Attributes

    public GameObject camera2D;
    public GameObject cameraTransition;
    public GameObject camera3D;
    public Transform lastCheckPoint;

    public static LevelManager Instance;

    #endregion

    #region Monobehaviour

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        if(lastCheckPoint == null)
        {
            lastCheckPoint = GameObject.FindObjectOfType<CheckPoint>().transform;
        }
    }

    private void Start()
    {
        Cinemachine.CinemachineBrain _cmBrain = GameObject.FindObjectOfType<Cinemachine.CinemachineBrain>();
        CameraManager.Instance.UpdateCameraReferences(camera2D, cameraTransition, camera3D, _cmBrain.gameObject);
    }

    #endregion
}
