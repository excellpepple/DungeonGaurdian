using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Ranged Weapon")]
public class RangedWeapon : ScriptableObject
{
    public GameObject projectilePrefab;
    public int attackDamage = 10;
    public float attackDelay = 1f;
    public int manaCost = 10;
}
