using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private InputsPlayers inputsPlayers;

    private bool canAttack = true;
    private bool inAttack;

    public PlayerCombat(PlayerMovement playerMovement, Rigidbody2D rb, InputsPlayers inputsPlayers)
    {
        this.playerMovement = playerMovement;
        this.rb = rb;
        this.inputsPlayers = inputsPlayers;
    }

    public bool CanAttack
    {
        get { return canAttack; }
        set { canAttack = value; }
    }

    public bool InAttack
    {
        get { return  inAttack; }
        set { inAttack = value; }
    }

    public void AttackLogic()
    {
        if (inputsPlayers.Button3 && canAttack && !playerMovement.InJump)
        {
            inputsPlayers.Button3 = false;
            rb.velocity = Vector2.zero;
            playerMovement.CanMove = false;
            canAttack = false;
            inAttack = true;
        }
    }
}
