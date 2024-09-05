using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;
    private EnemyStatus enemyStatus;

    private bool canAttack = true;

    public EnemyCombat(Rigidbody2D rb, EnemyMovement enemyMovement, EnemyStatus enemyStatus)
    {
        this.rb = rb;
        this.enemyMovement = enemyMovement;
        this.enemyStatus = enemyStatus;
    }

    public void ResetStatus()
    {
        enemyMovement.CanMove = true;
        canAttack = true;
    }

    public void InStun()
    {
        rb.velocity = Vector2.zero;
        enemyMovement.CanMove = false;
        canAttack = false;
        enemyStatus.InHurt = false;
    }
}
