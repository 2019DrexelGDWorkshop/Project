using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialLoadMain : MonoBehaviour
{
    [SerializeField] private Level levelToLoad;
    public void Start()
    {
        if (SceneManager.sceneCount == 1)
        {
            SceneManager.LoadScene((int)levelToLoad);
        }
        Destroy(this); 
    }
}
