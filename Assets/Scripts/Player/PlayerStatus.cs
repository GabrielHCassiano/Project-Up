using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.VolumeComponent;

public class PlayerStatus : MonoBehaviour
{
    private PlayerControl playerControl;
    private SpriteRenderer spriteRenderer;
    private Knockback knockback;
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
    private bool trueDeath;


    private int force;

    private Vector3 checkpoint;

    public PlayerStatus(PlayerControl playerControl, SpriteRenderer spriteRenderer, Knockback knockback, InputsPlayers inputsPlayers)
    {
        this.playerControl = playerControl;
        this.spriteRenderer = spriteRenderer;
        this.knockback = knockback;
        this.inputsPlayers = inputsPlayers;
        this.checkpoint = playerControl.transform.position;
        maxLife = 50;
        life = maxLife;
        maxStamina = 25;
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

    public bool Death
    {
        get { return death; }
        set { death = value; }
    }

    public bool TrueDeath
    {
        get { return trueDeath; }
        set { trueDeath = value; }
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
            inputsPlayers.MoveDirection = Vector2.zero;
            playerControl.InStun();
        }

        if (inputsPlayers.ButtonStart && death && !trueDeath && extraLife > 0 && extraLife - 1 >= 0)
        {
            extraLife -= 1;
            life = maxLife;
            stamina = maxStamina;
            death = false;
            playerControl.transform.position = new Vector2(checkpoint.x + FindObjectOfType<Camera>().transform.position.x, checkpoint.y + FindObjectOfType<Camera>().transform.position.y);
            inputsPlayers.ButtonStart = false;
            spriteRenderer.gameObject.SetActive(true);
            playerControl.ResetAttack();
        }

        if (death && extraLife == 0)
        {
            trueDeath = true;
        }

    }

    public void InHurtLogic(GameObject enemy)
    {
        EnemyControl enemyControl = enemy.GetComponentInParent<EnemyControl>();

        life -= enemyControl.EnemyStatus.Force;
        if (enemyControl.EnemyCombat.InCombo == enemyControl.LeghtCombo && life > 0)
        {
            knockback.Knocking(enemyControl.SpriteRenderer);
        }
        if (enemyControl.EnemyCombat.InCombo < enemyControl.LeghtCombo)
            enemyControl.EnemyCombat.CanAttack = true;
        enemyControl.AttackSound();
        inHurt = true;
    }

}
