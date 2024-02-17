using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat : MonoBehaviour
{
    public bool canAttack = true;
    public bool isAttacking = false;

    int currentAttack;

    public EnemyData currentEnemyData;
    public EnemyWeaponParent weaponParent;

    public Transform centerPoint;

    [HideInInspector] public ItemData currentWeaponData;
    [HideInInspector] public WeaponScript currentWeapon;

    [HideInInspector] public Animator currentWeaponAnim;

    //Animator characterAnim;
    public SpriteRenderer graphics;
    Rigidbody2D rb;

    float currentAnimationTime;

    public Vector2 attackDashInput;

    private void Awake()
    {
        //characterAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Attack()
    {
        // Set the attack dash
        if (currentWeaponData.dashAtAttack)
        {
            AttackDash();
            //anim.SetTrigger("Attack");
            PlayerMovement.instance.canMove = false;
        }

        // take the current attack time
        currentAnimationTime = GetCurrentAnimationInfo();

        currentAttack++;

        isAttacking = true;

        SetRotation(false);
        StartCoroutine(AttackTimer());

        // Set the animation of the weapon
        currentWeaponAnim.SetTrigger("Attack");
    }

    void AttackCombo()
    {
        float randomValue = Random.value;
        if (currentAttack < currentWeaponData.comboMax && randomValue < .6f)
        {
            // take the current attack time
            GetCurrentAnimationInfo();

            // Set the animation of the player and the weapon
            currentWeaponAnim.SetTrigger("Attack");

            // Set the attack dash
            if (currentWeaponData.dashAtAttack)
            {
                AttackDash();
                //characterAnim.SetTrigger("Attack");
            }

            currentAttack++;

            isAttacking = true;

            StartCoroutine(AttackTimer());
        }
        else
        {
            currentWeaponAnim.SetTrigger("StopAttacking");
            StartCoroutine(AttackCouldown());

            PlayerMovement.instance.canMove = true;

            isAttacking = false;

            SetRotation(true);
        }
    }


    IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(currentAnimationTime);

        AttackCombo();
    }

    IEnumerator AttackCouldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(currentEnemyData.hability);

        currentAttack = 0;
        canAttack = true;
    }


    float GetCurrentAnimationInfo()
    {
        return currentWeapon.attackTime / 60 * 100;
    }

    void AttackDash()
    {
        attackDashInput = (Vector2)currentWeapon.transform.position - (Vector2)centerPoint.position;

        rb.AddForce((attackDashInput.normalized) * currentWeaponData.dashAtAttackForce / 300, ForceMode2D.Force);

        if (attackDashInput.x < -.1f) graphics.flipX = true;
        else if (attackDashInput.x > .1f) graphics.flipX = false;
    }

    public void SetRotation(bool canRotate)
    {
        weaponParent.enabled = canRotate;
    }
}
