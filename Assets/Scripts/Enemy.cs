using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region health & Damage

    [Header("Health & Damage")]

    [SerializeField] GameObject takenDamageText;

    float health = 100f;
    float damageBreakTime = 0.2f;
    float damageTaken;

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

    float damagePauseTime = 0.1f;
    float damagePauseCurrentTime;
    float shoootCooldownLeft;

    public float maxShootRange = 7;
    public float shootCooldownMaxTime = 0.1f;
    public float bulletSpeed = 20f;
    public float bulletLifeTime = 1f;


    bool startShootCooldown;

    #endregion

    #region Movment

    [Header("Movment")]

    [SerializeField] float maxWalkDistance = 20;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float fallDownAfterMelee = -0.3f;

    Vector3 spaceToMoveTo;

    bool pauseNewNumber = false;
    bool sleep;
    bool meleeKnockback = false;

    #endregion

    #region Modes

    [Header("Mode")]

    public bool stationaryEnemy = false;
    public bool isDummy = false;

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

        #endregion

        #region Enemy AI + Weapon

        if (!isDummy)
        {

            #region Enemy AI

            if (!takeDamage && !inTheAir && !sleep)
            {
                if (DistanceToPlayer < maxWalkDistance)
                {
                    if (!stop)
                    {

                        Vector3 playerTransform = playerMovment.transform.position;
                        transform.position = Vector3.MoveTowards(transform.position, playerTransform, moveSpeed * Time.deltaTime);

                        transform.LookAt(new Vector3(playerMovment.transform.position.x, transform.position.y, playerMovment.transform.position.z));

                        bulletSpawner.transform.LookAt(playerMovment.transform.position);
                    }
                }
                else
                {
                    if (!stationaryEnemy && !stop)
                    {
                        StartCoroutine(RandomSpaceRoutine());
                        transform.position = Vector3.MoveTowards(transform.position, spaceToMoveTo, moveSpeed * Time.deltaTime);

                        transform.LookAt(new Vector3(spaceToMoveTo.x, transform.position.y, spaceToMoveTo.z));


                    }
                }
            }

            #endregion

            #region Weapon


            if (DistanceToPlayer < maxShootRange && !takeDamage && !inTheAir)
                {
                    stop = true;

                    transform.LookAt(new Vector3(playerMovment.transform.position.x, transform.position.y, playerMovment.transform.position.z));

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
        #endregion

        #region Am I Dummy?
        if (isDummy)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        #endregion

        #region Melee Knockback

        if (meleeKnockback)
        {
            damagePauseCurrentTime -= Time.deltaTime;

            knockBack = true;



            if (damagePauseCurrentTime <= 0)
            {
                rbEnemy.velocity = Vector3.zero;
            }
            if (damagePauseCurrentTime <= fallDownAfterMelee)
            {
                meleeKnockback = false;
                knockBack = false;
            }
        }

        #endregion

    }

    #region Collisions

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            velocity.y += Mathf.Sqrt(explosionKnockbackStrenght * -2 * gravity);

            if (!isDummy)
            {
                health -= 25;
            }
            else
            {
                GameObject spawnedEffect = Instantiate(takenDamageText);

                damageTaken = 25;



                spawnedEffect.transform.position = other.transform.position;
                spawnedEffect.transform.GetComponent<TextMeshPro>().text = damageTaken.ToString();

                spawnedEffect.transform.LookAt(playerMovment.transform.position);

                spawnedEffect.transform.rotation *= Quaternion.Euler(1, -180 ,1);

                StartCoroutine(DestroyEffectRoutine(spawnedEffect));
            }

            StartCoroutine(TakeDamageStopRoutine());

            
            damagePauseCurrentTime = damagePauseTime;
        }

        if(other.gameObject.layer == 9)
        {
            knockBack = true;

            rbEnemy.velocity = Vector3.zero + other.gameObject.transform.forward * 20;
            //rbEnemy.velocity = velocity;

            if (!isDummy)
            {
                health -= 20;
            }
            else
            {
                GameObject spawnedEffect = Instantiate(takenDamageText);

                damageTaken = 20;



                spawnedEffect.transform.position = other.transform.position;
                spawnedEffect.transform.GetComponent<TextMeshPro>().text = damageTaken.ToString();

                spawnedEffect.transform.LookAt(playerMovment.transform.position);
                spawnedEffect.transform.rotation *= Quaternion.Euler(1, -180, 1);

                StartCoroutine(DestroyEffectRoutine(spawnedEffect));
            }

            meleeKnockback = true;
            StartCoroutine(TakeDamageStopRoutine());
            //StartCoroutine(meleeKnockbackRoutine());
            damagePauseCurrentTime = damagePauseTime;
        }

        if (other.gameObject.layer == 8)
        {
            if(!isDummy)
            {
                health -= 1;

                StartCoroutine(TakeDamageStopRoutine());
                damagePauseCurrentTime = damagePauseTime;
            }
            else
            {
                GameObject spawnedEffect = Instantiate(takenDamageText);

                damageTaken = 1;

                

                spawnedEffect.transform.position = other.transform.position;
                spawnedEffect.transform.GetComponent<TextMeshPro>().text = damageTaken.ToString();

                spawnedEffect.transform.LookAt(playerMovment.transform.position);
                spawnedEffect.transform.rotation *= Quaternion.Euler(1, -180, 1);

                StartCoroutine(DestroyEffectRoutine(spawnedEffect));
            }
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

    IEnumerator TakeDamageStopRoutine()
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
                spawnProjectile.SpawnProjectile(bulletSpawner.transform.position, playerMovment.transform.position, bulletSpeed, bulletLifeTime);

                shoootCooldownLeft = shootCooldownMaxTime;
            }
        }

    }

    IEnumerator RandomSpaceRoutine()
    {
        if (!pauseNewNumber)
        {
            pauseNewNumber = true;

            spaceToMoveTo = new Vector3(transform.position.x + Random.Range(-5, 5), transform.position.y, transform.position.z + Random.Range(-5, 5));

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

    IEnumerator DestroyEffectRoutine(GameObject objectToDestroy)
    {


        yield return new WaitForSeconds(1f);

        Destroy(objectToDestroy);
    }
    #endregion

}
