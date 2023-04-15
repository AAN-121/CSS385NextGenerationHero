using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public float speed = 20f;

    private Rigidbody2D rb2d;

    public static bool controlToggle = false;

    void Update()
    {

        Vector3 pos = transform.position;
        rb2d = rb2d = GetComponent<Rigidbody2D>();

        if (Input.GetKeyDown(KeyCode.M))
        {
            controlToggle = !controlToggle;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb2d.rotation += 0.25f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb2d.rotation -= 0.25f;
        }

        if (controlToggle == false)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            transform.position = mousePosition;
        } else
        {
            if (Input.GetKey(KeyCode.W))
            {
                speed += 0.2f;
            }

            if (Input.GetKey(KeyCode.S))
            {
                speed -= 0.2f;
            }

            rb2d.velocity = transform.up * speed;
        }
    }

}