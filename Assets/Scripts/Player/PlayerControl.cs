using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private int idPlayer;

    private InputsPlayers inputsPlayers;

    private PlayerMovement playerMovement;
    private PlayerStatus playerStatus;
    private PlayerCombat playerCombat;
    private PlayerAnimation playerAnimation;
    private PlayerHurtbox playerHurtbox;

    private PlayerHUD playerHUD;

    private Rigidbody2D rb;
    private Knockback knockback;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField] private AudioSource[] audios;

    [SerializeField] private float ds;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        playerHurtbox = GetComponentInChildren<PlayerHurtbox>();

        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();

        playerHUD = GetComponent<PlayerHUD>();

        playerHUD.SetPlayerData(inputsPlayers.PlayerData);
        playerHUD.SetPlayerUI();
        animator.runtimeAnimatorController = inputsPlayers.PlayerData.AnimatorController;

        switch (inputsPlayers.PlayerData.CharName)
        {
            case "Suni":
                playerMovement = new PlayerMovement(gameObject, rb, knockback, inputsPlayers, 16, 0.45f);
                break;
            case "Alu":
                playerMovement = new PlayerMovement(gameObject, rb, knockback, inputsPlayers, 12, 0.5f);
                break;
        }
        playerStatus = new PlayerStatus(this, spriteRenderer, knockback, inputsPlayers);
        playerCombat = new PlayerCombat(playerMovement, playerStatus, rb, inputsPlayers);
        playerAnimation = new PlayerAnimation(animator, spriteRenderer, playerMovement, playerStatus, playerCombat, inputsPlayers);

        playerHurtbox.SetStatus(playerStatus);
    }

    // Update is called once per frame
    void Update()
    {
        //playerMovement.SpeedDash = speed;
        //playerMovement.DistanceDash = ds;


        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + 1.0f, 1), new Vector3(transform.position.x, transform.position.y - 1.0f, 1), Color.red);
        playerMovement.CheckGround();
        playerMovement.DashLogic();
        //playerMovement.JumpLogic();
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

    public int IdPlayer
    {
        get { return idPlayer; }
        set { idPlayer = value; }
    }

    public InputsPlayers InputsPlayers
    {
        get { return inputsPlayers; }
        set { inputsPlayers = value; }
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
    public SpriteRenderer SpriteRenderer
    {
        get { return spriteRenderer; }
        set { spriteRenderer = value; }
    }


    public void GetIten()
    {
        rb.velocity = Vector2.zero;
        playerMovement.CanMove = false;
        playerCombat.CanAttack = false;
        playerStatus.GetIten = false;
        playerMovement.ResetDash();
    }

    public void MoveChar(float speed)
    {
        rb.velocity = new Vector2(speed * spriteRenderer.transform.localScale.x, rb.velocity.y);
    }

    public void MoveZeroChar()
    {
        rb.velocity = Vector2.zero;
    }

    public void SetIten()
    {
        playerStatus.SetIten = true;
    }

    public void SetForce(int force)
    {
        playerStatus.Force = force;
    }

    public void ReturnCombo()
    {
        playerCombat.ReturnCombo();
    }

    public void SetCombo()
    {
        playerCombat.SetCombo();
        //playerMovement.ResetDash();
    }

    public void ResetAttack()
    {
        playerCombat.ResetStatus();
        playerMovement.ResetDash();
    }

    public void InStun()
    {
        playerCombat.InStun();
        playerMovement.ResetDash();
        playerHUD.CancelHit();
    }

    public void AttackSound(int id)
    {
        audios[id].Play();
    }

    public void ItenSound()
    {
        audios[3].Play();
    }
}
