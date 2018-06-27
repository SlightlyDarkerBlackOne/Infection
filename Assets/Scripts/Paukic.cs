using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paukic : MonoBehaviour {

    public float speed;

    Rigidbody2D rb;
    Animator anim;

    bool facingRight = false;
    bool facingLeft = false;
    bool facingDown = true;
    bool facingUp = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        float horzMove = Input.GetAxisRaw("Horizontal");
        float vertMove = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(horzMove * speed, vertMove * speed);

        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            anim.Play("Paukic walk");

            Facing("lijevo");
        }

        if (Input.GetKey("w") || Input.GetKey("up"))
        {
            anim.Play("Paukic walk");

            Facing("gore");
        }

        if (Input.GetKey("d")|| Input.GetKey("right"))
        {
            anim.Play("Paukic walk");

            Facing("desno");
        }

        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            anim.Play("Paukic walk");

            Facing("dolje");
        }

        if (Input.GetKeyUp("s") || Input.GetKeyUp("a") || Input.GetKeyUp("w") || Input.GetKeyUp("d") || Input.GetKey("right") || Input.GetKey("left") || Input.GetKey("up") || Input.GetKey("down"))
        {
            anim.Play("Paukic idle");
            Debug.Log("idle");
        }

        
    }

    void Facing(string direction)
    {
        if (direction == "lijevo")
        {
            if (facingUp)
            {
                anim.transform.Rotate(0, 0, 90);
                facingUp = false;
            }
            if (facingDown)
            {
                anim.transform.Rotate(0, 0, 270);
                facingDown = false;
            }
            if (facingRight)
            {
                anim.transform.Rotate(0, 0, 180);
                facingRight = false;
            }

            facingLeft = true;
        }

        if (direction == "desno")
        {
            if (facingUp)
            {
                anim.transform.Rotate(0, 0, 270);
                facingUp = false;
            }
            if (facingDown)
            {
                anim.transform.Rotate(0, 0, 90);
                facingDown = false;
            }
            if (facingLeft)
            {
                anim.transform.Rotate(0, 0, 180);
                facingLeft = false;
            }

            facingRight = true;
        }

        if (direction == "dolje")
        {
            if (facingUp)
            {
                anim.transform.Rotate(0, 0, 180);
                facingUp = false;
            }
            if (facingLeft)
            {
                anim.transform.Rotate(0, 0, 90);
                facingLeft = false;
            }
            if (facingRight)
            {
                anim.transform.Rotate(0, 0, 270);
                facingRight = false;
            }

            facingDown = true;
        }

        if (direction == "gore")
        {
            if (facingLeft)
            {
                anim.transform.Rotate(0, 0, 270);
                facingLeft = false;
            }
            if (facingDown)
            {
                anim.transform.Rotate(0, 0, 180);
                facingDown = false;
            }
            if (facingRight)
            {
                anim.transform.Rotate(0, 0, 90);
                facingRight = false;
            }

            facingUp = true;
        }
    }
}
