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

    private void Start()
    {
        origPosition = gameObject.transform.position;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine("FallAfterDelay");
        }

    }

    IEnumerator FallAfterDelay()
    {

        yield return new WaitForSeconds(fallDelay);
        GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine("RespawnAfterDelay");



    }

    IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        gameObject.transform.position = origPosition;
        GetComponent<Rigidbody>().isKinematic = true;

    }
}
