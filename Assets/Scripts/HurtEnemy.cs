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

    public bool yoyoEquiped;
    public bool bowEquipped;
    public bool bladeVortex;

    private void OnTriggerEnter2D(Collider2D other) { 
        if (other.gameObject.tag == "Enemy")
        {
            if (PlayerController.Instance.Attacking() || yoyoEquiped || bowEquipped || bladeVortex) {
                currentDamage = damageToGive + PlayerStats.Instance.currentAttack;
                currentDamage = Crit(currentDamage); //Critical Strike

                other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(currentDamage);
                SFXManager.Instance.PlaySound(SFXManager.Instance.enemyHit);

                Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
                var clone = (GameObject)Instantiate(damageNumber, hitPoint.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
                clone.transform.position = new Vector2(hitPoint.position.x, hitPoint.position.y);
                if (bowEquipped)
                {
                    Destroy(gameObject, 0.001f);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            if (yoyoEquiped || bowEquipped) {
                currentDamage = damageToGive + PlayerStats.Instance.currentAttack;
                currentDamage = Crit(currentDamage); //Critical Strike

                other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(currentDamage);
                SFXManager.Instance.PlaySound(SFXManager.Instance.enemyHit);
                
                Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
                var clone = (GameObject)Instantiate(damageNumber, hitPoint.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
                clone.transform.position = new Vector2(hitPoint.position.x, hitPoint.position.y);
                if(bowEquipped){
                    Destroy(gameObject, 0.001f);
                }
            }
        }
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
