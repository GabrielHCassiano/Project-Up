using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackCount;
    [SerializeField] private float knockbackTime;
    [SerializeField] private bool isKnock;

    [SerializeField] private float difference;
    private Rigidbody2D rb;
    [SerializeField] private GameObject sprite;

    // Start is called before the first frame update
    void Start()
    {
        knockbackForce = 10;
        knockbackCount = -0.1f;
        knockbackTime = 0.3f;
        rb = GetComponent<Rigidbody2D>();
    }

    public float KnockbackCountLogic
    {
        get { return knockbackCount; }
        set { knockbackCount = value; }
    }

    public void KnockLogic()
    {
        rb.velocity = Vector2.zero;

        if (isKnock == true)
        {
            rb.velocity = new Vector2(difference, rb.velocity.y);
        }
        knockbackCount -= Time.deltaTime;
    }

    public void Knocking(SpriteRenderer spriteRenderer)
    {
        knockbackCount = knockbackTime;

        if (gameObject != null)
        {
            //difference = rb.transform.position - gameObject.transform.position;
            difference = (spriteRenderer.transform.localScale.x) * knockbackForce;
            isKnock = true;
        }
        else
        {
            isKnock = false;
        }
    }
}