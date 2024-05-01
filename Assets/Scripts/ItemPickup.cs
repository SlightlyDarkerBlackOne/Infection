using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	public Item item;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			if (Inventory.instance.InventoryNotFull())
			{
				Inventory.instance.Add(item);
				gameObject.SetActive(false);

				MessagingSystem.Publish(MessageType.ItemPickedUp);
			}
		}

	}
}
