using UnityEngine;

[CreateAssetMenu(fileName = "Consumable", menuName = "Items/Consumable")]
public class Consumable : Item
{
	public int heal = 0;

	public override void Use()
	{
		PlayerHealthManager phm = FindObjectOfType<Player>().PlayerHealthManager;

		if (phm.CanHeal())
		{
			phm.Heal(heal);
			Inventory.instance.Remove(this);
		}
		else
		{
			//Show health is full text - use FloatingText
			//Add sound effect
		}
	}
}
