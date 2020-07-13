using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "SpeedScroll", menuName = "Items/SpeedScroll")]
public class SpeedScroll : Item 
{
    public int speedBonusModifier = 2;
    public int duration;
    public int cooldown = 5;

    public override void Use(){
        GameObject player = PlayerController.Instance.gameObject;
        PlayerController pc = player.GetComponent<PlayerController>();
       
        pc.SetMoveSpeedBonuses(speedBonusModifier, duration, cooldown);

        Inventory.instance.Remove(this);
   }
}
