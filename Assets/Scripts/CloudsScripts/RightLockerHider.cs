using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLockerHider : MonoBehaviour
{
    public GameObject[] selfLocker;
    void Update()
    {
        if (CameraManager.Instance.cameraState == CameraState.SIDE_SCROLLER)
        {
            foreach (GameObject j in selfLocker)
            {
                j.SetActive(false);
            }
            
        }
        else
        {
            foreach (GameObject j in selfLocker)
            {
                j.SetActive(true);
            }
        }
    }
    //IEnumerator ExampleCoroutine()
    //{
        //yield return new WaitForSeconds(.5f);
        //selfLocker.SetActive(true);
    //}

    //IEnumerator WaitThenOff()
    //{
    //    yield return new WaitForSeconds(.5f);
    //    selfLocker.SetActive(false);
    //}
}