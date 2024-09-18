using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCollider : MonoBehaviour
{

    private Rigidbody2D rb;

    private bool colliderCam;
    private bool canCollider = true;
    private bool nextlevel = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (colliderCam && canCollider && !nextlevel)
            rb.velocity = new Vector2(6, 0);
        else
            rb.velocity = Vector2.zero;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("HurtboxPlayer"))
        {
            colliderCam = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("HurtboxPlayer"))
        {
            colliderCam = false;
        }
    }

    public bool CanCollider
    { 
        get { return canCollider; }
        set { canCollider = value; }
    }

    public bool NextLevel
    {
        get { return nextlevel; }
        set { nextlevel = value; }
    }

}
