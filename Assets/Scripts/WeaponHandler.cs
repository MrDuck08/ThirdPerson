using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum WeaponState
{
    Melee = 0,
    Projectile = 1,
    Hitscan = 2,
    Total
}

public class WeaponHandler : MonoBehaviour
{
    public PlayerMovment myPlayerMovment = null;

    [SerializeField] GameObject[] meleeWeapon;

    public Weapon[] avilableWeapons = new Weapon[(int)WeaponState.Total];
    public Weapon currentWeapon = null;


    float mouseAxisBreakpoin = 1.0f;
    float ScollWhellDelta = 0.0f;

    private void Update()
    {
        HandleWeaponSwap();

        foreach (Weapon weapon in avilableWeapons)
        {
            weapon.gameObject.SetActive(false);
        }

        currentWeapon.gameObject.SetActive(true);

        if (Input.GetMouseButtonDown(0) && currentWeapon != null)
        {
            currentWeapon.Fire();
        }

        //Debug.Log(currentWeapon.ammunition.ToString() + " Ammo");

        WhatWeaponToShow();
    }

    void WhatWeaponToShow()
    {
        int CurrenWeaponIndex = (int)currentWeapon.weaponType;
        if (CurrenWeaponIndex == (int)WeaponState.Projectile)
        {
            //Debug.Log("Projectile");
        }
        if (CurrenWeaponIndex == (int)WeaponState.Melee)
        {
            //Debug.Log("Melee");
        }
        if (CurrenWeaponIndex == (int)WeaponState.Hitscan)
        {
            //Debug.Log("HitScan");
        }
    }

    public void Start()
    {
        int currentWeaponIndex = (int)currentWeapon.weaponType;
        WeaponSwapAnimation(currentWeaponIndex);
    }
    private void HandleWeaponSwap()
    {

        ScollWhellDelta += Input.mouseScrollDelta.y;
        if (Mathf.Abs(ScollWhellDelta) > mouseAxisBreakpoin)
        {
            int SwapDirection = (int)Mathf.Sign(ScollWhellDelta);
            ScollWhellDelta -= SwapDirection * mouseAxisBreakpoin;

            int CurrenWeaponIndex = (int)currentWeapon.weaponType;
            CurrenWeaponIndex += SwapDirection;

            if (CurrenWeaponIndex < 0)
            {
                CurrenWeaponIndex = (int)WeaponState.Total + CurrenWeaponIndex;
            }
            if (CurrenWeaponIndex >= (int)WeaponState.Total)
            {
                CurrenWeaponIndex = 0;
            }
            WeaponSwapAnimation(CurrenWeaponIndex);

        }
    }

    private void WeaponSwapAnimation(int currentWeaponIndex)
    {
        foreach (var weapon in avilableWeapons)
        {
            weapon.gameObject.SetActive(false);
        }
        Debug.Log(currentWeaponIndex);
        currentWeapon = avilableWeapons[currentWeaponIndex];
        currentWeapon.gameObject.SetActive(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        int CurrenWeaponIndex = (int)currentWeapon.weaponType;
        if (CurrenWeaponIndex == (int)WeaponState.Melee)
        {
            return;
        }
        if (other.gameObject.layer == 10)
        {
            Destroy(other.gameObject);

            currentWeapon.ammunition += 50;
        }
    }
}
