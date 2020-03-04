using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShiftTrigger : MonoBehaviour
{
    [SerializeField] private float shiftAmoutY = -.2f;
    [SerializeField] private float shiftAmoutX = -.2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != LevelManager.Instance.player)
            return;

        CameraManager.Instance.ScreenShiftY(shiftAmoutY);
        CameraManager.Instance.ScreenShiftX(shiftAmoutX);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != LevelManager.Instance.player)
            return;

        CameraManager.Instance.ScreenShiftY();
        CameraManager.Instance.ScreenShiftX();
    }
}
