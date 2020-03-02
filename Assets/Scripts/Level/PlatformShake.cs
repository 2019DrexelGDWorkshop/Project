using UnityEngine;
using System.Collections;

public class PlatformShake : MonoBehaviour
{
    [Range(0f,.05f)]
    public float shakeStrength;

    private float shakeIntensity;

    public bool shaking;
    Vector3 origPosition;
    Quaternion origRotation;
    Transform _transform;


    void OnEnable()
    {
        _transform = transform;
    }

    IEnumerator ShakeIt()
    {
        while (shaking)
        {
            _transform.localPosition = origPosition + Random.insideUnitSphere * shakeIntensity;
            /*_transform.localRotation = new Quaternion(
                origRotation.x + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
                origRotation.y + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
                origRotation.z + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
                origRotation.w + Random.Range(-shakeIntensity, shakeIntensity) * .2f);*/
            yield return null;
        }

        StopShaking();

        yield return null;
    }

    public void StopShaking()
    {
        shaking = false;
        _transform.localPosition = origPosition;
        _transform.localRotation = origRotation;
    }

    public void Shake()
    {
        shaking = true;
        origPosition = _transform.localPosition;
        origRotation = _transform.localRotation;

        shakeIntensity = shakeStrength;
        StartCoroutine("ShakeIt");
    }
}