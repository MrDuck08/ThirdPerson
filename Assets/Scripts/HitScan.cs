using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScan : Weapon
{

    public override void Start()
    {
        base.Start();
    }

    public override bool Fire()
    {

        if (base.Fire() == false)
        {
            return true;
        }

        Ray WeaponRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(WeaponRay, out hit, weaponRange, ~ignoreHitMask))
        {

            var PlayerHit = hit.transform.gameObject.GetComponent<PlayerMovment>();
        }
        return false;
    }
}
