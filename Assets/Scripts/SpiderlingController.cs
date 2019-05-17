using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderlingController : MonoBehaviour {

    public float moveSpeed;
    public float moveSpeedChaseModifier;
    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;

    private Rigidbody2D rb;

    private bool moving;

    private Animator anim;

    public float timeBetweenMove;
    private float timeBetweenMoveCounter;
    public float timeToMove;
    private float timeToMoveCounter;

    private Vector3 moveDirection;

    //for level reload if the player dies
    public float waitToReload;
    public bool reloading;
    private Transform player;

    public Collider2D walkZone;
    private bool hasWalkZone;

    public float chasingDistance;
    public float unChasingDistance;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

        if(walkZone != null) {
            minWalkPoint = walkZone.bounds.min;
            maxWalkPoint = walkZone.bounds.max;
            hasWalkZone = true;
        }
        
	}
	
	void Update () {

        //Ako je unutar chase udaljenosti lovi ga
        if(Vector2.Distance(transform.position, player.position) < chasingDistance) {
            Chase();
        }
        
        //Ako je izaso izvan chase udaljenosti prestani ga lovit (patroliraj)
        if (Vector2.Distance(transform.position, player.position) > unChasingDistance) {
            Patrol();
        }

        if (reloading)
        {
            waitToReload -= Time.deltaTime;
            if(waitToReload < 0)
            {
                //reload scene
                //SceneManager.LoadScene(SceneManager.GetActiveScene().builtindex);
            }
        }

    }

    void Chase() {
        //Debug.Log("Chasing");
        anim.SetBool("isFollowing", true);
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * moveSpeedChaseModifier * Time.deltaTime);
        anim.SetBool("isMoving", true);
    }

    void Patrol() {
        //Debug.Log("Patrolling");
        anim.SetBool("isFollowing", false);
        if (moving) {
            timeToMoveCounter -= Time.deltaTime;
            rb.velocity = moveDirection;

            //Ako je dosao do ruba zone za hodanje
            //IsOverTheZone();

            if (timeToMoveCounter < 0f) {
                moving = false;
                anim.SetBool("isMoving", false);
                timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
            }

        } else {
            timeBetweenMoveCounter -= Time.deltaTime;
            rb.velocity = Vector2.zero;

            if (timeBetweenMoveCounter < 0f) {
                moving = true;
                anim.SetBool("isMoving", true);
                timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

                moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
            }
        }
    }

    void IsOverTheZone() {
        if (hasWalkZone && transform.position.y > maxWalkPoint.y || transform.position.x > maxWalkPoint.x) {
            rb.velocity = Vector2.zero;
            moving = false;
            anim.SetBool("isMoving", false);
            timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        }
    }
}
