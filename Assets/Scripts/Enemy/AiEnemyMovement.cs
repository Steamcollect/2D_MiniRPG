using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiEnemyMovement : MonoBehaviour
{
    [Header("Patrolling")]
    Vector2 walkPoint;
    bool walkPointSet;
    float walkPointRange = 2f;

    [Header("References")]
    public float detectionRange;
    public float attackRange;

    public Transform centerPoint;

    public LayerMask playerLayer;
    public LayerMask wallLayer;

    bool canMove = true;

    bool isInFollowingRange;
    bool isInAttackRange;

    Transform target;
    NavMeshAgent agent;
    EnemyCombat combat;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<EnemyCombat>();
    }

    private void Update()
    {
        //check if the target is in range
        isInFollowingRange = Vector2.Distance(centerPoint.position, target.position) <= detectionRange;
        isInAttackRange = Vector2.Distance(centerPoint.position, target.position) <= attackRange;

        transform.localRotation = Quaternion.Euler(0, 0, 0);

        // chase player
        if (isInAttackRange)
        {
            canMove = false;

            if(!combat.isAttacking && combat.canAttack)
            {
                combat.Attack();
                agent.SetDestination(transform.position);
            }
        }
        else canMove = true;
        if (canMove)
        {
            if (isInFollowingRange && !isInAttackRange)
            {
                agent.SetDestination(target.position);
                walkPointSet = false;
                return;
            }
        }

        // finf and walk to walk point
        if (!isInFollowingRange && !walkPointSet) SearchNewWalkPoint();
        else if (!isInFollowingRange && walkPointSet) agent.SetDestination(walkPoint);

    }

    void SearchNewWalkPoint()
    {
        walkPoint.x = Random.Range(transform.position.x - walkPointRange, transform.position.x + walkPointRange);
        walkPoint.y = Random.Range(transform.position.y - walkPointRange, transform.position.y + walkPointRange);

        if (!Physics2D.OverlapCircle(walkPoint, .5f, wallLayer))
        {
            walkPointSet = true;
            StartCoroutine(WaitForTheNextWalkPoint());
        }
    }

    IEnumerator WaitForTheNextWalkPoint()
    {
        yield return new WaitForSeconds(Random.Range(2, 8));
        walkPointSet = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(centerPoint.position, detectionRange);
        Gizmos.DrawWireSphere(centerPoint.position, attackRange);
    }
}
