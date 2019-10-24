using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWallDelay : MonoBehaviour
{
    public float timerLength = 0.0f;

    private Timer wallDelay = null;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Animator>().enabled = false;
        wallDelay = new Timer(timerLength);
    }

    // Update is called once per frame
    void Update()
    {
        if (wallDelay.timeExpired())
        {
            this.gameObject.GetComponent<Animator>().enabled = true;
        }
    }
}
