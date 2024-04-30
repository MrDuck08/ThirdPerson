using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState
{
    unarmed = 0,
    HitScan = 1,
    Projectile = 2,
    Melee = 3,
    Total
}

public class WeaponHandler : MonoBehaviour
{
    #region Melee

    [SerializeField] GameObject[] meleeWeapon;

    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Melee()
    {

    }
}
