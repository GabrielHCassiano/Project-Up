using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerStatus playerStatus;
    private Rigidbody2D rb;
    private InputsPlayers inputsPlayers;

    private bool canAttack = true;
    private bool specialAttack;

    private int combo = 0;
    private int inCombo = 0;

    public PlayerCombat(PlayerMovement playerMovement, PlayerStatus playerStatus, Rigidbody2D rb, InputsPlayers inputsPlayers)
    {
        this.playerMovement = playerMovement;
        this.playerStatus = playerStatus;
        this.rb = rb;
        this.inputsPlayers = inputsPlayers;
    }

    public bool CanAttack
    {
        get { return canAttack; }
        set { canAttack = value; }
    }

    public bool SpecialAttack
    {
        get { return specialAttack; }
        set { specialAttack = value; }
    }

    public int Combo
    {
        get { return combo; }
        set { combo = value; }
    }

    public void AttackLogic()
    {
        if (inputsPlayers.Button3 && canAttack && !playerMovement.InJump)
        {
            inputsPlayers.Button3 = false;
            rb.velocity = Vector2.zero;
            playerMovement.CanMove = false;
            canAttack = false;
            inCombo++;
            combo = inCombo;
        }
    }

    public void SpecialAttackLogic()
    {
        if (inputsPlayers.Button4 && canAttack && !playerMovement.InJump && playerStatus.Stamina > 50)
        {
            inputsPlayers.Button4 = false;
            rb.velocity = Vector2.zero;
            playerMovement.CanMove = false;
            canAttack = false;
            playerStatus.Stamina -= 50;
            specialAttack = true;
        }
    }

    public void SetCombo()
    {
        inCombo = combo;
        combo = 0;
    }

    public void ResetStatus()
    {
        inCombo = 0;
        playerMovement.CanMove = true;
        canAttack = true;
        specialAttack = false;
    }

    public void InStun()
    {
        rb.velocity = Vector2.zero;
        playerMovement.CanMove = false;
        canAttack = false;
        playerStatus.InHurt = false;
    }
}
