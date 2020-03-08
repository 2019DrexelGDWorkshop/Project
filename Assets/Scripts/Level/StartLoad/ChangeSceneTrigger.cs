using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneTrigger : MonoBehaviour
{

    public Level levelToLoad;
    public GameObject loadingScreen;
    public Slider slider;
	
	private bool returnTo3D = true;

    private void OnTriggerEnter(Collider other)
    {
        LoadLevel((int)levelToLoad);
        if (!returnTo3D && CameraManager.Instance.cameraState == CameraState.THIRD_PERSON)
        {
            CameraManager.Instance.Toggle3D2D();
		}
        else if (returnTo3D && CameraManager.Instance.cameraState == CameraState.SIDE_SCROLLER)
        {
			CameraManager.Instance.Toggle3D2D();
		}
        
    }

    //
    public void LoadLevel(int sceneIndex)
    {
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
}
