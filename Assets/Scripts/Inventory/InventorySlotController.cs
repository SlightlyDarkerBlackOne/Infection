using UnityEngine;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour
{
	public Item item;

	public void UpdateInfo()
	{
		Image displayImage = transform.Find("Image").GetComponent<Image>();

		if (item)
		{
			displayImage.sprite = item.icon;
			displayImage.color = Color.white;
		}
		else
		{
			displayImage.sprite = null;
			displayImage.color = Color.clear;
		}
	}

	public void Use()
	{
		if (item)
		{
			item.Use();
		}
	}
}
