using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxControl : MonoBehaviour
{

    private float startPos, lenght;
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxEffect;

    [SerializeField] private float diference;

    private Vector3 pos;

    [SerializeField] private float distance;
    [SerializeField] private float movement;

    [SerializeField] private bool canCloud;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        ParallaxLogic();
    }

    public void ParallaxLogic()
    {
        if (canCloud)
        {
            distance += (0.1f * Time.deltaTime)/* * parallaxEffect*/;
            movement -= (0.1f * Time.deltaTime)/* * (1 - parallaxEffect)*/;

            transform.position = new Vector3(startPos + distance + cam.transform.position.x, transform.position.y, transform.position.z);

            if (movement > startPos + lenght)
                startPos += lenght;
            else if (movement < startPos - lenght)
                startPos -= lenght;
        }
        else
        {
            pos = cam.transform.position;

            distance = pos.x * parallaxEffect;
            movement = pos.x * (1 - parallaxEffect);

            transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

            if (movement > startPos + lenght + diference)
                startPos += lenght + diference;
            else if (movement < startPos - lenght + diference)
                startPos -= lenght + diference;
        }
    }

}
