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

    public override void Update()
    {
      
        base.Update();
     

        Ray WeaponRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit = new RaycastHit();

        transform.LookAt(hit.point);
        Debug.Log("WORK");

    }

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

        Ray WeaponRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit = new RaycastHit();
        
        if (Physics.Raycast(WeaponRay, out hit, Mathf.Infinity, ~ignoreHitMask))
        {
            while (true)
            {

                ShotgunProjectile SpawnedProjectile = Instantiate(shotgunProjectile);

                SpawnedProjectile.SpawnProjectile(new Vector3(transform.position.x, transform.position.y, transform.position.z), Camera.main.transform.forward.normalized * hit.distance + Camera.main.transform.position, hit);

                howManyMorePellets++;

                if (howManyMorePellets == 70)
                {
                    //playerMovment.ShotgunPushBack(Camera.main.transform.forward.normalized);

                    howManyMorePellets = 0;
                    return false;
                }
            }
        }
        return false;
    }
}
