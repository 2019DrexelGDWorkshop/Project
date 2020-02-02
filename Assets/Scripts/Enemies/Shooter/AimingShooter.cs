using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingShooter : Shooter_base
{
    private Transform player;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindObjectOfType<PlayerInput>().transform;
    }
        
    protected override void Aim(Vector3 _pos)
    {
        this.transform.LookAt(player);
        Shoot();
    }
}
