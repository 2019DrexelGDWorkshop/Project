using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingShooter : Shooter_base
{
    private Transform player;

    protected override void CustomAwake()
    {
        base.CustomAwake();
        player = GameObject.FindObjectOfType<PlayerInput>().transform;
    }
    protected override void Aim(Vector3 _pos)
    {
        this.transform.LookAt(player);
        Shoot();
    }
}
