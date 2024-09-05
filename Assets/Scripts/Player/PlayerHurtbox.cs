using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtbox : MonoBehaviour
{
    private PlayerStatus playerStatus;

    public void SetStatus(PlayerStatus playerStatus)
    {
        this.playerStatus = playerStatus;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("HitboxEnemy")
            && GetComponentInParent<PlayerControl>().PlayerMovement.Ground <= collision.GetComponentInParent<EnemyControl>().EnemyMovement.Ground + 1.0f
            && GetComponentInParent<PlayerControl>().PlayerMovement.Ground >= collision.GetComponentInParent<EnemyControl>().EnemyMovement.Ground - 1.0f)
        {
            print("Enemy-Hit");
            playerStatus.InHurtLogic(collision.gameObject);
        }
    }
}
