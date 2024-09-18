using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStop : MonoBehaviour
{
    [SerializeField] private CamCollider camCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "CamLock")
        {
            camCollider.NextLevel = true;
        }
    }
}
