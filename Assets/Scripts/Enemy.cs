using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region health & Damage

    [Header("health")]

    float health = 100f;
    float damageBreakTime = 0.2f;

    public float explosionKnockbackStrenght = 2;

    Rigidbody rbEnemy;

    bool knockBack;
    bool takeDamage = false;
    bool inTheAir = false;
    bool stop = false;
    
    PlayerMovment playerMovment;

    #endregion

    #region Gravity

    [Header("Gravity")]

    public bool isGrounded;

    public Transform groundCheck;

    public float groundDistance = 0.4f;
    public float gravity = -9.82f;

    public LayerMask groundMask;

    Vector3 velocity;

    #endregion

    #region Shooting

    [Header("Shooting")]

    public MachineGunBullet machineGunBullet;

    [SerializeField] GameObject bulletSpawner;

    float damagePauseTime = 0.4f;
    float damagePauseCurrentTime;
    float shoootCooldownLeft;

    public float maxShootRange = 7;
    public float shootCooldownMaxTime = 0.1f;
    public float bulletSpeed = 20f;


    bool startShootCooldown;

    #endregion

    #region Movment

    [Header("Movment")]

    [SerializeField] float maxWalkDistance = 20;
    [SerializeField] float moveSpeed = 5;

    Vector3 spaceToMoveTo;

    bool pauseNewNumber = false;

    public bool stationaryEnemy = false;

    bool sleep;

    #endregion

    private void Start()
    {
        rbEnemy = GetComponent<Rigidbody>();

        knockBack = false;

        damagePauseCurrentTime = 0;


        StartCoroutine(SleepOnStartRoutine());
    }

    void Update()
    {
        playerMovment = FindObjectOfType<PlayerMovment>();
        float DistanceToPlayer = Vector3.Distance(playerMovment.transform.position, transform.position);

        #region Movemnt & Gravity

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1.0f;
        }
        if (!isGrounded)
        {
            inTheAir = true;
        }
        else
        {
            inTheAir = false;
        }
        

        velocity.y += gravity * Time.deltaTime;

        if(!knockBack)
        {
            rbEnemy.velocity = velocity;
        }

        if(!takeDamage && !inTheAir && !sleep)
        {
            if (DistanceToPlayer < maxWalkDistance)
            {
                if (!stop)
                {

                    Vector3 playerTransform = playerMovment.transform.position;
                    transform.position = Vector3.MoveTowards(transform.position, playerTransform, moveSpeed * Time.deltaTime);
                }
            }
            else
            {
                if (!stationaryEnemy && !stop)
                {
                    StartCoroutine(RandomSpaceRoutine());
                    transform.position = Vector3.MoveTowards(transform.position, spaceToMoveTo, moveSpeed * Time.deltaTime);

                    transform.LookAt(spaceToMoveTo);
                }
            }

        }

        #endregion

        #region Weapon


        if(DistanceToPlayer < maxShootRange && !takeDamage && !inTheAir)
        {
           stop = true;

            transform.LookAt(playerMovment.transform.position);

            bulletSpawner.transform.LookAt(playerMovment.transform.position);

            startShootCooldown = true;            
        }
        else
        {
            startShootCooldown = false;

            stop = false;
        }


        ShotCooldown();
        #endregion
    }

    #region Collisions

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            velocity.y += Mathf.Sqrt(explosionKnockbackStrenght * -2 * gravity);

            health -= 0;

            StartCoroutine(TakeDamageStopRoutine(damageBreakTime));
            rbEnemy.velocity = velocity;
            damagePauseCurrentTime = damagePauseTime;
        }

        if(other.gameObject.layer == 9)
        {
            knockBack = true;

            rbEnemy.velocity = Vector3.zero + other.gameObject.transform.forward * 20;

            health -= 20;

            StartCoroutine(TakeDamageStopRoutine(damageBreakTime));
            StartCoroutine(meleeKnockbackRoutine());
            damagePauseCurrentTime = damagePauseTime;
        }

        if (other.gameObject.layer == 8)
        {
            health -= 1;

            StartCoroutine(TakeDamageStopRoutine(damageBreakTime));
            damagePauseCurrentTime = damagePauseTime;
        }
    }

    #endregion

    #region Timers

    IEnumerator meleeKnockbackRoutine()
    {

        yield return new WaitForSeconds(0.1f);

        rbEnemy.velocity = Vector3.zero;

        yield return new WaitForSeconds(0.3f);

        knockBack = false;
    }

    IEnumerator TakeDamageStopRoutine(float howLongTillFalse)
    {
        takeDamage = true;

        yield return new WaitForSeconds(0.4f);

        takeDamage = false;
    }

    public void ShotCooldown()
    {

        if (startShootCooldown)
        {
            shoootCooldownLeft -= Time.deltaTime;

            if (shoootCooldownLeft <= 0)
            {
                startShootCooldown = false;

                MachineGunBullet spawnProjectile = Instantiate(machineGunBullet);
                spawnProjectile.SpawnProjectile(bulletSpawner.transform.position, playerMovment.transform.position, bulletSpeed);

                shoootCooldownLeft = shootCooldownMaxTime;
            }
        }

    }

    IEnumerator RandomSpaceRoutine()
    {
        if (!pauseNewNumber)
        {
            pauseNewNumber = true;

            spaceToMoveTo = new Vector3(transform.position.x + Random.Range(0, 5), transform.position.y, transform.position.z + Random.Range(0, 5));

            yield return new WaitForSeconds(2);

            pauseNewNumber = false;
        }
    }

    IEnumerator SleepOnStartRoutine()
    {
        sleep = true;

        yield return new WaitForSeconds(0.3f);

        sleep = false;
    }
    #endregion

}
