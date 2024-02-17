using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ItemData weaponFrom;

    public characterType parentType;
    public LayerMask targetLayer;

    public Transform detectionPoint;
    public float detectionRange;

    public float speed;
    public int damage;
    public int knowbackForce;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // if game is paused, dont move
        if (GameStateManager.instance.CurrentGameState == GameState.Gameplay) rb.velocity = transform.right.normalized * speed * 10 * Time.deltaTime;
        else rb.velocity = new Vector2(0, 0);

        //recuperer tout les enemy dans la range
        Collider2D[] hit = Physics2D.OverlapCircleAll(detectionPoint.position, detectionRange, targetLayer);

        // envoyer les degats aux ennemies touchés
        foreach (Collider2D collid in hit)
        {
            switch (parentType)
            {
                case characterType.Player:

                    EnemyHealth enemyHealth = collid.gameObject.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(damage, transform.position, knowbackForce, weaponFrom.currentWeapon);
                    }
                    break;

                case characterType.Monster:
                    PlayerHealth playerHealth = collid.gameObject.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(damage, transform.position, knowbackForce);
                    }
                    break;
            }

            Destroy(gameObject, 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(detectionPoint.position, detectionRange);
    }
}
