using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private GameObject enemy;

    private int maxLife;
    private int life;
    private int maxStamina;
    private int stamina;

    private bool death;

    private int force;

    public EnemyStatus(GameObject enemy)
    {
        this.enemy = enemy;
        maxLife = 150;
        life = maxLife;
        maxStamina = 0;
        stamina = maxStamina;
        force = 70;
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

    public void DeathLogic()
    {
        if (life <= 0)
            death = true;
        if (death)
            enemy.SetActive(false);
    }
}
