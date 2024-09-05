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
        if (collision.gameObject.layer == LayerMask.NameToLayer("HitboxEnemy"))
        {
            print("Enemy-Hit");
            playerStatus.Life -= collision.GetComponentInParent<EnemyControl>().EnemyStatus.Force;
        }
    }
}
