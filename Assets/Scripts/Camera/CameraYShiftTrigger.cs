using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraYShiftTrigger : MonoBehaviour
{
    [SerializeField] private float shiftAmout = -.2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != LevelManager.Instance.player)
            return;

        CameraManager.Instance.ScreenShift(shiftAmout);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != LevelManager.Instance.player)
            return;

        CameraManager.Instance.ScreenShift();
    }
}
