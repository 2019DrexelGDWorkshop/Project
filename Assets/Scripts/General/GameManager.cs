using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Level
{
    INIT = 0,
    MAINMENU = 1,
    DREAM1 = 2,
    DREAM2 = 3,
    DREAM3 = 4,
    DREAM4 = 5
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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

    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }




}
