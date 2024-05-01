using UnityEngine;

[CreateAssetMenu(fileName = "ManaConsumable", menuName = "Items/ManaConsumable")]
public class ManaConsumable : Item
{
	public int heal = 0;

	public override void Use()
	{
		GameObject player = FindObjectOfType<PlayerController2D>().gameObject;
		PlayerManaManager pMm = player.GetComponent<PlayerManaManager>();

		if (pMm.playerCurrentMana != pMm.playerMaxMana)
		{
			pMm.HealMana(heal);
			Inventory.instance.Remove(this);
			MessagingSystem.Publish(MessageType.ManaPotionPop);
		}
	}
}