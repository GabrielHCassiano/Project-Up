using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private InputsPlayers inputsPlayers;

    private PlayerMovement playerMovement;
    private PlayerStatus playerStatus;
    private PlayerCombat playerCombat;
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
        playerStatus = new PlayerStatus(this, spriteRenderer, inputsPlayers);
        playerCombat = new PlayerCombat(playerMovement, playerStatus, rb, inputsPlayers);
        playerAnimation = new PlayerAnimation(animator, spriteRenderer, playerMovement, playerStatus, playerCombat, inputsPlayers);

        playerHurtbox.SetStatus(playerStatus);
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.CheckGround();
        playerMovement.JumpLogic();
        playerCombat.AttackLogic();
        playerCombat.HeavyAttackLogic();
        playerCombat.SpecialAttackLogic();
        playerStatus.StatusBalance();
        playerStatus.DeathLogic();
        playerHUD.SetHUD(playerStatus);
        playerHUD.HeavyAnim(playerCombat, spriteRenderer);
        playerAnimation.FlipLogic();
        playerAnimation.AnimationLogic();
    }

    private void FixedUpdate()
    {
        playerMovement.MoveLogic();
    }

    public PlayerMovement PlayerMovement
    {
        get { return playerMovement; }
        set { playerMovement = value; }
    }

    public PlayerCombat PlayerCombat
    {
        get { return playerCombat; }
        set { playerCombat = value; }
    }

    public PlayerStatus PlayerStatus
    { 
        get { return playerStatus; }
        set { playerStatus = value; }
    }


    public void SetForce(int force)
    {
        playerStatus.Force = force;
    }

    public void SetCombo()
    {
        playerCombat.SetCombo();
    }

    public void ResetAttack()
    {
        playerCombat.ResetStatus();
    }

    public void InStun()
    {
        playerCombat.InStun();
        playerHUD.CancelHit();
    }
}
