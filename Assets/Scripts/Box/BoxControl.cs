using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Analytics;
using NavMeshPlus.Components;

public class BoxControl : MonoBehaviour
{
    [SerializeField] private int life;

    [SerializeField] private GameObject iten;

    [SerializeField] private bool secret = false;

    private float ground;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        ground = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorLogic();
    }

    public void DropItenLogic()
    {
        if (life <= 0)
        {
            if (iten != null)
            {
                iten.transform.parent = null;
                iten.SetActive(true);
            }

            FindObjectOfType<NavMeshSurface>().BuildNavMesh();

            Destroy(gameObject);
        }
    }

    public void ResetHitSecret()
    {
        animator.SetBool("Hurt", false);
    }


    public void AnimatorLogic()
    {
        animator.SetInteger("Life", life);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("HitboxPlayer")
            && ground <= collision.GetComponentInParent<PlayerControl>().PlayerMovement.Ground + 1.0f
            && ground >= collision.GetComponentInParent<PlayerControl>().PlayerMovement.Ground - 1.0f)
        {
            PlayerControl playerControl = collision.GetComponentInParent<PlayerControl>();
            life -= playerControl.PlayerStatus.Force;
            playerControl.PlayerStatus.Stamina += 2;

            if (!playerControl.PlayerCombat.HeavyAttack && !playerControl.PlayerCombat.SpecialAttack)
                playerControl.AttackSound(playerControl.PlayerCombat.InCombo - 1);
            else if (playerControl.PlayerCombat.HeavyAttack && !playerControl.PlayerCombat.SpecialAttack)
                playerControl.AttackSound(2);
            else
                playerControl.AttackSound(0);

            playerControl.PlayerCombat.CanAttack = true;

            animator.SetBool("Hurt", true);


            if (!secret)
                collision.GetComponentInParent<PlayerHUD>().AddHit();

        }
    }
}
