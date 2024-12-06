using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private GameObject player;
    private InputsPlayers inputsPlayers;
    private Rigidbody2D rb;
    private Knockback knockback;

    private Vector2 direction;

    private float speed = 8;
    private float curretSpeed = 4;

    private float speedDash;

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

    private float distanceDash;

    public PlayerMovement(GameObject player, Rigidbody2D rb, Knockback knockback, InputsPlayers inputsPlayers, float speedDash, float distanceDash)
    {
        this.player = player;
        this.rb = rb;
        this.knockback = knockback;
        this.inputsPlayers = inputsPlayers;
        this.speedDash = speedDash;
        this.distanceDash = distanceDash;
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

    public float DirectionDash
    { 
        get { return directionDash; } 
        set {   directionDash = value; }
    }

    public float SpeedDash
    {
        get { return speedDash; }
        set { speedDash = value; }
    }

    public float DistanceDash
    {
        get { return distanceDash; }
        set { distanceDash = value; }
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

        if (knockback.KnockbackCountLogic < 0)
        {
            if (canMove)
            {
                if (direction != Vector2.zero)
                {
                    if (curretSpeed < speed)
                    {
                        curretSpeed += 1 * Time.deltaTime;
                    }
                    else if (curretSpeed >= speed)
                    {
                        curretSpeed = speed;
                    }
                }
                else
                    curretSpeed = 4;

                rb.velocity = direction * curretSpeed;
            }
        }
        else
            knockback.KnockLogic();
        //else if (inJump)
        //rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }

    public void JumpLogic()
    {
        if(player.transform.position.y == ground)
        {
            print("InGround");
        }

        if (inputsPlayers.Button3 && player.transform.position.y == ground && !inJump)
        {
            print("jump");
            inputsPlayers.Button3 = false;
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
            if (delayDash >= distanceDash)
            {
                delayDash = 0;
                rb.velocity = Vector2.zero;
                inDash = false;
                canMove = true;
            }
            else
                delayDash += 1 * Time.deltaTime;

            rb.velocity = new Vector2(directionDash * speedDash, 0);
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
                directionDash = -1f;
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
                directionDash = 1f;
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
