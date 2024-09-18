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

    [SerializeField] private GameObject[] players;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private AudioSource attackAudio;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        enemyHurtbox = GetComponentInChildren<EnemyHurtbox>();

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();

        attackAudio = GetComponent<AudioSource>();

        enemyMovement = new EnemyMovement(rb, gameObject, players);
        enemyStatus = new EnemyStatus(gameObject);
        enemyCombat = new EnemyCombat(rb, enemyMovement, enemyStatus);
        enemyAnimation = new EnemyAnimation(animator, spriteRenderer, rb, enemyMovement, enemyStatus, enemyCombat);

        enemyHurtbox.SetStatus(enemyStatus);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + 1.0f, 1), new Vector3(transform.position.x, transform.position.y - 1.0f, 1), Color.red);
        enemyMovement.LockPlayer();
        enemyMovement.CheckGround();
        enemyCombat.AttackLogic();
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

    public void SetForce(int force)
    {
        enemyStatus.Force = force;
    }

    public void SpawnLogic()
    {
        enemyMovement.SpawnLogic();
    }

    public void ResetAttack()
    {
        enemyCombat.ResetAttack();
    }

    public void ResetStatus()
    {
        enemyCombat.ResetStatus();
    }

    public void InStun()
    {
        enemyCombat.InStun();
    }

    public void AttackSound()
    {
        attackAudio.Play();
    }
}
