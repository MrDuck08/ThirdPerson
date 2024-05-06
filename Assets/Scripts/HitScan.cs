using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScan : Weapon
{
    public ParticleSystem hitParticle = null;
    public ParticleSystem muzzleFlash = null;

    public override void Start()
    {
        hitParticle.gameObject.SetActive(false);
        base.Start();
    }

    public override bool Fire()
    {
        muzzleFlash.Play();

        if (base.Fire() == false)
        {
            return true;
        }

        hitParticle.gameObject.SetActive(false);

        Ray WeaponRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(WeaponRay, out hit, weaponRange, ~ignoreHitMask))
        {
            hitParticle.Play();

            hitParticle.transform.SetParent(null);
            hitParticle.transform.position = hit.point;

            hitParticle.transform.position = hit.point;
            hitParticle.transform.forward = hit.normal;
            hitParticle.transform.Translate(hit.normal.normalized * 0.1f);

            hitParticle.gameObject.SetActive(true);
            Debug.Log(hit);
            var PlayerHit = hit.transform.gameObject.GetComponent<PlayerMovment>();
        }

        if (Physics.Raycast(WeaponRay, out hit, weaponRange, enemyMask))
        {
            //var EnemyHit = hit.transform.gameObject.GetComponent<Enemy>();

            //if (EnemyHit != null)
            //{
            //    EnemyHit.TakeDamage();
            //}
        }
        return false;
    }
}
