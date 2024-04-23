using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerMovment : MonoBehaviour
{
    Rigidbody rb;

    Vector3 movementInput;
    Vector3 playerVelocity;

    float XDirection = 1;
    float ZDirection = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        XDirection = movementInput.x;
        ZDirection = movementInput.y;

        Vector3 Test = transform.right * XDirection + transform.forward * movementInput.z;

        rb.velocity = Test;
    }

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector3>();
    }
}
