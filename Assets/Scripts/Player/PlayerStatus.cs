using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatus : MonoBehaviour
{
    private PlayerControl playerControl;
    private SpriteRenderer spriteRenderer;
    private InputsPlayers inputsPlayers;

    private int maxLife;
    private int life;
    private int maxStamina;
    private int stamina;
    private int extraLife;

    private bool inHurt;
    private bool getIten;
    private bool setIten;
    private bool death;

    private int force;

    private Vector3 checkpoint;

    public PlayerStatus(PlayerControl playerControl, SpriteRenderer spriteRenderer, InputsPlayers inputsPlayers)
    {
        this.playerControl = playerControl;
        this.spriteRenderer = spriteRenderer;
        this.inputsPlayers = inputsPlayers;
        checkpoint = playerControl.transform.position;
        maxLife = 200;
        life = maxLife;
        maxStamina = 100;
        stamina = maxStamina;
        extraLife = 1;
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

    public int ExtraLife
    {
        get { return extraLife; }
        set { extraLife = value; }
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

    public bool GetIten
    {
        get { return getIten; }
        set { getIten = value; }
    }

    public bool SetIten
    {
        get { return setIten; }
        set { setIten = value; }
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
        {
            death = true;
        }
        if (death)
        {
            spriteRenderer.gameObject.SetActive(false);
            playerControl.InStun();
        }

        if (inputsPlayers.ButtonStart && death && extraLife > 0 && extraLife - 1 >= 0)
        {
            extraLife -= 1;
            life = maxLife;
            stamina = maxStamina;
            death = false;
            //playerControl.transform.position = checkpoint;
            inputsPlayers.ButtonStart = false;
            spriteRenderer.gameObject.SetActive(true);
            playerControl.ResetAttack();
        }

    }

    public void InHurtLogic(GameObject enemy)
    {
        life -= enemy.GetComponentInParent<EnemyControl>().EnemyStatus.Force;
        enemy.GetComponentInParent<EnemyControl>().AttackSound();
        inHurt = true;
    }

}
