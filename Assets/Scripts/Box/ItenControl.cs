using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItenControl : MonoBehaviour
{
    private PlayerControl playerControl;
    private bool inGet = false;

    public void Update()
    {
        GetStatus();
    }

    public void GetStatus()
    {
        if (inGet == true && playerControl.PlayerStatus.SetIten == true)
        {
            playerControl.PlayerStatus.SetIten = false;
            playerControl.ItenSound();
            SetStatus();
        }
        else if (inGet == true && playerControl.PlayerStatus.InHurt == true) 
        {
            inGet = false;
            playerControl.PlayerStatus = null;
        }
    }

    public void SetStatus()
    {
        if (tag == "Candy")
        {
            playerControl.PlayerStatus.Life += 5;
            gameObject.SetActive(false);
        }
        if (tag == "Coca")
        {
            playerControl.PlayerStatus.Life += 20;
            gameObject.SetActive(false);
        }
        if (tag == "ExtraLife")
        {
            playerControl.PlayerStatus.ExtraLife += 1;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("GroundCollisionPlayer")
            && collision.GetComponentInParent<PlayerControl>() != null
            && collision.GetComponentInParent<PlayerControl>().InputsPlayers.Button1 
            && collision.GetComponentInParent<PlayerControl>().PlayerStatus.InHurt == false)
        {
            playerControl = collision.GetComponentInParent<PlayerControl>();
            playerControl.InputsPlayers.Button1 = false;
            playerControl.PlayerStatus.GetIten = true;
            inGet = true;
        }
    }
}
