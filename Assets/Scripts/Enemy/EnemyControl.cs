using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    private EnemyCombat enemyCombat;
    private EnemyStatus enemyStatus;
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
        enemyCombat = new EnemyCombat();
        enemyStatus = new EnemyStatus(gameObject);
        enemyAnimation = new EnemyAnimation(animator, spriteRenderer, enemyMovement);

        enemyHurtbox.SetStatus(enemyStatus);
    }

    // Update is called once per frame
    void Update()
    {
        enemyStatus.DeathLogic();
        enemyAnimation.FlipLogic();
        enemyAnimation.AnimationLogic();
    }

    private void FixedUpdate()
    {
        enemyMovement.MoveLogic();
    }

    public EnemyStatus EnemyStatus
    {
        get { return enemyStatus; }
        set { enemyStatus = value; }
    }
}
