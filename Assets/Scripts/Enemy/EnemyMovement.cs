using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private GameObject enemy;
    private GameObject[] players;

    private float speed = 2;

    private Vector3 direction;
    private float distance;

    private float ground;

    private bool canMove = true;
    private bool lockMove;
    private int idLockPlayer = 0;

    private bool inSpawn = true;

    public EnemyMovement(Rigidbody2D rb,GameObject enemy, GameObject[] players)
    {
        this.rb = rb;
        this.enemy = enemy;
        this.players = players;
    }

    public Vector2 Direction
    { 
        get { return direction; }
        set { direction = value; }
    }

    public float Distance
    {
        get { return distance; }
        set { distance = value; }
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
        idLockPlayer = Random.Range(0, players.Length);
        inSpawn = false;
        canMove = false;
    }

    public void MoveLogic()
    {
        direction = players[idLockPlayer].transform.position - enemy.transform.position;  
        direction = direction.normalized;

        if (canMove && lockMove && players[idLockPlayer].GetComponentInChildren<SpriteRenderer>().gameObject.activeSelf)
        {
            rb.velocity = direction * speed;
        }
    }

    public void LockPlayer()
    {
        for (int i = 0; i < players.Length; i++)
        {
            float distanceCurrent = Vector2.Distance(players[idLockPlayer].transform.position, enemy.transform.position);
            float newDistance = Vector2.Distance(players[i].transform.position, enemy.transform.position);
            if (newDistance < distanceCurrent)
            {
                idLockPlayer = i;
                distanceCurrent = newDistance;
            }
            distance = distanceCurrent;
        }
        if (distance < 10 || distance > 20)
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
