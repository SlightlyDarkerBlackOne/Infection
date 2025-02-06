using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	public Item item;
	private string m_playerTag = "Player";

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag(m_playerTag))
		{
			PickUpItem();
		}

	}

	private void PickUpItem()
	{
		//if (Inventory.InventoryNotFull())
		//{
		//	Inventory.Add(item);
		//	gameObject.SetActive(false);
		//	MessagingSystem.Publish(MessageType.ItemPickedUp);
		//}
	}	
}
