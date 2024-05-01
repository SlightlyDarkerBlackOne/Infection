using UnityEngine;

public class ShowCooldown : MessagingBehaviour
{
	[SerializeField] private GameObject m_floatingText;
	[SerializeField] private PlayerController2D m_player;

	private float m_lastShownTime;
	private float m_showTextCooldown = 1f;

	private void OnEnable()
	{
		Subscribe(MessageType.ShowCooldown, OnShowCooldown);
	}

	private void OnShowCooldown(object _obj)
	{
		if (_obj is string itemName)
		{
			if (Time.time - m_lastShownTime >= m_showTextCooldown)
			{
				ShowFloatingText(itemName, Color.yellow);
				m_lastShownTime = Time.time;
			}
		}
	}

	public void ShowFloatingText(string itemName, Color color)
	{
		var clone = (GameObject)Instantiate(m_floatingText, m_player.transform.position, Quaternion.Euler(Vector3.zero));
		clone.GetComponent<FloatingText>().displayText.color = color;
		clone.GetComponent<FloatingText>().textToShow = itemName + " is on Cooldown! " + m_player.MoveBonusCooldown;
		clone.transform.position = new Vector2(m_player.transform.position.x, m_player.transform.position.y);
	}
}
