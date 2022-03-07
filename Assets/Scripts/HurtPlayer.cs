using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

    [SerializeField]
    private int damageToGive;
    private int _currentDamage;
    [SerializeField]
    private GameObject damageNumber;

    [SerializeField]
    private int critChance;
    [SerializeField]
    private int critMultiplier;
    private int crit;

    [SerializeField]
    private bool isShurikenTrap = false;
    [SerializeField]
    private float knockbackMultiplier = 5f;

    [SerializeField]
    private float startCoolDownBetweenHits = 1f;
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
                clone.GetComponent<FloatingNumbers>().damageNumber = _currentDamage;
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
        _currentDamage = Crit(damageToGive) - PlayerStats.Instance.currentDefense;
        if (_currentDamage <= 0)
            _currentDamage = 1;
        
        return _currentDamage;
    }

    private int Crit(int damage) {
        crit = Random.Range(0, 100);
        if (crit > (100 - critChance))
            return _currentDamage *= critMultiplier;
        else {
            return damage;
        }
    }
}
