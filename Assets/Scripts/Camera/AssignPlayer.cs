using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCameraBase))]
public class AssignPlayer : MonoBehaviour
{
    [SerializeField]
    private bool shouldFollow = true;

    [SerializeField]
    private bool shouldLookAt = true;

    private Transform playerTransform;

    private CinemachineVirtualCameraBase camBase = null;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindObjectOfType<PlayerInput>().gameObject.transform; //Replace this with singleton reference

        camBase = this.GetComponent<CinemachineVirtualCameraBase>();

        if(shouldFollow)
        {
            camBase.Follow = playerTransform;
        }

        if(shouldLookAt)
        {
            camBase.LookAt = playerTransform;
        }
    }
}
