using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;
    private InputsPlayers inputsPlayers;
    
    public PlayerAnimation(Animator animator, SpriteRenderer spriteRenderer, PlayerMovement playerMovement, PlayerCombat playerCombat, InputsPlayers inputsPlayers)
    {
        this.animator = animator;
        this.spriteRenderer = spriteRenderer;
        this.playerMovement = playerMovement;
        this.playerCombat = playerCombat;
        this.inputsPlayers = inputsPlayers;
    }

    public void FlipLogic()
    {
        if (inputsPlayers.MoveDirection.x > 0 && !playerMovement.InJump)
            spriteRenderer.transform.localScale = new Vector2(1, spriteRenderer.transform.localScale.y);
        else if (inputsPlayers.MoveDirection.x < 0 && !playerMovement.InJump)
            spriteRenderer.transform.localScale = new Vector2(-1, spriteRenderer.transform.localScale.y);
    }

    public void AnimationLogic()
    {
        animator.SetFloat("Horizontal", inputsPlayers.MoveDirection.x);
        animator.SetFloat("Vertical", inputsPlayers.MoveDirection.y);
        animator.SetBool("InJump", playerMovement.InJump);
        animator.SetBool("InAttack", playerCombat.InAttack);
    }
}