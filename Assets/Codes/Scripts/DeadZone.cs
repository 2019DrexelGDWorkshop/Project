using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour
{
    public Transform startPos;
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.position = GameManager.Instance.lastCheckPoint.position;
    } 
    
}
