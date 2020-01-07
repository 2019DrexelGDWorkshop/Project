using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnim : MonoBehaviour
{
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        _animator.SetBool("Come", true);
    }
}
