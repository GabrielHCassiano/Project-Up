using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour
{
    private EnemyStatus enemyStatus;

    public void SetStatus(EnemyStatus enemyStatus)
    {
        this.enemyStatus = enemyStatus;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("HitboxPlayer")
            && GetComponentInParent<EnemyControl>().EnemyMovement.Ground <= collision.GetComponentInParent<PlayerControl>().PlayerMovement.Ground + 1.0f 
            && GetComponentInParent<EnemyControl>().EnemyMovement.Ground >= collision.GetComponentInParent<PlayerControl>().PlayerMovement.Ground - 1.0f)
        {
            enemyStatus.InHurtLogic(collision.gameObject);
        }
    }
}
