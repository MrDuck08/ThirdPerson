using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DualKnifes : MonoBehaviour
{
    public List<MelleHitbox> MeleeHitbox = new List<MelleHitbox>();

    float comboDropTimer = 1f;
    int onWhatCombo = -1;
    int maxCombo;

    float comboTime = 1;
    float currentComboTime;

    bool meleeAttackPause;
    bool comboTimerStart = false;


    private void Start()
    {
        maxCombo = MeleeHitbox.Count;
    }

    public void DualKnifeAttack(Transform whereToSpawn)
    {

        if (meleeAttackPause)
        {
            return;
        }



        StartCoroutine(NextAttackRoutien());

        onWhatCombo++;

        if(onWhatCombo < maxCombo)
        {
            GameObject spawnedHitbox = Instantiate(MeleeHitbox[onWhatCombo].HitboxPrefab);

            spawnedHitbox.transform.position = whereToSpawn.position + whereToSpawn.forward * 1.11f;
            spawnedHitbox.gameObject.transform.rotation = whereToSpawn.rotation;


            currentComboTime = comboTime;
            comboTimerStart = true;
        }
        else
        {
            onWhatCombo = -1;
        }

    }

    private void Update()
    {
        if (comboTimerStart)
        {
            currentComboTime -= Time.deltaTime;

            if (currentComboTime <= 0)
            {
                comboTimerStart = false;
                onWhatCombo = -1;
            }
        }




    }

    IEnumerator NextAttackRoutien()
    {
        meleeAttackPause = true;

        yield return new WaitForSeconds(0.1f);


        meleeAttackPause = false;
    }
}
