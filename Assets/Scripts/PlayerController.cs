using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    private float currentMoveSpeed;
    
    //dodati malo kasnije WaitForSeconds(0.1) dash f-ju
    public float dashSpeed;
    [SerializeField]
    private float dashTime;
    public float startDashTime;

    private Animator anim;
    private Rigidbody2D rb;

    private bool playerMoving;
    private Vector2 lastMove;
    private Vector2 moveInput;

    private bool attacking; 
    public float attackTime;
    private float attackTimeCounter;

    public float startTimeBtwTrail;
    private float timeBtwTrail;
    public GameObject trailEffect;

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
            //Movement Mechanic
            //Normalized so diagonal movespeed is same as vert & horz ms
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            if(moveInput != Vector2.zero) {
                rb.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
                playerMoving = true;
                lastMove = moveInput;
            } else {
                rb.velocity = Vector2.zero;
            }

            //Movement when attacking
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.RightControl))
            {
                attackTimeCounter = attackTime;
                attacking = true;

                //zaustavlja movement
                rb.velocity = Vector2.zero;

                anim.SetBool("Attack", true);
            }

            //Dash
            if (Input.GetKeyDown(KeyCode.Space)) {
                //lmao ovo je kaos
                //rb.AddForce;(new Vector2(rb.velocity.x, dashSpeed));
                if(dashTime <= 0) {
                    dashTime = startDashTime;
                    rb.velocity = Vector2.zero;
                }
                else {
                    dashTime -= Time.deltaTime;
                    rb.velocity = rb.velocity * dashSpeed;
                }
                

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

        if(playerMoving){
            if (timeBtwTrail <= 0) {
                Instantiate(trailEffect, transform.position, Quaternion.identity);
                timeBtwTrail = startTimeBtwTrail;
            } else {
                timeBtwTrail -= Time.deltaTime;
            }
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
