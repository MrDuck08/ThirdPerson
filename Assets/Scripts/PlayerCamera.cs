using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Options options;

    [SerializeField] float mouseSensitivity = 110;

    public Transform playerBody;

    float xRotation = 0f;
    float yRotation = 0f;

    public float yClamp = 0f;
    public float zClamp = 30f;


    void Start()
    {       
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * options.cameraSensativity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * options.cameraSensativity * Time.deltaTime;

        xRotation -= MouseY;
        yRotation += MouseX;

        xRotation = Mathf.Clamp(xRotation, yClamp, zClamp);

        playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
