using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class ProjectileWeapon : Weapon
{
    public ShotgunProjectile shotgunProjectile;

    int howManyMorePellets = 0;

    PlayerMovment playerMovment;

    public override void Start()
    {
        base.Start();

        playerMovment = FindObjectOfType<PlayerMovment>();
    }

    public override bool Fire()
    {
        if (base.Fire() == false)
        {
            return true;
        }

        while (true)
        {

            ShotgunProjectile SpawnedProjectile = Instantiate(shotgunProjectile);

            SpawnedProjectile.SpawnProjectile(new Vector3(transform.position.x, transform.position.y, transform.position.z), Camera.main.transform.forward.normalized * 10000.0f + Camera.main.transform.position);

            howManyMorePellets++;

            if (howManyMorePellets == 70)
            {
                playerMovment.ShotgunPushBack(Camera.main.transform.forward.normalized + Camera.main.transform.position);
                howManyMorePellets = 0;
                return false;
            }
        }
    }
}
