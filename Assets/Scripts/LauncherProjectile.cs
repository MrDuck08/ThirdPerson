using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherProjectile : MonoBehaviour
{
    protected Vector3 spawnPosition = Vector3.zero;

    protected Vector3 aimPoint = Vector3.zero;

    protected Vector3 aimDirection = Vector3.zero;

    protected Vector3 finalDestination = Vector3.zero;

    private void Update()
    {
        Vector3 MoveVector = transform.forward;

        MoveVector.x += 1f;

        transform.position +=  aimDirection * Time.deltaTime * 1;

        //new Vector3(MoveVector.x, 1, 0)
        //transform.position += aimDirection * movmentSpeed * Time.deltaTime;

        //transform.position += new Vector3(1, 1, 0) * aimDirection * Time.deltaTime;
    }

    public virtual void SpawnProjectile(Vector3 spawnPos, Vector3 finalDestination, Vector3 AimPosition)
    {
        spawnPosition = spawnPos;
        transform.position = spawnPosition;

        aimPoint = AimPosition;
        aimDirection = (aimPoint - spawnPosition).normalized;
        transform.LookAt(aimPoint);
    }
}
