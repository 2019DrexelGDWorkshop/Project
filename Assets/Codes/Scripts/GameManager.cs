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

    public static int cameraState3D = 0;
    public static int cameraState2D = 1;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = this;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraState();
    }

    public void ChangeCameraState()
    {
        cameraState = (cameraState + 1) % 2;
    }

    void UpdateCameraState()
    {
        if (cameraState == 0)   // 3D
        {
            Camera2D.gameObject.SetActive(false);
            Camera3D.gameObject.SetActive(true);
        }
        else    // 2D
        {
            Camera2D.gameObject.SetActive(true);
            Camera3D.gameObject.SetActive(false);
        }
    }
       
}
