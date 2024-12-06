using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;
    private EnemyStatus enemyStatus;
    private EnemyCombat enemyCombat;

    public EnemyAnimation(Animator animator, SpriteRenderer spriteRenderer, Rigidbody2D rb,  EnemyMovement enemyMovement, EnemyStatus enemyStatus, EnemyCombat enemyCombat)
    {
        this.animator = animator;
        this.spriteRenderer = spriteRenderer;
        this.rb = rb;
        this.enemyMovement = enemyMovement;
        this.enemyStatus = enemyStatus;
        this.enemyCombat = enemyCombat;
    }

    public void FlipLogic()
    {
        if (enemyMovement.DirectionEnemy.x > 0 && !enemyStatus.InHurt)
            spriteRenderer.transform.localScale = new Vector2(1, spriteRenderer.transform.localScale.y);
        else if (enemyMovement.DirectionEnemy.x < 0 && !enemyStatus.InHurt)
            spriteRenderer.transform.localScale = new Vector2(-1, spriteRenderer.transform.localScale.y);
    }

    public void AnimationLogic()
    {
        animator.SetFloat("Horizontal", enemyMovement.NavMeshAgent.velocity.x);
        animator.SetFloat("Vertical", enemyMovement.NavMeshAgent.velocity.y);
        animator.SetBool("InHurt", enemyStatus.InHurt);
        animator.SetBool("Intro", enemyMovement.InSpawn);
        animator.SetTrigger("Attack" + enemyCombat.Combo);
        animator.SetBool("Dead", enemyStatus.Death);
    }
}
