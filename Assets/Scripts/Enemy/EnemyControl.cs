using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    private EnemyStatus enemyStatus;
    private EnemyCombat enemyCombat;
    private EnemyAnimation enemyAnimation;
    private EnemyHurtbox enemyHurtbox;

    private GameObject player;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        enemyHurtbox = GetComponentInChildren<EnemyHurtbox>();

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();

        enemyMovement = new EnemyMovement(rb, gameObject, player);
        enemyStatus = new EnemyStatus(gameObject);
        enemyCombat = new EnemyCombat(rb, enemyMovement, enemyStatus);
        enemyAnimation = new EnemyAnimation(animator, spriteRenderer, enemyMovement, enemyStatus);

        enemyHurtbox.SetStatus(enemyStatus);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + 1.0f, 1), new Vector3(transform.position.x, transform.position.y - 1.0f, 1), Color.red);
        enemyMovement.LockPlayer();
        enemyMovement.CheckGround();
        enemyStatus.DeathLogic();
        enemyAnimation.FlipLogic();
        enemyAnimation.AnimationLogic();
    }

    private void FixedUpdate()
    {
        enemyMovement.MoveLogic();
    }

    public EnemyMovement EnemyMovement
    {
        get { return enemyMovement; }
        set { enemyMovement = value; }
    }

    public EnemyStatus EnemyStatus
    {
        get { return enemyStatus; }
        set { enemyStatus = value; }
    }

    public void SpawnLogic()
    {
        enemyMovement.SpawnLogic();
    }

    public void ResetStatus()
    {
        enemyCombat.ResetStatus();
    }

    public void InStun()
    {
        enemyCombat.InStun();
    }
}
