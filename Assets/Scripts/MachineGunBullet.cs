using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MachineGunBullet : MonoBehaviour
{
    #region Vectors

    protected Vector3 spawnPosition = Vector3.zero;

    protected Vector3 aimPoint = Vector3.zero;

    protected Vector3 aimDirection = Vector3.zero;

    #endregion

    float lifeTime = 1f;

    float bulletSpeed;

    private void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    public virtual void SpawnProjectile(Vector3 spawnPos, Vector3 AimGoal, float whatBulletSpeed, float bulletLifeTime)
    {
        spawnPosition = spawnPos;
        transform.position = spawnPos;
        bulletSpeed = whatBulletSpeed;
        lifeTime = bulletLifeTime;

        transform.LookAt(AimGoal);

        StartCoroutine(LifeRoutine());
    }

    IEnumerator LifeRoutine()
    {

        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3 || other.gameObject.layer == 10 || other.gameObject.layer == 11)
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
