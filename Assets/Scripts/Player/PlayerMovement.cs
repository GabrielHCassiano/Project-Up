using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject player;
    private InputsPlayers inputsPlayers;
    private Rigidbody2D rb;

    private Vector2 direction;

    private float speed = 8;
    private float forceJump = 8;

    private bool canMove = true;
    private bool inJump = false;
    private float ground;

    public PlayerMovement(GameObject player, Rigidbody2D rb, InputsPlayers inputsPlayers)
    {
        this.player = player;
        this.rb = rb;
        this.inputsPlayers = inputsPlayers;
    }

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    public bool InJump
    {
        get { return inJump; }
        set { inJump = value; }
    }

    public void MoveLogic()
    {
        direction = new Vector2(inputsPlayers.MoveDirection.x, inputsPlayers.MoveDirection.y).normalized;

        if (canMove)
            rb.velocity = direction * speed;
        //else if (inJump)
        //    rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }

    public void JumpLogic()
    {
        if (inputsPlayers.Button4 && player.transform.position.y == ground && !inJump)
        {
            inputsPlayers.Button4 = false;
            canMove = false;
            inJump = true;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 1;
            rb.velocity = new Vector2(direction.x * forceJump, forceJump);
        }
    }

    public void DashLogic()
    {
        if (inputsPlayers.MoveDirection.x > 0)
        {

        }
    }

    public void CheckGround()
    {
        if (!inJump)
            ground = player.transform.position.y;
        else if (inJump && player.transform.position.y <= ground)
        {
            canMove = true;
            inJump = false;
            rb.gravityScale = 0;
        }
    }
}
