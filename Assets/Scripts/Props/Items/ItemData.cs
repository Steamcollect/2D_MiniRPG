using UnityEngine;

[CreateAssetMenu(fileName = "Item data", menuName = "Props/Item data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea] public string itemDescription;
    public Sprite itemIcone;

    public ItemRarity rarity;

    [Header("")]
    public GameObject itemPrefabs;

    [Header("")]
    public ItemType itemType;
    public bool isStackable;

    [Header("Consomable")]
    public ConsomableType[] consomableType;

    [Header("Weapon")]
    public WeaponType weaponType;
    public AttackType attackType;
    public CurrentWeapon currentWeapon;

    [Header("")]
    public int attackDamage;
    public int knowbackForce;
    public int comboMax;

    public float bulletSpeed;

    [Header("")]
    public bool dashAtAttack;
    public int dashAtAttackForce;

    [Header("")]
    public float camShakeIntensityAtAttack;
    public float camShakeTimeAtAttack;

    [Header("")]
    public GameObject weaponPrefabs;
    public GameObject bulletPrefabs;

    [Header("Equipment")]
    public EquipmentType equipmentType;

    [Header("References")]
    public int damageGiven;
    public int healthGiven;
}

// what kind of item
public enum ItemType
{
    Consomable,
    Weapon,
    Equipment,
    Artifact
}

public enum ItemRarity
{
    Basic,
    Rare,
    Epic,
    Legendary,
    Unique
}

public enum CurrentWeapon
{
    Default,
    SpiritSword,
}

// what bonnus given
public enum ConsomableType
{
    Health,
    AttackDamage
}

// what type of equipment
public enum EquipmentType
{
    Helmet,
    Plate,
    Feet
}
public enum WeaponType
{
    Magique,
    Physique,
}

// what kind of attack
public enum AttackType
{
    Melee,
    Range,
}