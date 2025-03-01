using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;
    private PlayerStatus playerStatus;
    private InputsPlayers inputsPlayers;
    
    public PlayerAnimation(Animator animator, SpriteRenderer spriteRenderer, PlayerMovement playerMovement, PlayerStatus playerStatus, PlayerCombat playerCombat, InputsPlayers inputsPlayers)
    {
        this.animator = animator;
        this.spriteRenderer = spriteRenderer;
        this.playerMovement = playerMovement;
        this.playerCombat = playerCombat;
        this.playerStatus = playerStatus;
        this.inputsPlayers = inputsPlayers;
    }

    public void FlipLogic()
    {
        if (inputsPlayers.MoveDirection.x > 0 && !playerMovement.InDash && !playerMovement.InJump && playerCombat.InCombo == 0 && !playerCombat.HeavyAttack && !playerCombat.SpecialAttack)
            spriteRenderer.transform.localScale = new Vector2(1, spriteRenderer.transform.localScale.y);
        else if (inputsPlayers.MoveDirection.x < 0 && !playerMovement.InDash && !playerMovement.InJump && playerCombat.InCombo == 0 && !playerCombat.HeavyAttack && !playerCombat.SpecialAttack)
            spriteRenderer.transform.localScale = new Vector2(-1, spriteRenderer.transform.localScale.y);

        if (playerMovement.DirectionDash > 0 && playerMovement.InDash && !playerMovement.InJump && playerCombat.InCombo == 0 && !playerCombat.HeavyAttack && !playerCombat.SpecialAttack)
            spriteRenderer.transform.localScale = new Vector2(1, spriteRenderer.transform.localScale.y);
        else if (playerMovement.DirectionDash < 0 && playerMovement.InDash && !playerMovement.InJump && playerCombat.InCombo == 0 && !playerCombat.HeavyAttack && !playerCombat.SpecialAttack)
            spriteRenderer.transform.localScale = new Vector2(-1, spriteRenderer.transform.localScale.y);
    }

    public void AnimationLogic()
    {
        animator.SetFloat("Horizontal", inputsPlayers.MoveDirection.x);
        animator.SetFloat("Vertical", inputsPlayers.MoveDirection.y);
        animator.SetBool("InDash", playerMovement.InDash);
        animator.SetBool("InJump", playerMovement.InJump);
        animator.SetBool("Death", playerStatus.Death);
        animator.SetBool("InHurt", playerStatus.InHurt);
        animator.SetBool("GetIten", playerStatus.GetIten);
        animator.SetTrigger("Attack" + playerCombat.Combo);
        animator.SetBool("HeavyAttack", playerCombat.HeavyAttack);
        animator.SetBool("SpecialAttack", playerCombat.SpecialAttack);
    }
}
