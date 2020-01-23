using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuSceneLoader : MonoBehaviour
{
    public string[] scenes;
    public GameObject MM_GO;
    public GameObject LS_GO;
    public GameObject loadingScreen;
    public GameObject creditsScreen;
    public Slider slider;

    public void Start()
    {
        Cursor.visible = true;
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    SceneManager.LoadScene(scenes[3]);
        //}
    }

    //
    //
    //
    public void LoadLevel(int sceneIndex)
    {
        MM_GO.SetActive(false);
        LS_GO.SetActive(false);
        StartCoroutine(LoadAsynchronously(sceneIndex));
        
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            yield return null;
        }

    }
    //
    //
    //

    public void loadLVLSLCT()
    {
        MM_GO.SetActive(false);
        LS_GO.SetActive(true);
    }

    public void loadMM()
    {
        MM_GO.SetActive(true);
        LS_GO.SetActive(false);
        creditsScreen.SetActive(false);
    }
    public void loadCredits()
    {
        MM_GO.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void quitGame()
    {
        Debug.Log("Quit the game successfully");
        Application.Quit();
    }
}
