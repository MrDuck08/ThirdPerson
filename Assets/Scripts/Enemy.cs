using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    float health = 5f;
    float explosionKnockbackStrenght = 2;

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
        //if (knockBack)
        //{
        //    velocity.y += Mathf.Sqrt(explosionKnockbackStrenght * -2 * gravity);
        //}

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
            knockBack = true;

            velocity.y += Mathf.Sqrt(explosionKnockbackStrenght * -2 * gravity);

            rbEnemy.velocity = velocity;

            knockBack = false; 
        }
    }
}
