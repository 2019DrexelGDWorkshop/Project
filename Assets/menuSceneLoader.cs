using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuSceneLoader : MonoBehaviour
{
    public string[] scenes;
    public GameObject MM_GO;
    public GameObject LS_GO;

    public void Start()
    {
        Cursor.visible = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(scenes[3]);
        }
    }


    public void loadLevel1()
    {
        SceneManager.LoadScene(scenes[0]);
    }
    public void loadLevel2()
    {
        SceneManager.LoadScene(scenes[1]);
    }
    public void loadLevel3()
    {
        SceneManager.LoadScene(scenes[2]);
    }
    public void loadLVLSLCT()
    {
        MM_GO.SetActive(false);
        LS_GO.SetActive(true);
    }

    public void loadMM()
    {
        MM_GO.SetActive(true);
        LS_GO.SetActive(false);
    }


    public void quitGame()
    {
        Debug.Log("Quit the game successfully");
        Application.Quit();
    }
}
