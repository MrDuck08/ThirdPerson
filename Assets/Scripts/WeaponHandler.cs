using System.Collections;
using System.Collections.Generic;
using TMPro;
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


    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI whatWeaponText;

    float mouseAxisBreakpoin = 1.0f;
    float ScollWhellDelta = 0.0f;

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
