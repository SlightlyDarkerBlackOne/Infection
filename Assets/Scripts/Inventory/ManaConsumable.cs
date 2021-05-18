using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ManaConsumable", menuName = "Items/ManaConsumable")]
public class ManaConsumable : Item {
   public int heal = 0;

   public override void Use(){
       GameObject player = PlayerController2D.Instance.gameObject;
       PlayerManaManager pMm = player.GetComponent<PlayerManaManager>();
       
       if(pMm.playerCurrentMana != pMm.playerMaxMana){
           pMm.HealMana(heal);
           Inventory.instance.Remove(this);
           SFXManager.Instance.PlaySound(SFXManager.Instance.manaPotion);
       } 
   }
}