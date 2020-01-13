using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class GameManager : MonoBehaviour
{
    #region Attributes
    public int cameraState = 0;

    public GameObject Camera2D;
    public GameObject CameraTrans;
    public GameObject Camera3D;
    public static GameManager Instance;
    public Transform lastCheckPoint;

    public static int cameraState3D = 0;
    public static int cameraState2D = 1;

    public Player rewiredPlayer;
    [Tooltip("Rewired Player ID of the character")]
    public int playerId = 0;
    #endregion

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
        rewiredPlayer = ReInput.players.GetPlayer(playerId);
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCameraState()
    {
        cameraState = (cameraState + 1) % 2;
        UpdateCameraState();
    }

    public float transTime = 2.0f;

    void UpdateCameraState()
    {
        if (cameraState == 0)   // 3D
        {
            Camera2D.gameObject.SetActive(false);
            Invoke("CameraTransSetFalse", transTime);
            //Camera3D.gameObject.SetActive(true);
        }
        else    // 2D
        {
            CameraTrans.gameObject.SetActive(true);
            Invoke("Camera2DSetTrue", transTime);
            //Camera3D.gameObject.SetActive(false);
        }
    }

    public void Camera2DSetTrue()
    {
        Camera2D.gameObject.SetActive(true);
    }

    public void CameraTransSetFalse()
    {
        CameraTrans.gameObject.SetActive(false);
    }




}
