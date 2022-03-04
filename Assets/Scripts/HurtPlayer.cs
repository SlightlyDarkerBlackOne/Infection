using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

    public int damageToGive;
    private int currentDamage;
    public GameObject damageNumber;

    public int critChance;
    public int critMultiplier;
    private int crit;

    public bool isShurikenTrap = false;
    public float knockbackMultiplier = 5f;

    public float startCoolDownBetweenHits = 1f;
    private float coolDownBetweenHits = 0;
	
	// Update is called once per frame
	void Update () {
        if(coolDownBetweenHits > 0)
		    coolDownBetweenHits -= Time.deltaTime;
        else
            coolDownBetweenHits = 0;
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            if(coolDownBetweenHits <= 0){
                coll.gameObject.GetComponent<Player>().playerHealthManager.TakeDamage(DamageCalculation());

                var clone = (GameObject)Instantiate(damageNumber, coll.transform.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
                clone.transform.position = new Vector2(coll.transform.position.x, coll.transform.position.y);
                Destroy(clone, 2f);

                coolDownBetweenHits = startCoolDownBetweenHits;
            }
            

            //Knockback
            if(isShurikenTrap){
                Rigidbody2D playerRB = coll.GetComponent<Rigidbody2D>();
                Vector2 difference =  playerRB.transform.position - transform.position;

                //Vector2 afterKnockbackPos = new Vector2(coll.transform.position.x + difference.x, coll.transform.position.y + difference.y);

                difference = difference.normalized * knockbackMultiplier;

                playerRB.AddForce(difference, ForceMode2D.Impulse);
            }
        }
    }

    public int DamageCalculation(){
        currentDamage = damageToGive - PlayerStats.Instance.currentDefense;
            currentDamage = Crit(currentDamage);
            if (currentDamage <= 0)
                currentDamage = 1;
        
        return currentDamage;
    }

    private int Crit(int damage) {
        crit = Random.Range(0, 100);
        if (crit > (100 - critChance))
            return currentDamage *= critMultiplier;
        else {
            return damage;
        }
    }
}
