using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    attack,
    interact
}
public class PlayerController : MonoBehaviour
{

    public PlayerState currentState;
    public float moveSpeed;
    private float currentMoveSpeed;

    //dodati malo kasnije WaitForSeconds(0.1) dash f-ju
    public float dashSpeed;
    [SerializeField]
    private float dashTime;
    public float startDashTime;
    public int dashManaCost = 10;


    private Animator anim;
    private Rigidbody2D rb;

    [SerializeField]
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

    bool playerFrozen = false;

    #region Singleton
    public static PlayerController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        transform.position = FindObjectOfType<LevelManager>()
                .playerLevelPosition.transform.position;

        currentState = PlayerState.idle;
        //crossHair.SetActive(true);
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
    void Update(){
        Menu();
        MoveAndAttack();
        TrailEffect();
        SetAnimations();
        //ProcessInputs();
        //AimAndShoot();
    }

    void Menu(){
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
        {
            GameObject inventoryPanel = Inventory.instance.InventoryPanel;

            if (!inventoryPanel.activeSelf){
                inventoryPanel.SetActive(true);
            } else {
                inventoryPanel.SetActive(false);
            }
        }
    }
    private void MoveAndAttack(){
        playerMoving = false;
        if (!attacking){
            if (playerFrozen){
                moveInput = Vector2.zero;
            } else{
                moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            }
            if (moveInput != Vector2.zero){
                rb.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
                playerMoving = true;
                lastMove = moveInput;
            } else{
                rb.velocity = Vector2.zero;
            }
            //if(meleeWeaponEquiped){}
            //Movement when attacking
            if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.RightControl))
                    && !playerFrozen && (currentState != PlayerState.attack)){
                attackTimeCounter = attackTime;
                attacking = true;

                SFXManager.Instance.PlaySound(SFXManager.Instance.playerAttack);

                rb.velocity = Vector2.zero;
                //GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                //arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(15.0f, 0.0f);
            }
            Dash();
        }

        if (attackTimeCounter >= 0){
            attackTimeCounter -= Time.deltaTime;
            //Da daje dmg dok napada
            //hitPoint.SetActive(true);
        }

        if (attackTimeCounter <= 0){
            attacking = false;
            anim.SetBool("Attack", false);
            //Da ne daje dmg dok je idle
            //hitPoint.SetActive(false);
        }
    }
    private void SetAnimations()
    {
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }
    private void TrailEffect(){
        if (playerMoving){
            if (timeBtwTrail <= 0){
                Instantiate(trailEffect, transform.position, Quaternion.identity);
                timeBtwTrail = startTimeBtwTrail;
            } else {
                timeBtwTrail -= Time.deltaTime;
            }
        }
    }
    public void FrezePlayer(){
        playerFrozen = true;
    }
    public void UnFreezePlayer(){
        playerFrozen = false;
    }

    public void Dash(){
        if (moveInput != Vector2.zero && Input.GetKeyDown(KeyCode.Space)){
            if (dashTime <= 0 && PlayerManaManager.Instance.playerCurrentMana >= dashManaCost){
                dashTime = startDashTime;
                rb.velocity = rb.velocity * dashSpeed;
                PlayerManaManager.Instance.TakeMana(dashManaCost);

                SFXManager.Instance.PlaySound(SFXManager.Instance.dash);
            } else {
                //rb.velocity = Vector2.zero;
            }
        }
        dashTime -= Time.deltaTime;
    }

    //used to access the private variable attacking
    public bool Attacking()
    {
        if (attacking)
            return true;
        else return false;
    }

    private IEnumerator AttackCo()
    {
        anim.SetBool("Attack", true);
        currentState = PlayerState.attack;
        yield return null;
        anim.SetBool("Attack", false);
        yield return new WaitForSeconds(attackTime);
        currentState = PlayerState.idle;
    }

    public void SetMoveSpeedForADuration(int modifier, int duration)
    {
        float oldMoveSpeed = moveSpeed;
        moveSpeed *= modifier;

        StartCoroutine(WaitCorutine(duration));
        moveSpeed = oldMoveSpeed;
    }

    IEnumerator WaitCorutine(int duration)
    {
        yield return new WaitForSeconds(duration);
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

    private void ProcessInputs()
    {
        Vector3 mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
        aim = aim + mouseMovement;
        if (aim.magnitude > 1.0f)
        {
            aim.Normalize();
        }
        isAiming = Input.GetButton("Fire1");
        endOfAiming = Input.GetButtonUp("Fire1");
        if (crossHair != null)
            crossHair.transform.localPosition = aim * cursorDistance;


        Vector2 shootingDirection = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        shootingDirection.Normalize();
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * 15.0f;
            arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
            Destroy(arrow, 2.0f);
        }

    }
}
