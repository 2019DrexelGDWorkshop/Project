using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Shooter_base : Enemy_base
{
    protected enum ShooterState
    {
        NULL,
        WAITING,
        AIMING,
        FIRING,
        RECOVERING
    }

    #region Attributes

    [Min(0), SerializeField, Tooltip("Minimum time between shots.")]
    protected float fireRate = 1.5f;

    [Min(5), SerializeField, Tooltip("How far can this enemy shoot?")]
    protected ushort bulletDistance = 15;

    [Min(5), SerializeField, Tooltip("From how far away can this enemy see the player?")]
    protected ushort detectionRadius = 15;

    [Min(0), SerializeField, Tooltip("Speed at which the bullet travels through the air.")]
    protected float bulletSpeed = 6f;

    [Min(0), SerializeField, Tooltip("Time it takes the enemy to start aiming again after shooting.")]
    protected float recoverTime = .5f;

    [SerializeField, Tooltip("Transform that is at the end of the gun barrel. Spawn location for bullets.")]
    protected Transform bulletSpawnLocation;

    [SerializeField, Tooltip("Is the player in range right now? Is this enemy engaging the Player?")]
    protected bool playerInRange = false;

    [SerializeField, Tooltip("The state that the AI is in currently.")]
    protected ShooterState currState = ShooterState.WAITING;

    [SerializeField]
    private GameObject player;

    [SerializeField, Tooltip("Prefab for bullet shot goes here")]
    private GameObject bulletPrefab;

    private Coroutine engagePlayer;

    #endregion

    #region Private

    protected override void CustomAwake()
    {
        currState = ShooterState.WAITING;
    }

    protected virtual void Start()
    {
        player = LevelManager.Instance.player;
    }

    private void GenericSpawnBullet(Vector3 _travelDir, float _speed)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnLocation.position, Quaternion.identity, this.transform);
        Rigidbody bulletBody =  bullet.GetComponent<Rigidbody>();
        bulletBody.velocity = _travelDir * _speed;
        Projectile bulletProjectile = bullet.GetComponent<Projectile>();
        bulletProjectile.SetKillDistance(bulletDistance);
    }

    

    private IEnumerator EngagePlayer()
    {
        while(true)
        {
            switch (currState)
            {
                case ShooterState.AIMING:
                    Aim(player.transform.position);
                    break;
                case ShooterState.FIRING:
                    currState = ShooterState.RECOVERING;
                    break;
                case ShooterState.RECOVERING:
                    {
                        float startTime = 0;
                        while(startTime < recoverTime)
                        {
                            startTime += Time.deltaTime;
                            yield return null;
                        }
                        currState = ShooterState.AIMING;
                        break;
                    }
                default:
                    Debug.LogError(currState + " is not a supported state.");
                    break;
            }

            yield return null;
        }
    }

    protected override void OnTriggerEnter(Collider _col)
    { 
        if(player == null)
        {
            if (_col.gameObject.tag != "Player")
            {
                return;
            }
            player = _col.gameObject;
        }
        else if(_col.gameObject != player)
        {
            return;
        }


        playerInRange = true;
        currState = ShooterState.AIMING;
        engagePlayer = StartCoroutine(EngagePlayer());
    }

    private void OnTriggerExit(Collider _col)
    {
        if (_col.gameObject.tag != "Player")
        {
            return;
        }

        playerInRange = false;
        currState = ShooterState.WAITING;
        StopCoroutine(engagePlayer);
    }

    #endregion

    #region Protected


    protected void SpawnBullet(Vector3 _travelDir)
    {
        GenericSpawnBullet(_travelDir, bulletSpeed);
    }

    protected void SpawnBullet(Vector3 _travelDir, float _speed)
    {
        GenericSpawnBullet(_travelDir, _speed);
    }

    #endregion

    #region Virtual

    protected virtual void Aim(Vector3 _pos)
    {
        Shoot();
    }

    protected virtual void Shoot()
    {
        currState = ShooterState.FIRING;
        SpawnBullet(bulletSpawnLocation.forward);
    }

    #endregion

}
