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

    private float delayDash;
    private bool canDash = true;
    private bool inDash;
    private float directionDash = 2;

    private float delayCanDash;
    private float delayInputLeft = 0;
    private float delayInputRight = 0;
    private bool inputDashLeft = false;
    private bool inputDashRight = false;

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

    public float Ground
    {
        get { return ground; }
        set { ground = value; }
    }

    public float DelayDash
    {
        get { return delayDash; }
        set { delayDash = value; }
    }

    public float DelayInputLeft
    {
        get { return delayInputLeft; }
        set { delayInputLeft = value; }
    }

    public float DelayInputRight
    {
        get { return delayInputRight; }
        set { delayInputRight = value; }
    }

    public bool InDash
    {
        get { return inDash; }
        set { inDash = value; }
    }

    public bool CanDash
    {
        get { return canDash; }
        set { canDash = value; }
    }


    public void MoveLogic()
    {

        direction = new Vector2(inputsPlayers.MoveDirection.x, inputsPlayers.MoveDirection.y).normalized;

        if (direction == Vector2.zero)
        {
            rb.mass = 100000;
        }
        else
            rb.mass = 0.00001f;

        if (canMove)
            rb.velocity = direction * speed;
        //else if (inJump)
            //rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }

    public void JumpLogic()
    {
        if(player.transform.position.y == ground)
        {
            print("InGround");
        }

        if (inputsPlayers.Button2 && player.transform.position.y == ground && !inJump)
        {
            print("jump");
            inputsPlayers.Button2 = false;
            canMove = false;
            inJump = true;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 1;
            rb.velocity = new Vector2(direction.x * forceJump, forceJump);
        }
    }

    public void DashLogic()
    {
        if (!canDash && !inDash)
        {
            if (delayCanDash >= 0.5f)
            {
                delayCanDash = 0;
                canDash = true;
            }
            else
                delayCanDash += 1 * Time.deltaTime;
        }

        if (inDash)
        {
            if (delayDash >= 0.3f)
            {
                delayDash = 0;
                rb.velocity = Vector2.zero;
                inDash = false;
                canMove = true;
            }
            else
                delayDash += 1 * Time.deltaTime;

            rb.velocity = new Vector2(directionDash * speed, 0);
        }

        if (inputDashLeft)
        {
            if (delayInputLeft >= 0.3f)
            {
                delayInputLeft = 0;
                inputDashLeft = false;
            }
            else
                delayInputLeft += 1 * Time.deltaTime;
        }
        if (inputsPlayers.ButtonDashLeft && !inDash && canDash)
        {
            inputsPlayers.ButtonDashLeft = false;
            if (inputDashLeft && !inDash)
            {
                canMove = false;
                directionDash = -2;
                inDash = true;
                canDash = false;
                inputDashLeft = false;
            }
            else
            {
                inputDashLeft = true;
            }
        }
        if (inputDashRight)
        {
            if (delayInputRight >= 0.5f)
            {
                delayInputRight = 0;
                inputDashRight = false;
            }
            else
                delayInputRight += 1 * Time.deltaTime;
        }
        if (inputsPlayers.ButtonDashRight && !inDash && canDash)
        {
            inputsPlayers.ButtonDashRight = false;
            if (inputDashRight && !inDash)
            {
                canMove = false;
                directionDash = 2;
                inDash = true;
                canDash = false;
                inputDashRight = false;
            }
            else
            {
                inputDashRight = true;
            }
        }
    }

    public void ResetDash()
    {
        delayDash = 0;
        delayCanDash = 0;
        delayInputLeft = 0;
        delayInputRight = 0;
        inDash = false;
        canDash = true;
        inputDashLeft = false;
        inputDashRight = false;
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
