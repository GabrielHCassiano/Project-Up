using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;
    private EnemyStatus enemyStatus;

    private bool canAttack = true;
    private bool inAttack;
    private bool attack1;

    private float delayAttack;
    private float attackCooldown;

    public EnemyCombat(Rigidbody2D rb, EnemyMovement enemyMovement, EnemyStatus enemyStatus)
    {
        this.rb = rb;
        this.enemyMovement = enemyMovement;
        this.enemyStatus = enemyStatus;
    }

    public bool Attack1
    {
        get { return attack1; }
        set { attack1 = value; }
    }

    public void AttackLogic()
    {
        if (inAttack)
        {
            if (delayAttack >= 0.3f)
            {
                delayAttack = 0;
                inAttack = false;
                attack1 = true;
            }
            else
                delayAttack += 1 * Time.deltaTime;
        }

        if (!canAttack && !inAttack)
        {
            if (attackCooldown >= 0.5f)
            {
                attackCooldown = 0;
                canAttack = true;
            }
            else
                attackCooldown += 1 * Time.deltaTime;
        }
        if (canAttack && enemyMovement.Distance < 3)
        {
            rb.velocity = Vector2.zero;
            enemyMovement.CanMove = false;
            canAttack = false;
            inAttack = true;
        }
        else if (!canAttack && enemyMovement.Distance > 3)
        {
            ResetAttack();
        }
    }

    public void ResetAttack()
    {
        attackCooldown = 0;
        delayAttack = 0;
        enemyMovement.CanMove = true;
        attack1 = false;
        inAttack = false;
    }

    public void ResetStatus()
    {
        attackCooldown = 0;
        delayAttack = 0;
        enemyMovement.CanMove = true;
        canAttack = true;
        inAttack = false;
        attack1 = false;
    }

    public void InStun()
    {
        rb.velocity = Vector2.zero;
        attackCooldown = 0;
        delayAttack = 0;
        enemyMovement.CanMove = false;
        canAttack = false;
        inAttack = false;
        enemyStatus.InHurt = false;
    }
}
