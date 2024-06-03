using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{

    [SerializeField] Transform playerTransform;

    DualKnifes dualKnifes;

    PlayerMovment playerMovment;

    public override void Start()
    {
        base.Start();

        playerMovment = FindObjectOfType<PlayerMovment>();
    }

    public override bool Fire()
    {
        if (base.Fire() == false)
        {
            return true;
        }

        dualKnifes = FindFirstObjectByType<DualKnifes>();

        if(dualKnifes.onWhatCombo <= 1)
        {
            dualKnifes.DualKnifeAttack(playerTransform);

            playerMovment.meleeAtackTimeLeft = playerMovment.stopMovmentAfterMeleeTime;
            playerMovment.meleeAtack = true;

        }
        return false;
    }
}
