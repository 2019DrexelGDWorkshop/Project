using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;

    public int cameraState = 0;

    public Camera Camera2D;
    public Camera Camera3D;
    public static GameManager Instance;
    public Transform lastCheckPoint;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCameraState();
    }

    void ChangeCameraState()
    {
        if (Input.GetMouseButtonDown(1))
            cameraState = (cameraState + 1) % 2;
        if (cameraState == 0)
        {
            Camera2D.gameObject.SetActive(false);
            Camera3D.gameObject.SetActive(true);
        }
        else
        {
            Camera2D.gameObject.SetActive(true);
            Camera3D.gameObject.SetActive(false);
        }
    }
       
}
