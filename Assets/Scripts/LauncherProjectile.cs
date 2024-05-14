using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherProjectile : MonoBehaviour
{
    #region Vectors

    protected Vector3 spawnPosition = Vector3.zero;

    protected Vector3 aimPoint = Vector3.zero;

    protected Vector3 aimDirection = Vector3.zero;

    protected Vector3 finalDestination = Vector3.zero;

    #endregion

    RaycastHit diveLocation;

    bool diveBool = false;
    bool hitSomething;

    float lifeTime = 10f;

    int testInt = 3;

    public GameObject ExplosionObject;

    private void Start()
    {
        ExplosionObject.SetActive(false);
    }

    private void Update()
    {
        if (!diveBool)
        {
            transform.position += aimDirection * 5 * Time.deltaTime;

            transform.position += new Vector3(0, 5, 0) * Time.deltaTime;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, diveLocation.point, 100 * Time.deltaTime);
            if (transform.position == diveLocation.point)
            {
                StartCoroutine(ExplodeRoutine());
            }
        }

        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ExplodeRoutine()
    {
        ExplosionObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        Destroy(gameObject);
    }

    public virtual void SpawnProjectile(Vector3 spawnPos, Vector3 finalDestination, Vector3 AimPosition, RaycastHit raycastHit)
    {
        spawnPosition = spawnPos;
        transform.position = spawnPosition;

        aimPoint = AimPosition;
        aimDirection = (aimPoint - spawnPosition).normalized;
        transform.LookAt(aimPoint);

        StartCoroutine(DownShot(raycastHit));
    }

    IEnumerator DownShot(RaycastHit whereToGo)
    {
        yield return new WaitForSeconds(1);

        diveLocation = whereToGo;

        diveBool = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.layer == testInt)
        //{
        //    Debug.Log("GROUND");
        //}
        //if (other.gameObject.layer == ~2)
        //{
        //    Debug.Log("WORK 1");
        //    if(other.gameObject.layer == ~6)
        //    {
        //        Debug.Log("WORK 2");
        //    }
        //}
    }
}
