using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerMovment : MonoBehaviour
{
    Rigidbody rb;

    Vector2 movementInput;

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

        rb.velocity = new Vector3 (XDirection, 0, ZDirection);
    }

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector3>();
    }
}
