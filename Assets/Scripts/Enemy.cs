using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    float health = 100f;
    public float explosionKnockbackStrenght = 2;

    Rigidbody rbEnemy;

    bool knockBack;

    #region Gravity

    public bool isGrounded;

    public Transform groundCheck;

    public float groundDistance = 0.4f;
    public float gravity = -9.82f;

    public LayerMask groundMask;

    Vector3 velocity;

    #endregion

    private void Start()
    {
        rbEnemy = GetComponent<Rigidbody>();

        knockBack = false;
    }

    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1.0f;
        }

        velocity.y += gravity * Time.deltaTime;

        if(!knockBack)
        {
            rbEnemy.velocity = velocity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            velocity.y += Mathf.Sqrt(explosionKnockbackStrenght * -2 * gravity);

            health -= 25;

            rbEnemy.velocity = velocity;
        }

        if(other.gameObject.layer == 9)
        {
            knockBack = true;

            rbEnemy.velocity = Vector3.zero + other.gameObject.transform.forward * 20;

            health -= 20;

            StartCoroutine(meleeKnockbackRoutine());
        }

        if (other.gameObject.layer == 8)
        {
            health -= 1;
        }
    }

    IEnumerator meleeKnockbackRoutine()
    {
        yield return new WaitForSeconds(0.1f);

        knockBack = false;
    }
}
