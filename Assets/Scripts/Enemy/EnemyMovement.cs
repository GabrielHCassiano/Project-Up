using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private GameObject enemy;
    private GameObject player;

    private float speed = 0;

    private Vector2 direction;
    private float distance;

    private float ground;

    private bool canMove = true;
    private bool lockMove;

    private bool inSpawn = true;

    public EnemyMovement(Rigidbody2D rb,GameObject enemy, GameObject player)
    {
        this.rb = rb;
        this.enemy = enemy;
        this.player = player;
    }

    public Vector2 Direction
    { 
        get { return direction; }
        set { direction = value; }
    }

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    public bool InSpawn
    {
        get { return inSpawn; }
        set { inSpawn = value; }
    }

    public float Ground
    {
        get { return ground; }
        set { ground = value; }
    }

    public void SpawnLogic()
    {
        inSpawn = false;
        canMove = false;
    }

    public void MoveLogic()
    {
        direction = player.transform.position - enemy.transform.position;
        direction = direction.normalized;

        if (canMove && lockMove && player != null)
        {
            rb.velocity = direction * speed;
        }
    }

    public void LockPlayer()
    {
        distance = Vector2.Distance(player.transform.position, enemy.transform.position);

        if (distance < 10)
        {
            lockMove = true;
        }
        else
        {
            lockMove = false;
            rb.velocity = Vector2.zero;
        }
    }

    public void CheckGround()
    {
        ground = enemy.transform.position.y;
    }
}
