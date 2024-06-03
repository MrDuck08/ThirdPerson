using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    float health = 100;

    [SerializeField] GameObject loseCanvas;

    public bool dead = false;

    private void Start()
    {
        loseCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            health -= 5;
            if (health <= 0)
            {
                Time.timeScale = 0f;
                loseCanvas.SetActive(true);
                dead = true;

                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
