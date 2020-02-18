using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Consumable", menuName = "Items/Consumable")]
public class Consumable : Item 
{
   public int heal = 0;

   public override void Use(){
       GameObject player = Inventory.instance.player;
       PlayerHealthManager phm = player.GetComponent<PlayerHealthManager>();
       
       phm.Heal(heal);
       Inventory.instance.Remove(this);
   }
}
