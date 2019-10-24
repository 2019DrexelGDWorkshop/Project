using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int secTime = 0;

    public int minTime = 0;

    private float timerLength = 0;

    private float timeRemaining = 0;

    public Timer(float setLength)
    {
        timerLength = setLength;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = timerLength;
        StartCoroutine(timeTick());
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    IEnumerator timeTick()
    {
        while( timeRemaining > 0)
        {
            yield return new WaitForSeconds(1.0f);
            print(timeRemaining);
            timeRemaining--;
        }
    }

    void resetTimer()
    {
        timeRemaining = timerLength;
    }

    public bool timeExpired()
    {
        if (timeRemaining == 0)
            return true;
        else
            return false;
    }
}
