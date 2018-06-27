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

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            currentDamage = damageToGive - thePS.currentDefense;
            if (currentDamage <= 0)
                currentDamage = 1;

            coll.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(currentDamage);

            var clone = (GameObject)Instantiate(damageNumber, coll.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
            clone.transform.position = new Vector2(coll.transform.position.x, coll.transform.position.y);
        }
    }
}
