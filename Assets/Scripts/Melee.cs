using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    public List<MelleHitbox> Guns = new List<MelleHitbox>();


    public override bool Fire()
    {
        if (base.Fire() == false)
        {
            return true;
        }

        foreach (MelleHitbox m in Guns)
        {

        }

        return false;
    }


}
