using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy data", menuName = "Characters/Enemy data")]
public class EnemyData : ScriptableObject
{
    [Header("Statistics")]
    public int maxHealth;
    public float moveSpeed;

    public int strenge;
    public int power;

    public float hability;

    [Header("Weapon")]
    public List<ItemData> weaponCouldBeEquiped = new List<ItemData>();

    public Vector2 basicWeaponProbaility;
    public Vector2 RareWeaponProbaility;
    public Vector2 EpicWeaponProbaility;
    public Vector2 LegendaryWeaponProbaility;
    public Vector2 UniqueWeaponProbaility;
}