using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private InputsPlayers inputsPlayers;

    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;
    private PlayerStatus playerStatus;
    private PlayerAnimation playerAnimation;
    private PlayerHurtbox playerHurtbox;

    private PlayerHUD playerHUD;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        inputsPlayers = GetComponentInChildren<InputsPlayers>();

        playerHurtbox = GetComponentInChildren<PlayerHurtbox>();

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();

        playerHUD = GetComponent<PlayerHUD>();

        playerMovement = new PlayerMovement(gameObject, rb, inputsPlayers);
        playerCombat = new PlayerCombat(playerMovement, rb, inputsPlayers);
        playerStatus = new PlayerStatus(gameObject);
        playerAnimation = new PlayerAnimation(animator, spriteRenderer, playerMovement, playerCombat, inputsPlayers);

        playerHurtbox.SetStatus(playerStatus);
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.CheckGround();
        playerMovement.JumpLogic();
        playerCombat.AttackLogic();
        playerStatus.DeathLogic();
        playerHUD.SetHUD(playerStatus);
        playerAnimation.FlipLogic();
        playerAnimation.AnimationLogic();
    }

    private void FixedUpdate()
    {
        playerMovement.MoveLogic();
    }

    public PlayerStatus PlayerStatus
    { 
        get { return playerStatus; }
        set { playerStatus = value; }
    }

    public void ResetAttack()
    {
        playerMovement.CanMove = true;
        playerCombat.InAttack = false;
        playerCombat.CanAttack = true;
    }
}
