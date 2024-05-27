using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.XR;

public class PlayerMovment : MonoBehaviour
{
    #region Movment

    [Header("Movment")]

    Rigidbody rb;

    Vector3 movementInput;
    Vector3 playerVelocity;

    float XDirection = 1;
    float ZDirection = 1;

    public float moveSpeed = 8;
    public float jumpStrengh = 3;

    public float ShotgunKnockbackPower = 10;

    bool knockBack = false;

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

    #region melee

    [Header("Melee")]

    public bool meleeAtack = false;

    public float meleeAtackTime = 0.1f;
    public float meleeAtackTimeLeft;

    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1.0f;
        }

        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y += Mathf.Sqrt(jumpStrengh * -2 * gravity);
        }

        XDirection = movementInput.x;
        ZDirection = movementInput.z;

        Vector3 Movment = transform.right * XDirection + transform.forward * ZDirection;

        velocity.y += gravity * Time.deltaTime;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 16;
        }
        else
        {
            moveSpeed = 8;
        }

        if(!knockBack)
        {
            rb.velocity = Movment * moveSpeed + velocity;
        }

        if (meleeAtack)
        {
            meleeAtackTimeLeft -= Time.deltaTime;

            knockBack = true;

            rb.velocity = Vector3.zero + gameObject.transform.forward * 20;

            if (meleeAtackTimeLeft <= 0)
            {
                meleeAtack = false;
                knockBack = false;
            }
        }
    }

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector3>();
    }
}
