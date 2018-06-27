using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour {

    //weapon dmg
    public int damageToGive;
    
    //full dmg
    private int currentDamage;

    public int critChance;
    public int critMultiplier;
    private int crit;

    public GameObject damageBurst;
    public Transform hitPoint;
    public GameObject damageNumber;

    private PlayerStats thePS;


	// Use this for initialization
	void Start () {

        //Crit
        crit = Random.Range(0, 100);
        if (crit > 100 - critChance)
            damageToGive *= critMultiplier;

        thePS = FindObjectOfType<PlayerStats>();
    }
	
	// Update is called once per frame
	void Update () {



	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            currentDamage = damageToGive + thePS.currentAttack;

            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(currentDamage);

            Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
            var clone = (GameObject)Instantiate(damageNumber, hitPoint.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
            clone.transform.position = new Vector2(hitPoint.position.x, hitPoint.position.y);
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageToGive);

            Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
            var clone = (GameObject) Instantiate(damageNumber, hitPoint.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = damageToGive;
        }
    }
    */
}
