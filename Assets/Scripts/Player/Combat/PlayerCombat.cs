using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [HideInInspector] public bool canAttack = true;
    bool canClickToAttack = true;
    bool haveClickToAttack = false;
    bool isAttacking = false;

    int currentAttack;

    [HideInInspector] public WeaponScript currentWeapon;
    [HideInInspector] public Animator currentWeaponAnimator;

    public WeaponParent weaponParent;
    [HideInInspector] public ItemData currentWeaponData;

    public Transform centerPoint;

    Rigidbody2D rb;
    SpriteRenderer graphics;
    Animator anim;
    PlayerStats stats;

    Vector2 attackDashInput;

    float currentAnimationTime;

    public static PlayerCombat instance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        graphics = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
        
        instance = this;
    }

    private void Update()
    {
        //Si peut attaquer et que jeu pas en pause
        if (canAttack && GameStateManager.instance.CurrentGameState == GameState.Gameplay)
        {
            //Si peut cliquer et clique
            if (Input.GetKeyDown(KeyCode.Mouse0) && canClickToAttack) haveClickToAttack = true;

            if (haveClickToAttack && !isAttacking) Attack();
        }
    }

    void Attack()
    {
        // Set the attack dash
        if (currentWeaponData.dashAtAttack)
        {
            AttackDash();
            anim.SetTrigger("Attack");
            PlayerMovement.instance.canMove = false;
        }

        // take the current attack time
        currentAnimationTime = GetCurrentAnimationInfo();

        // set the cam shake
        CameraShake.instance.ShakeCam(currentWeaponData.camShakeIntensityAtAttack, currentWeaponData.camShakeTimeAtAttack);

        currentAttack++;

        isAttacking = true;
        haveClickToAttack = false;
        canClickToAttack = false;

        SetRotation(false);
        StartCoroutine(AttackTimer());

        // Set the animation of the weapon
        currentWeaponAnimator.SetTrigger("Attack");
    }

    void AttackCombo()
    {
        if (haveClickToAttack)
        {
            // take the current attack time
            GetCurrentAnimationInfo();

            // set the cam shake
            CameraShake.instance.ShakeCam(currentWeaponData.camShakeIntensityAtAttack, currentWeaponData.camShakeTimeAtAttack);

            // Set the animation of the player and the weapon
            currentWeaponAnimator.SetTrigger("Attack");

            // Set the attack dash
            if (currentWeaponData.dashAtAttack)
            {
                AttackDash();
                anim.SetTrigger("Attack");
            }

            currentAttack++;

            isAttacking = true;
            haveClickToAttack = false;
            canClickToAttack = false;

            StartCoroutine(AttackTimer());
        }
        else
        {
            currentWeaponAnimator.SetTrigger("StopAttacking");
            StartCoroutine(AttackCouldown());

            PlayerMovement.instance.canMove = true;

            isAttacking = false;
            
            SetRotation(true);
        }
    }

    IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(currentAnimationTime - .3f);

        if (currentAttack < currentWeaponData.comboMax) canClickToAttack = true;

        yield return new WaitForSeconds(.3f);
        canClickToAttack = false;

        AttackCombo();
    }

    IEnumerator AttackCouldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(stats.hability);

        currentAttack = 0;
        canAttack = true;
        canClickToAttack = true;
    }

    void AttackDash()
    {
        attackDashInput = (Vector2)currentWeapon.transform.position - (Vector2)centerPoint.position;

        rb.AddForce((attackDashInput.normalized) * currentWeaponData.dashAtAttackForce * 3000, ForceMode2D.Force);

        if (attackDashInput.x < -.1f) graphics.flipX = true;
        else if (attackDashInput.x > .1f) graphics.flipX = false;
    }

    float GetCurrentAnimationInfo()
    {
        return currentWeapon.attackTime / 60 * 100;
    }

    public void SetRotation(bool canRotate)
    {
        weaponParent.enabled = canRotate;
    }
}