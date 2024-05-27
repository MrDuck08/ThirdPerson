using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "MelleHitbox")]
public class MelleHitbox : ScriptableObject
{
    public GameObject HitboxPrefab = null;
    public float damage = 0;
    public Vector3 forcePower = Vector3.zero;
    public int weaponOrder = 0;
}
