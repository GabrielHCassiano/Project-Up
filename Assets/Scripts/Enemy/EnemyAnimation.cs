using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private EnemyMovement enemyMovement;

    public EnemyAnimation(Animator animator, SpriteRenderer spriteRenderer, EnemyMovement enemyMovement)
    {
        this.animator = animator;
        this.spriteRenderer = spriteRenderer;
        this.enemyMovement = enemyMovement;
    }

    public void FlipLogic()
    {
        if (enemyMovement.Direction.x > 0)
            spriteRenderer.transform.localScale = new Vector2(1, spriteRenderer.transform.localScale.y);
        else if (enemyMovement.Direction.x < 0)
            spriteRenderer.transform.localScale = new Vector2(-1, spriteRenderer.transform.localScale.y);
    }

    public void AnimationLogic()
    {
        animator.SetFloat("Horizontal", enemyMovement.Direction.x);
        animator.SetFloat("Vertical", enemyMovement.Direction.y);
    }
}
