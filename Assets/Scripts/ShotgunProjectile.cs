using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShotgunProjectile : MonoBehaviour
{
    protected Vector3 spawnPosition = Vector3.zero;

    protected Vector3 aimPoint = Vector3.zero;

    protected Vector3 aimDirection = Vector3.zero;

    protected Vector3 whereToAim = Vector3.zero;

    public float randomDirection1 = 15f;
    public float randomDirection2 = -15f;

    public void Update()
    {
        transform.position += transform.forward * 50 * Time.deltaTime;
    }

    public virtual void SpawnProjectile(Vector3 spawnPos, Vector3 AimPosition)
    {
        spawnPosition = spawnPos;
        transform.position = spawnPos;

        aimPoint = AimPosition;
        aimDirection = (aimPoint - spawnPosition).normalized;

        Vector3 RandomRotation = new Vector3(Random.Range(randomDirection1, randomDirection2), Random.Range(randomDirection1, randomDirection2), Random.Range(randomDirection1, randomDirection2));


        whereToAim = aimPoint + RandomRotation;

        transform.LookAt(whereToAim);
    }
}
