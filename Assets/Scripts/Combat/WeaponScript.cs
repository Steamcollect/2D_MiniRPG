using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public ItemData currentWeapon;

    public float attackRange;
    public float attackTime;

    [HideInInspector] public EnemyData currentEnemyData;

    public Transform attackPoint;

    Transform playerPos;

    public characterType parentType;
    public LayerMask targetLayer;

    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Attack()
    {
        switch (currentWeapon.attackType)
        {
            case AttackType.Melee:
                MeleeAttack();
                break;
            case AttackType.Range:
                RangeAttack();
                break;
        }
    }

    void MeleeAttack()
    {
        //recuperer tout les enemy dans la range
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, targetLayer);

        // envoyer les degats aux ennemies touchés
        foreach (Collider2D collid in hit)
        {
            switch (parentType)
            {
                case characterType.Player:

                    // take the enemy health
                    EnemyHealth enemyHealth = collid.gameObject.GetComponent<EnemyHealth>();
                    if (enemyHealth == null) return;

                    if (currentWeapon.weaponType == WeaponType.Physique) enemyHealth.TakeDamage(PlayerStats.instance.strenge + currentWeapon.attackDamage, playerPos.position, currentWeapon.knowbackForce, currentWeapon.currentWeapon);
                    if (currentWeapon.weaponType == WeaponType.Magique) enemyHealth.TakeDamage(PlayerStats.instance.power + currentWeapon.attackDamage, playerPos.position, currentWeapon.knowbackForce, currentWeapon.currentWeapon);
                    
                    break;

                case characterType.Monster:

                    // take the player health
                    PlayerHealth playerHealth = collid.gameObject.GetComponent<PlayerHealth>();
                    if (playerHealth == null) return;

                    if (currentWeapon.weaponType == WeaponType.Physique) playerHealth.TakeDamage(currentEnemyData.strenge + currentWeapon.attackDamage, transform.position, currentWeapon.knowbackForce);
                    if (currentWeapon.weaponType == WeaponType.Magique) playerHealth.TakeDamage(currentEnemyData.power + currentWeapon.attackDamage, transform.position, currentWeapon.knowbackForce);
                    break;
            }
            
        }
    }
    void RangeAttack()
    {
        Bullet currentBullet = Instantiate(currentWeapon.bulletPrefabs, attackPoint.position, attackPoint.rotation).GetComponent<Bullet>();
        currentBullet.speed = currentWeapon.bulletSpeed;
        currentBullet.targetLayer = targetLayer;
        currentBullet.parentType = parentType;
        currentBullet.knowbackForce = currentWeapon.knowbackForce;
        currentBullet.weaponFrom = currentWeapon;

        // set the bullet Damage
        if (currentWeapon.weaponType == WeaponType.Physique) currentBullet.damage = PlayerStats.instance.strenge + currentWeapon.attackDamage;
        if (currentWeapon.weaponType == WeaponType.Magique) currentBullet.damage = PlayerStats.instance.power + currentWeapon.attackDamage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

public enum characterType
{
    Player,
    Monster,
}