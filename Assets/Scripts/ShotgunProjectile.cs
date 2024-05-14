using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShotgunProjectile : MonoBehaviour
{
    protected Vector3 spawnPosition = Vector3.zero;

    protected Vector3 aimPoint = Vector3.zero;

    protected Vector3 aimDirection = Vector3.zero;

    public void Update()
    {
        transform.position += transform.forward * 5 * Time.deltaTime;
    }

    public virtual void SpawnProjectile(Vector3 spawnPos, Vector3 AimPosition)
    {
        spawnPosition = spawnPos;
        transform.position = spawnPos;

        aimPoint = AimPosition;
        aimDirection = (aimPoint - spawnPosition).normalized;
        transform.LookAt(aimPoint);

        transform.rotation = Quaternion.Euler(Random.Range(15, -15), Random.Range(15, -15), 0);
    }
}
