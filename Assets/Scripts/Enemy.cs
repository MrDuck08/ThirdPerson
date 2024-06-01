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

    public float maxRange = 7;

    public float shootCooldownMaxTime = 0.1f;
    float shoootCooldownLeft;

    bool startShootCooldown;

    #endregion

    private void Start()
    {
        rbEnemy = GetComponent<Rigidbody>();

        knockBack = false;

        damagePauseCurrentTime = 0;
    }

    void Update()
    {
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

        if(!takeDamage && !inTheAir)
        {
            if (!stop)
            {
                playerMovment = FindObjectOfType<PlayerMovment>();
                Vector3 playerTransform = playerMovment.transform.position;
                transform.position = Vector3.MoveTowards(transform.position, playerTransform, 5 * Time.deltaTime);
            }

            transform.LookAt(playerMovment.transform.position);

            bulletSpawner.transform.LookAt(playerMovment.transform.position);
        }

        #endregion

        #region Weapon

        float DistanceToPlayer = Vector3.Distance(playerMovment.transform.position, transform.position);

        if(DistanceToPlayer < maxRange && !takeDamage && !inTheAir)
        {
           stop = true;

            startShootCooldown = true;

           //damagePauseCurrentTime -= Time.deltaTime;


           //if (damagePauseCurrentTime <= 0)
           //{
           //     MachineGunBullet spawnProjectile = Instantiate(machineGunBullet);
           //     spawnProjectile.SpawnProjectile(bulletSpawner.transform.position, playerMovment.transform.position);
           //}            
        }
        else
        {
            startShootCooldown = false;

            //damagePauseCurrentTime = 0;
            stop = false;
        }


        ShotCooldown();
        #endregion
    }

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

        yield return new WaitForSeconds(0.1f);

        takeDamage = false;
    }

    IEnumerator ShotCooldownRoutine()
    {
        yield return new WaitForSeconds(shootCooldownMaxTime);

        MachineGunBullet spawnProjectile = Instantiate(machineGunBullet);
        spawnProjectile.SpawnProjectile(bulletSpawner.transform.position, playerMovment.transform.position);
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
                spawnProjectile.SpawnProjectile(bulletSpawner.transform.position, playerMovment.transform.position);

                shoootCooldownLeft = shootCooldownMaxTime;
            }
        }

    }
}
