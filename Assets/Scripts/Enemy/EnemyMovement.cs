using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private GameObject enemy;
    private GameObject player;

    private float speed = 1;

    private Vector2 direction;

    private bool canMove;

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

    public void MoveLogic()
    {
        direction = player.transform.position - enemy.transform.position;
        direction = direction.normalized;
        rb.velocity = direction * speed;
    }
}
