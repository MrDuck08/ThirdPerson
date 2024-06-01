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

    public LayerMask ignoreLayer;

    float health = 100;

    public float howlongStill = -1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if(health < 0)
        {
            Debug.Log("DIE");
            Time.timeScale = 0;
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1.0f;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y += Mathf.Sqrt(jumpStrengh * -2 * gravity);
        }

        XDirection = movementInput.x;
        ZDirection = movementInput.z;

        Vector3 Movment = transform.right * XDirection + transform.forward * ZDirection;

        velocity.y += gravity * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 16;
        }
        else
        {
            moveSpeed = 8;
        }

        if (knockBack == false)
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
                rb.velocity = Vector3.zero;
            }
            if(meleeAtackTimeLeft <= howlongStill)
            {
                meleeAtack = false;
                knockBack = false;
            }
        }

        Ray WeaponRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(WeaponRay, out hit, Mathf.Infinity, ~ignoreLayer))
        {

            gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.transform.LookAt(hit.point);
        }

    }

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector3>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7)
        {
            health -= 10;
        }
    }
}
