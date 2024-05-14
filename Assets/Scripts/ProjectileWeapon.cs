using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class ProjectileWeapon : Weapon
{
    public ShotgunProjectile shotgunProjectile;

    public override bool Fire()
    {
        if (base.Fire() == false)
        {
            return true;
        }

        ShotgunProjectile SpawnedProjectile = Instantiate(shotgunProjectile);

        SpawnedProjectile.SpawnProjectile(new Vector3(transform.position.x, transform.position.y, transform.position.z), Camera.main.transform.forward.normalized * 10000.0f + Camera.main.transform.position);

        return false;
    }
}
