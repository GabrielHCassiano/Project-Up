using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItenControl : MonoBehaviour
{
    private PlayerStatus playerStatus;
    private bool inGet = false;

    public void Update()
    {
        GetStatus();
    }

    public void GetStatus()
    {
        if (inGet == true && playerStatus.SetIten == true)
        {
            playerStatus.SetIten = false;
            SetStatus();
        }
        else if (inGet == true && playerStatus.InHurt == true) 
        {
            inGet = false;
            playerStatus = null;
        }
    }

    public void SetStatus()
    {
        if (tag == "Candy")
        {
            print("GetCandy");
            playerStatus.Life += 50;
            Destroy(gameObject);
        }
        if (tag == "Coca")
        {
            print("GetCoca");
            playerStatus.Life += 200;
            Destroy(gameObject);
        }
        if (tag == "ExtraLife")
        {
            print("GetExtra");
            playerStatus.ExtraLife += 1;
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
            collision.GetComponentInParent<PlayerControl>().InputsPlayers.Button1 = false;
            playerStatus = collision.GetComponentInParent<PlayerControl>().PlayerStatus;
            playerStatus.GetIten = true;
            inGet = true;
        }
    }
}
