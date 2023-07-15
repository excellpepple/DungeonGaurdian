using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/MeleeWeapon")]
public class MeleeWeaponSO : ScriptableObject
{
    public int attackDamage = 20;
    public float attackDelay = 1.5f;
    public float attackRange = 2f;
}
