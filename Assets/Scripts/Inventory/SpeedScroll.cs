using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "SpeedScroll", menuName = "Items/SpeedScroll")]
public class SpeedScroll : Item 
{
   public int speedBonusModifier = 2;
   public int duration;

   public override void Use(){
       GameObject player = Inventory.instance.player;
       PlayerController pc = player.GetComponent<PlayerController>();
       
       pc.SetMoveSpeedForADuration(speedBonusModifier, duration);
       Inventory.instance.Remove(this);
   }
}
