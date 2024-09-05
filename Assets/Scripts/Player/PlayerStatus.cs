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

    private bool death;

    private int force;

    public PlayerStatus(GameObject player)
    {
        this.player = player;
        maxLife = 1000;
        life = maxLife;
        maxStamina = 0;
        stamina = maxStamina;
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

    public void DeathLogic()
    {
        if (life <= 0)
            death = true;
        if (death)
            player.SetActive(false);
    }
}
