using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD.MinMaxSlider;

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

    [MinMaxSlider(0, 100)] public Vector2 basicWeaponProbaility;
    [MinMaxSlider(0, 100)] public Vector2 RareWeaponProbaility;
    [MinMaxSlider(0, 100)] public Vector2 EpicWeaponProbaility;
    [MinMaxSlider(0, 100)] public Vector2 LegendaryWeaponProbaility;
    [MinMaxSlider(0, 100)] public Vector2 UniqueWeaponProbaility;
}