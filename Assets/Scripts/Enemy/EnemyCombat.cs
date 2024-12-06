using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;
    private EnemyStatus enemyStatus;

    private bool canAttack = true;
    private bool inAttack;

    private float delayAttack;
    private float attackCooldown;

    private int inCombo = 0;
    private int combo = 0;
    private int leghtCombo;

    public EnemyCombat(Rigidbody2D rb, EnemyMovement enemyMovement, EnemyStatus enemyStatus, int leghtCombo)
    {
        this.rb = rb;
        this.enemyMovement = enemyMovement;
        this.enemyStatus = enemyStatus;
        this.leghtCombo = leghtCombo;
    }

    public bool CanAttack
    {
        get { return canAttack; }
        set { canAttack = value; }
    }

    public int Combo
    {
        get { return combo; }
        set { combo = value; }
    }

    public int InCombo
    {
        get { return inCombo; }
        set { inCombo = value; }
    }

    public void AttackLogic()
    {
        /*if (inAttack)
        {
            if (delayAttack >= 0.01f)
            {
                delayAttack = 0;
                inAttack = false;
                attack1 = true;
            }
            else
                delayAttack += 1 * Time.deltaTime;
        }*/

        /*if (!canAttack && !inAttack)
        {
            if (attackCooldown >= 0.5f)
            {
                attackCooldown = 0;
                canAttack = true;
            }
            else
                attackCooldown += 1 * Time.deltaTime;
        }*/
        if (canAttack && enemyMovement.Distance < 2 && enemyMovement.Distance != 0)
        {
            enemyMovement.NavMeshAgent.velocity = Vector2.zero;
            rb.velocity = Vector2.zero;
            enemyMovement.CanMove = false;
            enemyMovement.NavMeshAgent.isStopped = true;
            canAttack = false;
            if (inCombo < leghtCombo)
                inCombo++;
            combo = inCombo;
            //inAttack = true;
        }
        else if (!canAttack && enemyMovement.Distance > 2)
        {
            ResetAttack();
        }
    }

    public void SetCombo()
    {
        inCombo = combo;
        combo = 0;
    }


    public void ResetAttack()
    {
        inCombo = 0;
        attackCooldown = 0;
        delayAttack = 0;
        enemyMovement.CanMove = true;
        enemyMovement.NavMeshAgent.velocity = Vector2.zero;
        enemyMovement.NavMeshAgent.isStopped = false;
        inAttack = false;
    }

    public void ResetStatus()
    {
        inCombo = 0;
        attackCooldown = 0;
        delayAttack = 0;
        enemyMovement.CanMove = true;
        enemyMovement.NavMeshAgent.isStopped = false;
        canAttack = true;
        inAttack = false;
    }

    public void InStun()
    {
        enemyMovement.NavMeshAgent.velocity = Vector2.zero;
        rb.velocity = Vector2.zero;
        attackCooldown = 0;
        delayAttack = 0;
        enemyMovement.CanMove = false;
        enemyMovement.NavMeshAgent.isStopped = true;
        canAttack = false;
        inAttack = false;
        enemyStatus.InHurt = false;
    }
}
