using UnityEngine;

[CreateAssetMenu(fileName = "SpeedScroll", menuName = "Items/SpeedScroll")]
public class SpeedScroll : Item
{
	public int speedBonusModifier = 2;
	public int duration;
	public int cooldown = 5;

	public override void Use()
	{
		PlayerController2D pc = FindObjectOfType<PlayerController2D>();

		if (!pc.IsSpeedBonusOnCD())
		{
			pc.SetMoveSpeedBonuses(speedBonusModifier, duration, cooldown);
			//Inventory.instance.Remove(this);
		}
		else
		{
			MessagingSystem.Publish(MessageType.ShowCooldown, itemName);
		}

	}
}
