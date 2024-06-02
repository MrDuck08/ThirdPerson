using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject menuCanvas;
    [SerializeField] TMP_InputField sensitivityInputField;
    [SerializeField] Options options;

    public bool MenuIsActive;

    private void Update()
    {
        MenuAnywhere();
    }

    private void Start()
    {
        menuCanvas.SetActive(false);
    }

    public void startAgain()
    {
        menuCanvas.SetActive(false);
        Time.timeScale = 1.0f;
        MenuIsActive = false;
    }

    void MenuAnywhere()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (!MenuIsActive)
            {
                menuCanvas.SetActive(true);

                MenuIsActive = true;

                Cursor.lockState = CursorLockMode.None;

                Time.timeScale = 0f;

                return;
            }

            if (MenuIsActive)
            {
                menuCanvas.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;

                MenuIsActive = false;

                Time.timeScale = 1f;
            }
        }
    }

    public void ChangeSensetivity()
    {
        int inputFieldValue;
        bool successfulValue = int.TryParse(sensitivityInputField.text, out inputFieldValue);

        if (successfulValue)
        {
            options.cameraSensativity = inputFieldValue;
        }
    }
}
