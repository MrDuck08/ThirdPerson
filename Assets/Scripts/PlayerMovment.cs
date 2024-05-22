using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.XR;

public class PlayerMovment : MonoBehaviour
{
    #region Movment

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

    public bool isGrounded;

    public Transform groundCheck;

    public float groundDistance = 0.4f;
    public float gravity = -9.82f;

    public LayerMask groundMask;

    Vector3 velocity;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
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
    }

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector3>();
    }

    //public void ShotgunPushBack(Vector3 whereToshotBack)
    //{
    //    knockBack = true;

    //    //rb.velocity = -whereToshotBack * ShotgunKnockbackPower;

    //    //transform.position += -whereToshotBack * ShotgunKnockbackPower * Time.deltaTime;

    //    knockBack = false;

    //    StartCoroutine(ShotgunKnockBackRoutine(whereToshotBack));
    //}

    //IEnumerator ShotgunKnockBackRoutine(Vector3 whereToshotBack)
    //{
    //    knockBack = true;

    //    yield return new WaitForSeconds(0.3f);

    //    //rb.velocity = -whereToshotBack * ShotgunKnockbackPower;

    //    transform.position += -whereToshotBack * ShotgunKnockbackPower * Time.deltaTime;

    //    knockBack = false;
    //}
}
