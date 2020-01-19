using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_base : MonoBehaviour
{
    #region Attributes

    [Min(0), SerializeField]
    protected ushort health = 1;

    [Min(0), SerializeField]
    protected ushort maxHealth = 1;

    [SerializeField]
    protected ushort minHealth = 0;

    #endregion

    #region Getters Setters

    public ushort GetHealth()
    {
        return health;
    }

    public void SetHealth(ushort _val)
    {
        health = _val;
    }

    public void SetHealth(int _val)
    {
        health = (ushort) _val;
    }

    #endregion

    #region Monobehaviour

    private void Awake()
    {
        CustomAwake();
    }

    #endregion

    #region Protected

    #region SubtractHealth
    protected void SubtractHealth(ushort _val)
    {
        health -= _val;
        if(health < minHealth)
        {
            Die();
        }
    }

    protected void SubtractHealth(int _val)
    {
        health -= (ushort)_val;
        if(health < minHealth)
        {
            Die();
        }
    }
    #endregion

    #region AddHealth
    protected void AddHealth(ushort _val)
    {
        health += _val;
        health = (ushort) Mathf.Min(maxHealth, health + _val);
    }

    protected void AddHealth(int _val)
    {
        health += (ushort)Mathf.Min(maxHealth, health + _val);
    }
    #endregion

    #endregion

    #region Virtual
    protected virtual void CustomAwake()
    {

    }

    protected virtual void Die()
    {
        Debug.Log(gameObject + "Died");
        Destroy(this);
    }
    #endregion

    #region Abstract

    protected abstract void OnTriggerEnter(Collider _col);

    #endregion

}
