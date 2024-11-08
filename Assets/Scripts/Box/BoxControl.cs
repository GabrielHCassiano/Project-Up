using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Analytics;

public class BoxControl : MonoBehaviour
{
    [SerializeField] private int life;

    [SerializeField] private GameObject iten;

    private float ground;

    // Start is called before the first frame update
    void Start()
    {
        ground = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        DropItenLogic();
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

            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("HitboxPlayer")
            && ground <= collision.GetComponentInParent<PlayerControl>().PlayerMovement.Ground + 1.0f
            && ground >= collision.GetComponentInParent<PlayerControl>().PlayerMovement.Ground - 1.0f)
        {
            PlayerControl playerControl = collision.GetComponentInParent<PlayerControl>();
            life -= playerControl.PlayerStatus.Force;
            playerControl.PlayerStatus.Stamina += 5;
            playerControl.AttackSound(playerControl.PlayerCombat.InCombo - 1);
            playerControl.PlayerCombat.CanAttack = true;
            collision.GetComponentInParent<PlayerHUD>().AddHit();
        }
    }
}
