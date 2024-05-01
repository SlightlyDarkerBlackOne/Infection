using UnityEngine;

public class FlashSprite : MessagingBehaviour
{
	private bool flashActive;
	public float flashLength;
	[HideInInspector]
	public float flashCounter;

	[SerializeField] private PlayerController2D m_playerController2D;

	private void Awake()
	{
		Subscribe(MessageType.PlayerHealthChanged, SetCounter);
	}

	private void Update()
	{
		Flash(m_playerController2D.transform.Find("Animation").GetComponent<SpriteRenderer>());
	}

	//Flashing the unit sprite with white when it takes damage
	public void Flash(SpriteRenderer spriteRenderer)
	{
		if (flashActive)
		{
			if (flashCounter > flashLength * 0.66f)
			{
				spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
			}
			else if (flashCounter > flashLength * 0.33f)
			{
				spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
			}
			else if (flashCounter > 0)
			{
				spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
			}
			else
			{
				spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
				flashActive = false;
			}

			flashCounter -= Time.deltaTime;
		}
	}

	public void SetCounter(object obj)
	{
		flashActive = true;
		flashCounter = flashLength;
	}
}
