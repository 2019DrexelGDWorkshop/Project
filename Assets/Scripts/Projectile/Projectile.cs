using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    #region Attributes

    [SerializeField, Tooltip("Layers that should be hit by the bullets")]
    private LayerMask hitLayers;

    [SerializeField, Tooltip("Effect for when the bullt hits an object and explodes")]
    private ParticleSystem blastEffect;

    private ushort killDistance = 0;

    private float killTime = 0;

    private Vector3 originalPosition;

    #endregion

    #region Monobehaviour

    private void Awake()
    {
        originalPosition = transform.position;
    }

    #endregion

    #region Public

    public void SetKillDistance(ushort _val)
    {
        killDistance = _val;

        StartCoroutine(CheckKillDistance());
    }

    public void SetKillTime(float _val)
    {
        killTime = _val;

        StartCoroutine(CheckKillTime());
    }

    #endregion

    #region Private

    private void OnCollisionEnter(Collision collision)
    {
        Destroy();
    }

    private IEnumerator CheckKillDistance()
    {
        while(Vector3.Distance(originalPosition, transform.position) < killDistance)
        {
            yield return null;
        }
        Destroy();
    }

    private IEnumerator CheckKillTime()
    {
        float startTime = 0;
        while(startTime < killTime)
        {
            startTime += Time.deltaTime;
            yield return null;
        }
        Destroy();
    }

    private void Destroy()
    {
        Debug.Log("Bullet Destroyed.");
        StopAllCoroutines();
        Instantiate(blastEffect, this.transform.position, Quaternion.identity, null);
        Destroy(this.gameObject);
    }

    #endregion
}
