using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySetup : MonoBehaviour
{
    public EnemyData currentEnemy;
    public EnemyWeaponParent weaponParent;
    public Transform weaponContent;
 
    public LayerMask targetLayer;

    ItemData currentWeapon;
    GameObject currentWeaponGO;
    List<ItemData> weaponCouldBeEquiped;

    NavMeshAgent agent;
    EnemyHealth health;
    EnemyCombat combat;
    AiEnemyMovement movement;

    ItemRarity raritySelected;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        combat = GetComponent<EnemyCombat>();
        movement = GetComponent<AiEnemyMovement>();
    }

    private void Start()
    {
        // set movement
        agent.speed = currentEnemy.moveSpeed;

        // set health
        health.maxHealth = currentEnemy.maxHealth;
        health.currentHealth = health.maxHealth;

        if (currentEnemy.weaponCouldBeEquiped.Count == 0) return;

        // set weapon
        weaponCouldBeEquiped = new List<ItemData>(currentEnemy.weaponCouldBeEquiped);
        SetWeapon();
    }

    public void SetMoveSpeed(bool canMove)
    {
        if(canMove) agent.speed = currentEnemy.moveSpeed;
        else agent.speed = 0;
    }

    void SetWeapon()
    {
        // set random weapon
        float randomNumber = Random.value * 100;
        print(randomNumber);
        SetWeaponList(randomNumber);
        currentWeapon = weaponCouldBeEquiped[Random.Range(0, weaponCouldBeEquiped.Count)];

        //Instancier la nouvelle arme
        GameObject newWeapon = Instantiate(currentWeapon.weaponPrefabs, weaponContent);
        newWeapon.transform.position = weaponContent.position;
        currentWeaponGO = newWeapon;

        // reference script with current weapon
        weaponParent.currentWeaponGraphics = currentWeaponGO.GetComponent<SpriteRenderer>();
        WeaponScript weaponScript = currentWeaponGO.GetComponent<WeaponScript>();
        weaponScript.parentType = characterType.Monster;
        weaponScript.targetLayer = targetLayer;

        combat.currentWeaponData = currentWeapon;
        combat.currentWeapon = weaponScript;
        combat.currentWeaponAnim = currentWeaponGO.GetComponent<Animator>();

        movement.attackRange = weaponScript.attackRange + .2f;
    }

    void SetWeaponList(float randomNumber)
    {
        // set rarity selected to the good value
        if (randomNumber < currentEnemy.UniqueWeaponProbaility.y) raritySelected = ItemRarity.Unique;
        else if (randomNumber < currentEnemy.LegendaryWeaponProbaility.y) raritySelected = ItemRarity.Legendary;
        else if (randomNumber < currentEnemy.EpicWeaponProbaility.y) raritySelected = ItemRarity.Epic;
        else if (randomNumber < currentEnemy.RareWeaponProbaility.y) raritySelected = ItemRarity.Rare;
        else if (randomNumber < currentEnemy.basicWeaponProbaility.y) raritySelected = ItemRarity.Basic;

        // set the list only with the weapon rarity selected
        for (int i = 0; i < weaponCouldBeEquiped.Count; i++)
        {
            if (weaponCouldBeEquiped[i].rarity != raritySelected) weaponCouldBeEquiped.Remove(weaponCouldBeEquiped[i]);
        }
    }
}