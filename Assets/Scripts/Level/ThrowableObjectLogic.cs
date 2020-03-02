using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObjectLogic : MonoBehaviour
{
    private Rigidbody m_rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        holder = null;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMotion();
    }

    private GameObject holder = null;

    public void pickUp(GameObject player)
    {
        m_rigidbody.isKinematic = true;
        holder = player;
        transform.SetParent(holder.transform);
    }

    public void throwOut()
    {
        m_rigidbody.isKinematic = false;
        holder = null;
        transform.SetParent(null);
        Vector3 tmpForce = transform.forward * throwForce.z + transform.up * throwForce.y;
        m_rigidbody.AddForce(tmpForce, ForceMode.Impulse);
    }

    public Vector3 HandlePos = new Vector3(0.0f, 0.0f, 2.0f);
    public Vector3 throwForce = new Vector3(0.0f, 7.0f, 5.0f);

    private void UpdateMotion()
    {
        if (holder == null)
            return;
        transform.localPosition = HandlePos;
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }
}
