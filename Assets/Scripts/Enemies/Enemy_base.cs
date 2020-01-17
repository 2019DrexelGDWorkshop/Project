using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_base : MonoBehaviour
{
    #region Attributes

    [Min(0), SerializeField]
    protected ushort health;

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

    #region Virtual
    protected virtual void Die()
    {
        Debug.Log(gameObject + "Died");
        Destroy(this);
    }
    #endregion
}
