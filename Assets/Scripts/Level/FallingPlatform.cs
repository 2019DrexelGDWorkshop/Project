using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField, Tooltip("Seconds before platform falls")]
    public float fallDelay = 2.0f;

    [Tooltip("Seconds before platform respawns after falling")]
    public float respawnDelay = 5.0f;

    private Vector3 origPosition;
    [SerializeField]
    private bool falling;
    public PlatformShake platShake;
    public bool stillFallAfterJumpingOff;
    private bool canFall;

    private void Start()
    {
        canFall = true;
        origPosition = gameObject.transform.position;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && !falling && canFall)
        {
            StartCoroutine("FallAfterDelay");
            
        }

    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && !stillFallAfterJumpingOff && canFall)
        {
            platShake.StopShaking();
            canFall = false;
            falling = false;
            
        }

    }

    IEnumerator FallAfterDelay()
    {
        Debug.Log("fallafter");
        falling = false;
        platShake.Shake();
        yield return new WaitForSeconds(fallDelay);
        if (canFall && !falling)
        {
            Fall();
        }
        else
        {
            canFall = true;
            yield return null;
        }

    }

    void Fall()
    {
        falling = true;
        canFall = false;
        Debug.Log("Fall");
        platShake.StopShaking();
        GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine("RespawnAfterDelay");
    }
    IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        falling = false;
        canFall = true;
        gameObject.transform.position = origPosition;
        GetComponent<Rigidbody>().isKinematic = true;

    }
}
