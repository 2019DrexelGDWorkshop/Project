using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Level
{
    INIT = 0,
    MAINMENU = 1,
    SCIENCE1 = 6,
    GYM1 = 11,
    MATH1 = 15
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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

    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnLevelWasLoadedHandler;
        LockCursor(true);
    }

    private void LockCursor(bool _shouldLock)
    {
        Cursor.visible = !_shouldLock;
#if !UNITY_EDITOR && _shouldLock
        Cursor.lockState = CursorLockMode.Locked;
#else
        Cursor.lockState = CursorLockMode.None;
#endif

    }

    private void OnLevelWasLoadedHandler(Scene _scene, LoadSceneMode _loadSceneMode)
    {
        if(_scene.buildIndex == (int)Level.MAINMENU)
        {
            LockCursor(false);
        }
        else
        {
            LockCursor(true);
        }
    }




}
