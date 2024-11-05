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
            print("GetCandy");
            playerControl.PlayerStatus.Life += 50;
            Destroy(gameObject);
        }
        if (tag == "Coca")
        {
            print("GetCoca");
            playerControl.PlayerStatus.Life += 200;
            Destroy(gameObject);
        }
        if (tag == "ExtraLife")
        {
            print("GetExtra");
            playerControl.PlayerStatus.ExtraLife += 1;
            Destroy(gameObject);
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
