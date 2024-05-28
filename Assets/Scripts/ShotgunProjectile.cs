using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShotgunProjectile : MonoBehaviour
{

    #region Vectors

    protected Vector3 spawnPosition = Vector3.zero;

    protected Vector3 aimPoint = Vector3.zero;

    protected Vector3 aimDirection = Vector3.zero;

    protected Vector3 whereToAim = Vector3.zero;

    #endregion

    public float randomDirection1 = 2000f;
    public float randomDirection2 = -2000f;

    float lifeTime = 2;

    public void Update()
    {
        transform.position += transform.forward * 50 * Time.deltaTime;
    }

    public virtual void SpawnProjectile(Vector3 spawnPos, Vector3 AimPosition, RaycastHit hit)
    {
        spawnPosition = spawnPos;
        transform.position = spawnPos;

        aimPoint = AimPosition;
        aimDirection = (aimPoint - spawnPosition).normalized;


        if(hit.distance < 10)
        {
            hit.distance = 5;
        }

        Vector3 RandomRotation = new Vector3(Random.Range(hit.distance * randomDirection1, hit.distance * randomDirection2), Random.Range(hit.distance * randomDirection1, hit.distance * randomDirection2), Random.Range(hit.distance * randomDirection1, hit.distance * randomDirection2));


        whereToAim = aimPoint + RandomRotation;

        transform.LookAt(whereToAim);


        StartCoroutine(LifeRoutine());
    }

    IEnumerator LifeRoutine()
    {

        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6 || other.gameObject.layer == 8 || other.gameObject.layer == 11)
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
