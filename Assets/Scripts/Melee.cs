using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{


    [SerializeField] Transform playerTransform;

    DualKnifes dualKnifes;

    public override bool Fire()
    {
        if (base.Fire() == false)
        {
            return true;
        }




        dualKnifes = FindFirstObjectByType<DualKnifes>();
        dualKnifes.DualKnifeAttack(playerTransform);



        return false;
    }
}
