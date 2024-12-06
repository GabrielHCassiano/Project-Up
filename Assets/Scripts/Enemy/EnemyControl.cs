using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    private EnemyStatus enemyStatus;
    private EnemyCombat enemyCombat;
    private EnemyAnimation enemyAnimation;
    private EnemyHurtbox enemyHurtbox;

    [SerializeField] private GameObject[] players;

    private NavMeshAgent navMeshAgent;
    private Rigidbody2D rb;
    private Knockback knockback;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField] private int maxLife;
    [SerializeField] private int leghtCombo;

    private AudioSource attackAudio;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        enemyHurtbox = GetComponentInChildren<EnemyHurtbox>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();

        attackAudio = GetComponent<AudioSource>();

        enemyMovement = new EnemyMovement(navMeshAgent, rb, knockback, gameObject, players);
        enemyStatus = new EnemyStatus(knockback, gameObject, maxLife);
        enemyCombat = new EnemyCombat(rb, enemyMovement, enemyStatus, leghtCombo);
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

    public EnemyCombat EnemyCombat
    {
        get { return enemyCombat; }
        set { enemyCombat = value; }
    }

    public SpriteRenderer SpriteRenderer 
    { 
        get { return spriteRenderer; }
        set { spriteRenderer = value; }
    }

    public int LeghtCombo
    {
        get { return leghtCombo; }
        set { leghtCombo = value; }
    }

    public void SetForce(int force)
    {
        enemyStatus.Force = force;
    }

    public void SpawnLogic()
    {
        enemyMovement.SpawnLogic();
    }

    public void SetCombo()
    {
        enemyCombat.SetCombo();
    }

    public void ResetAttack()
    {
        enemyMovement.CanMove = true;
        enemyCombat.ResetAttack();
    }

    public void ResetStatus()
    {
        enemyMovement.CanMove = true;
        enemyCombat.ResetStatus();
    }

    public void InStun()
    {
        enemyMovement.CanMove = false;
        enemyCombat.InStun();
    }

    public void AttackSound()
    {
        attackAudio.Play();
    }

    public void StartDeath()
    {
        enemyMovement.CanMove = false;
        enemyStatus.InHurt = false;
        enemyStatus.Death = false;
    }

    public void DeathSet()
    {
        enemyStatus.DeathSet();
    }
}
