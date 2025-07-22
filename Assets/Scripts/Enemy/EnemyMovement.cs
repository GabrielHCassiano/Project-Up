using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Rigidbody2D rb;
    private Knockback knockback;

    private GameObject enemy;
    private GameObject[] players;

    private float speed = 2;

    private Vector3 direction;
    private Vector3 directionEnemy;
    private float distance;

    private float ground;

    private bool canMove = true;
    private bool lockMove;
    private int idLockPlayer = 0;

    private bool canEnemy = true;

    private bool inSpawn = true;

    public EnemyMovement(NavMeshAgent navMeshAgent, Rigidbody2D rb, Knockback knockback, GameObject enemy, GameObject[] players)
    {
        this.navMeshAgent = navMeshAgent;
        this.rb = rb;
        this.knockback = knockback;
        this.enemy = enemy;
        this.players = players;
    }

    public NavMeshAgent NavMeshAgent
    {
        get { return navMeshAgent; }
        set { navMeshAgent = value; }
    }

    public Vector2 DirectionEnemy
    { 
        get { return directionEnemy; }
        set { directionEnemy = value; }
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
        //canMove = false;
    }

    public void MoveLogic()
    {
        direction = players[idLockPlayer].transform.position;
        directionEnemy = players[idLockPlayer].transform.position - enemy.transform.position;
        //direction = players[idLockPlayer].transform.position - enemy.transform.position;  
        //direction = direction.normalized;

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Alpha9)) 
            CanMove = true;

        if (knockback.KnockbackCountLogic < 0)
        {
            if (canMove && !players[idLockPlayer].GetComponent<PlayerControl>().PlayerStatus.Death)
            {
                /*print("0");
                print("0 - " + canMove);
                print("0 - " + direction);
                print("0 - " + directionEnemy);*/
                if (navMeshAgent.isStopped == true && players.Length == 1)
                {
                    idLockPlayer = 0;
                    navMeshAgent.isStopped = false;
                    canEnemy = true;
                }

                canEnemy = true;
                //rb.velocity = direction * speed;
                navMeshAgent.SetDestination(direction);
                //navMeshAgent.isStopped = true;
            }
            else if(players[idLockPlayer].GetComponent<PlayerControl>().PlayerStatus.Death)
            {
                if (idLockPlayer < players.Length - 1 && players.Length > 1)
                {
                    idLockPlayer++;
                }
                else if (idLockPlayer == players.Length - 1 && players.Length > 1)
                {
                    idLockPlayer = 0;
                }

                if ((players[0].GetComponent<PlayerControl>().PlayerStatus.Death && players.Length == 1) ||
                    (players[0].GetComponent<PlayerControl>().PlayerStatus.Death && players[1].GetComponent<PlayerControl>().PlayerStatus.Death && players.Length == 20))
                {
                    idLockPlayer = 0;
                    navMeshAgent.isStopped = true;
                    navMeshAgent.velocity = Vector2.zero;
                    rb.velocity = Vector2.zero;
                    canEnemy = false;
                    distance = 0;
                }
               
            }
        }
        else
            knockback.KnockLogic();
    }

    public void LockPlayer()
    {
        if (canEnemy)
        {
            for (int i = 0; i < players.Length; i++)
            {
                float distanceCurrent = Vector2.Distance(players[idLockPlayer].transform.position, enemy.transform.position);
                float newDistance = Vector2.Distance(players[i].transform.position, enemy.transform.position);
                if (newDistance < distanceCurrent && !players[i].GetComponent<PlayerControl>().PlayerStatus.Death)
                {
                    idLockPlayer = i;
                    distanceCurrent = newDistance;
                }
                distance = distanceCurrent;
            }
            /*if (distance < 5 || distance > 15)
            {
                lockMove = true;
            }
            else
            {
                //lockMove = false;
                //rb.velocity = Vector2.zero;
            }*/
        }
    }

    public void CheckGround()
    {
        ground = enemy.transform.position.y;
    }
}
