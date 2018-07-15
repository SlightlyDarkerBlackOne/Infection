﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    private float currentMoveSpeed;
    public float diagonalMoveModifier;
    
    //dodati malo kasnije WaitForSeconds(0.1) dash f-ju
    public float dashSpeed;

    private Animator anim;
    private Rigidbody2D rb;

    private bool playerMoving;
    private Vector2 lastMove;

    private bool attacking; 
    public float attackTime;
    private float attackTimeCounter;

    public GameObject hitPoint;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        playerMoving = false;

        if (!attacking)
        {

            if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * currentMoveSpeed, rb.velocity.y);
                playerMoving = true;
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            }

            if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            {
                //transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
                rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * currentMoveSpeed);
                playerMoving = true;
                lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            }

            if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
                rb.velocity = new Vector2(0f, rb.velocity.y);

            if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
                rb.velocity = new Vector2(rb.velocity.x, 0f);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                attackTimeCounter = attackTime;
                attacking = true;

                //zaustavlja movement
                rb.velocity = Vector2.zero;

                anim.SetBool("Attack", true);
            }

            if (Input.GetAxisRaw("Jump") > 0.5) {
                //lmao ovo je kaos
                //rb.AddForce(new Vector2(rb.velocity.x, dashSpeed));

                rb.AddForce(new Vector2(rb.velocity.x + dashSpeed, rb.velocity.y + dashSpeed), ForceMode2D.Impulse);

            }

            // fixed diagonal movement speed
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5 && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5) {
                currentMoveSpeed = moveSpeed * diagonalMoveModifier;
            } else {
                currentMoveSpeed = moveSpeed;
            }
        }

        if(attackTimeCounter >= 0)
        {
            attackTimeCounter -= Time.deltaTime;

            //Da daje dmg dok napada
            //hitPoint.SetActive(true);
        }

        if(attackTimeCounter <= 0)
        {
            attacking = false;
            anim.SetBool("Attack", false);

            //Da ne daje dmg dok je idle
            //hitPoint.SetActive(false);
        }

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }

    //used to access the private variable attacking
    public bool Attacking() {
        if (attacking)
            return true;
        else return false;
    }
}
