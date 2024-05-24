using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    public List<MelleHitbox> MeleeHitbox = new List<MelleHitbox>();

    float comboDropTimer = 1f;
    int onWhatCombo = 0;

    bool meleeAttackPause;

    [SerializeField] Transform playerTransform;

    public override bool Fire()
    {
        if (base.Fire() == false)
        {
            return true;
        }

        //StopCoroutine(ComboStopRoutine());
        //comboDropTimer = 1;

        if (meleeAttackPause)
        {
            return false;
        }

        
        onWhatCombo++;


        GameObject spawnedHitbox = Instantiate(MeleeHitbox[onWhatCombo].HitboxPrefab);
        spawnedHitbox.transform.position = playerTransform.position + new Vector3(0,0, 1.11f);
        spawnedHitbox.transform.rotation = playerTransform.rotation;
        //MeleeHitbox[onWhatCombo].HitboxPrefab.SetActive(true);

        StartCoroutine(ComboStopRoutine());

        return false;
    }

    IEnumerator NextAttackRoutien()
    {
        meleeAttackPause = true;

        yield return new WaitForSeconds(0.1f);

        meleeAttackPause = false;
    }

    IEnumerator ComboStopRoutine()
    {

        Debug.Log("START");

        yield return new WaitForSeconds(comboDropTimer);

        Debug.Log("RESET");
        onWhatCombo = 0;
    }
}
