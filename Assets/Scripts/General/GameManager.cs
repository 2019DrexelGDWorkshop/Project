using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Level
{
    INIT = 0,
    MAINMENU = 1,
    TUTORIALDREAM = 2,
    SCIENCE1 = 3,
    GYM1 = 4,
    MATH1 = 5
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
        bool lockCursor = true;

        if(SceneManager.sceneCount > 1)
        {
            if(SceneManager.GetActiveScene().buildIndex == (int)Level.MAINMENU)
            {
                lockCursor = false;
            }
        }

        LockCursor(lockCursor);
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
        if(_scene.buildIndex == (int)Level.INIT)
        {
            return;
        }

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
