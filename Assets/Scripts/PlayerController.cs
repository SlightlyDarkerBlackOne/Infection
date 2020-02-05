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

    public GameObject crossHair;
    public GameObject arrowPrefab;

    Vector3 aim;
    bool isAiming;
    bool endOfAiming;
    public float cursorDistance;

    bool meleeWeaponEquiped;

    void Awake(){
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        crossHair.SetActive(true);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
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
            
            //if(meleeWeaponEquiped){}
            //Movement when attacking
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.RightControl))
            {
                attackTimeCounter = attackTime;
                attacking = true;

                //zaustavlja movement
                rb.velocity = Vector2.zero;

                anim.SetBool("Attack", true);
                
                
                //GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                //arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(15.0f, 0.0f);
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
                    dashTime -= Time.fixedDeltaTime;
                    rb.velocity = rb.velocity * dashSpeed;
                }
                

            }
        }

        if(attackTimeCounter >= 0)
        {
            attackTimeCounter -= Time.fixedDeltaTime;

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
                timeBtwTrail -= Time.fixedDeltaTime;
            }
        }

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);

        ProcessInputs();
        //AimAndShoot();
    }

    //used to access the private variable attacking
    public bool Attacking() {
        if (attacking)
            return true;
        else return false;
    }

    /*private void AimAndShoot() {
        
        aim = new Vector3(Input.GetAxis("AimHorizontal"), Input.GetAxis("AimVertical"), 0.0f);
        Vector2 shootingDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        // For joystick
        if (aim.magnitude > 0.0f) {
            aim.Normalize();

            crossHair.transform.localPosition = aim * 0.4f;
            crossHair.SetActive(true);

            shootingDirection.Normalize();
            if (Input.GetButtonUp("Fire1")) {
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * 5.0f;
                arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                Destroy(arrow, 2.0f);    
        }

        } else {
            crossHair.SetActive(false);

        }
    } */



    private void ProcessInputs() {
        Vector3 mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
        aim = aim + mouseMovement;
        if(aim.magnitude > 1.0f){
            aim.Normalize();
        }
        isAiming = Input.GetButton("Fire1");
        endOfAiming = Input.GetButtonUp("Fire1");
        crossHair.transform.localPosition = aim * cursorDistance;
        

        Vector2 shootingDirection = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        shootingDirection.Normalize();
        if (Input.GetButtonDown("Fire1")) {
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * 15.0f;
            arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
            Destroy(arrow, 2.0f);
        }
        
    }
}
