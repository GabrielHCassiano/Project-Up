using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatus : MonoBehaviour
{
    private GameObject player;

    private int maxLife;
    private int life;
    private int maxStamina;
    private int stamina;

    private bool inHurt;
    private bool death;

    private int force;

    public PlayerStatus(GameObject player)
    {
        this.player = player;
        maxLife = 500;
        life = maxLife;
        maxStamina = 300;
        stamina = 0;
        force = 50;
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
        set {  stamina = value; }
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

    public void StatusBalance()
    {
        if (life > maxLife)
            life = maxLife;
        if (life < 0)
            life = 0;
        if (stamina > maxStamina)
            stamina = maxStamina;
        if (stamina < 0)
            stamina = 0;
    }

    public void DeathLogic()
    {
        if (life <= 0)
            death = true;
        if (death)
            player.SetActive(false);
    }

    public void InHurtLogic(GameObject enemy)
    {
        life -= enemy.GetComponentInParent<EnemyControl>().EnemyStatus.Force;
        inHurt = true;
    }

}
