using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Heals the player mana for 20 points if mana isn't full
public class MushroomConsumable : MonoBehaviour
{
   public int heal = 5;

   void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            GameObject player = Inventory.instance.player;
            PlayerManaManager pMm = player.GetComponent<PlayerManaManager>();
       
            if(pMm.playerCurrentMana != pMm.playerMaxMana){
                pMm.HealMana(heal);
                gameObject.SetActive(false);
            }   
        }
   }
}
