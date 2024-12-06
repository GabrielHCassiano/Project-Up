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

    private float holdInput;

    private bool heavyAttack;
    private bool specialAttack;

    private int combo = 0;
    private int inCombo = 0;
    private bool canCombo = true;

    public PlayerCombat(PlayerMovement playerMovement, PlayerStatus playerStatus, Rigidbody2D rb ,InputsPlayers inputsPlayers)
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

    public float HoldInput
    {
        get { return holdInput; }
        set { holdInput = value; }
    }

    public bool HeavyAttack
    {
        get { return heavyAttack; }
        set { heavyAttack = value; }
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

    public int InCombo
    {
        get { return inCombo; }
        set { inCombo = value; }
    }

    public void AttackLogic()
    {
        if (inputsPlayers.Button4 && canAttack && canCombo && !playerMovement.InJump && !playerMovement.InDash && !heavyAttack && !specialAttack)
        {
            inputsPlayers.Button4 = false;
            rb.velocity = Vector2.zero;
            playerMovement.CanMove = false;
            playerMovement.CanDash = false;
            canAttack = false;
            if (inCombo < 3)
                inCombo++;
            combo = inCombo;
        }
    }

    public void HeavyAttackLogic()
    {
        if (!inputsPlayers.Button4Hold && holdInput > 1 && canAttack && combo == 0 && inCombo == 0 && !playerMovement.InJump && !playerMovement.InDash && !specialAttack)
        {
            rb.velocity = Vector2.zero;
            playerMovement.CanMove = false;
            playerMovement.CanDash = false;
            canAttack = false;
            heavyAttack = true;
        }

        if (inputsPlayers.Button4Hold && canAttack && !playerMovement.InJump)
        {
            holdInput += 1 * Time.deltaTime;
        }
        else
        {
            holdInput = 0;
        }
    }

    public void SpecialAttackLogic()
    {
        if (inputsPlayers.Button2 && canAttack && combo == 0 && inCombo == 0 && !playerMovement.InJump && playerStatus.Stamina >= 25 && !playerMovement.InDash && !heavyAttack)
        {
            inputsPlayers.Button2 = false;
            rb.velocity = Vector2.zero;
            playerMovement.CanMove = false;
            playerMovement.CanDash = false;
            canAttack = false;
            playerStatus.Stamina -= 25;
            specialAttack = true;
        }
    }

    public void ReturnCombo()
    {
        canCombo = false;
    }

    public void SetCombo()
    {
        inCombo = combo;
        combo = 0;
    }

    public void ResetStatus()
    {
        canCombo = true;
        combo = 0;
        inCombo = 0;
        playerMovement.CanMove = true;
        canAttack = true;
        heavyAttack = false;
        specialAttack = false;
        playerStatus.SetIten = true;
    }

    public void InStun()
    {
        rb.velocity = Vector2.zero;
        playerMovement.CanMove = false;
        canAttack = false;
        playerStatus.InHurt = false;
    }
}
