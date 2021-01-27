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
        GameObject player = PlayerController2D.Instance.gameObject;
        PlayerController2D pc = PlayerController2D.Instance;
        //player.GetComponent<PlayerController>();
        
        if(pc.SpeedNotOnCooldown()) {
            pc.SetMoveSpeedBonuses(speedBonusModifier, duration, cooldown);
            Inventory.instance.Remove(this);
        } else{
            Inventory.instance.GetComponent<ShowCooldown>()
                .ShowFloatingText(this.itemName, Color.yellow);
        }
   }
}
