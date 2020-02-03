using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol_basic : Enemy_base
{

    protected override void OnTriggerEnter(Collider _col)
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collisionWith " + collision.collider.transform.name);
        if (collision.gameObject == LevelManager.Instance.player.gameObject)
        {
            LevelManager.Instance.player.GetComponent<CharacterMovement>().Kill();
            this.transform.parent.GetComponent<PatrolSystem>().Reset();
            //Destroy(this.gameObject, 5);
        }
    }
}

