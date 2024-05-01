using UnityEngine;
using UnityEngine.UI;

public class CharacterScreenUI : MonoBehaviour
{
	[SerializeField] Text healthText;
	[SerializeField] Text levelText;
	[SerializeField] Text attackText;
	[SerializeField] Text defenceText;
	[SerializeField] Text monstersSlainText;

	public GameObject characterScreenPanel;

	public static CharacterScreenUI instance;

	[SerializeField] private PlayerStatsSO m_playerStatsSO;

	private void Start()
	{
		instance = this;
	}

	private void Update()
	{
		healthText.text = m_playerStatsSO.currentHP.ToString();
		levelText.text = m_playerStatsSO.currentLevel.ToString();
		attackText.text = m_playerStatsSO.currentAttack.ToString();
		defenceText.text = m_playerStatsSO.currentDefense.ToString();
		monstersSlainText.text = m_playerStatsSO.numberOfSlainBugs.ToString();
	}
}
