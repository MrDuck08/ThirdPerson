using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherWeapon : Weapon
{
    public LauncherProjectile Projectile;

    public float x = 0.5f;
    public float y = 0.5f;

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
            LauncherProjectile SpawnedProjectile = Instantiate(Projectile);

            SpawnedProjectile.SpawnProjectile(new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z),   hit.transform.position,   Camera.main.transform.forward.normalized * 10000.0f + Camera.main.transform.position, hit);
        }

        return false;
    }
}
