using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderlingController : MonoBehaviour {

    public float moveSpeed;
    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;

    private Rigidbody2D rb;

    private bool moving;

    public float timeBetweenMove;
    private float timeBetweenMoveCounter;
    public float timeToMove;
    private float timeToMoveCounter;

    private Vector3 moveDirection;

    //za reloudanje levela ako player umre
    public float waitToReload;
    public bool reloading;
    private GameObject thePlayer;

    public Collider2D walkZone;
    private bool hasWalkZone;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody2D>();

        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

        if(walkZone != null) {
            minWalkPoint = walkZone.bounds.min;
            maxWalkPoint = walkZone.bounds.max;
            hasWalkZone = true;
        }
        
	}
	
	// Update is called once per frame
	void Update () {

        if (moving)
        {
            timeToMoveCounter -= Time.deltaTime;
            rb.velocity = moveDirection;

            //Ako je dosao do ruba zone za hodanje
            //isOverTheZone();
            

            if (timeToMoveCounter < 0f)
            {
                moving = false;
                timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
            }

        } else
        {
            timeBetweenMoveCounter -= Time.deltaTime;
            rb.velocity = Vector2.zero;

            if(timeBetweenMoveCounter < 0f)
            {
                moving = true;
                timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

                moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
            }
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
    void isOverTheZone() {
        if (hasWalkZone && transform.position.y > maxWalkPoint.y || transform.position.x > maxWalkPoint.x) {
            rb.velocity = Vector2.zero;
            moving = false;
            timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        }
    }
}
