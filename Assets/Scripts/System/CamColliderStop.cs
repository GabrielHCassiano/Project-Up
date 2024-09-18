using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamColliderStop : MonoBehaviour
{
    [SerializeField] private CamCollider camCollider;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            camCollider.CanCollider = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            camCollider.CanCollider = true;
        }
    }
}
