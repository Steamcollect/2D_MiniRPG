using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector] public int maxHealth;
    [HideInInspector] public int currentHealth;

    public Transform centerPoint;
    public GameObject hitEffect;

    Rigidbody2D rb;
    AiEnemyMovement movement;
    EnemySetup stats;
    public Animator animator;

    bool isInvincible = false;

    public bool isPropulsed;

    Vector2 knowbackDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<EnemySetup>();
        movement = GetComponent<AiEnemyMovement>();
    }

    private void Update()
    {
        if (isPropulsed) PropulsedCorps();
    }

    public void TakeDamage(int damageTaken, Vector2 attackPos, int knowbackForce, CurrentWeapon attackantWeapon)
    {
        if (isInvincible) return;

        currentHealth -= damageTaken;
        StartCoroutine(CantMoveCouldown());

        //transforme position attaquant en angle
        Vector2 direction = (attackPos - (Vector2)centerPoint.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180;

        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.Euler(0, 0, angle));
        Destroy(effect, 5);

        if (currentHealth <= 0)
        {
            switch (attackantWeapon)
            {
                case CurrentWeapon.Default:
                    StartCoroutine(Die());
                    print("Basic death");
                    break;

                case CurrentWeapon.SpiritSword:
                    isPropulsed = true;
                    knowbackDirection = direction;
                    isInvincible = true;
                    movement.enabled = false;
                    print("Spirit sword death");
                    break;
            }

            return;
        }        

        rb.AddForce((direction.normalized * -1) * knowbackForce, ForceMode2D.Force);

        animator.SetTrigger("TakeDamage");
        print("damage");

        StartCoroutine(InvincibilityDelay());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(.2f);

        Destroy(gameObject);
    }

    IEnumerator InvincibilityDelay()
    {
        isInvincible = true;

        yield return new WaitForSeconds(.05f);

        isInvincible = false;
    }

    IEnumerator CantMoveCouldown()
    {
        stats.SetMoveSpeed(false);

        yield return new WaitForSeconds(.5f);

        stats.SetMoveSpeed(true);
    }

    void PropulsedCorps()
    {
        rb.velocity = (knowbackDirection.normalized * -1) * 15;
        //rb.AddForce((knowbackDirection.normalized * -1) * 10, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wall")) && isPropulsed)
        {
            StartCoroutine(Die());
        }
    }
}
