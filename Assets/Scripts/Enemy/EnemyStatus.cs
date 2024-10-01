using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyStatus : MonoBehaviour
{
    private GameObject enemy;

    private int maxLife;
    private int life;
    private int maxStamina;
    private int stamina;

    private bool inHurt;
    private bool death = false;
    private bool canDeath = true; 

    private int force;

    public EnemyStatus(GameObject enemy)
    {
        this.enemy = enemy;
        maxLife = 200;
        life = maxLife;
        maxStamina = 0;
        stamina = maxStamina;
    }

    public int MaxLife
    {
        get { return maxLife; }
        set { maxLife = value; }
    }

    public int Life
    {
        get { return life; }
        set { life = value; }
    }

    public int MaxStamina
    {
        get { return maxStamina; }
        set { maxStamina = value; }
    }

    public int Stamina
    {
        get { return stamina; }
        set { stamina = value; }
    }

    public int Force
    {
        get { return force; }
        set { force = value; }
    }

    public bool InHurt
    {
        get { return inHurt; }
        set { inHurt = value; }
    }

    public bool Death
    {
        get { return death; }
        set { death = value; }
    }

    public void DeathLogic()
    {
        if (life <= 0 && canDeath)
        {
            death = true;
            canDeath = false;
        }
    }

    public void DeathSet()
    {
        Destroy(enemy.gameObject);
    }

    public void InHurtLogic(GameObject player)
    {
        life -= player.GetComponentInParent<PlayerControl>().PlayerStatus.Force;
        player.GetComponentInParent<PlayerControl>().PlayerStatus.Stamina += 5;
        player.GetComponentInParent<PlayerControl>().PlayerCombat.CanAttack = true;
        player.GetComponentInParent<PlayerControl>().AttackSound();
        player.GetComponentInParent<PlayerHUD>().AddHit();
        inHurt = true;
    }
}
