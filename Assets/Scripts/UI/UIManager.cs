using UnityEngine;
using UnityEngine.UI;

public class UIManager : MessagingBehaviour
{
	public Image healthBar;
	public Image manaBar;
	public Text HPText;
	public Text ManaText;
	public Text levelText;
	public Slider xpBar;

	public Text numberOfSlainBugs;
	private int numberOfSlainBugsCounter = 0;
	[SerializeField]
	private Animator textPopup;
	[SerializeField]
	private GameObject[] checkmarks;
	[SerializeField]
	private GameObject deadPanel;

	[SerializeField] private PlayerManaManager m_playerMana;
	[SerializeField] private PlayerStatsSO m_playerStatsSO;

	private void Awake()
	{
		Subscribe(MessageType.PlayerHealthChanged, OnPlayerHealthChanged);
		Subscribe(MessageType.QuestFinished, ShowQuestCheckmark);
		Subscribe(MessageType.PlayerDied, ShowDeathScreen);
		Subscribe(MessageType.EnemyKilled, MonsterKilled);
	}

	private void OnPlayerHealthChanged(object _obj)
	{
		if (_obj is HealthInfo healthInfo)
		{
			healthBar.fillAmount = healthInfo.CurrentHealth / healthInfo.MaxHealth;
			HPText.text = "HP: " + healthInfo.CurrentHealth + "/" + healthInfo.MaxHealth;
		}
	}

	private void Start()
	{
		numberOfSlainBugs.text = "0";
	}

	private void Update()
	{
		UpdateUIElements();
	}

	private void UpdateUIElements()
	{
		manaBar.fillAmount = m_playerMana.playerCurrentMana / m_playerMana.playerMaxMana;
		ManaText.text = "Mana: " + m_playerMana.playerCurrentMana + "/" + m_playerMana.playerMaxMana;

		levelText.text = "Lvl: " + m_playerStatsSO.currentLevel;

		numberOfSlainBugs.text = numberOfSlainBugsCounter.ToString();

		//xpBar.value = m_playerStatsSO.currentExp / thePS.toLevelUp[thePS.currentLevel];
	}

	public void MonsterKilled(object obj)
	{
		numberOfSlainBugsCounter++;
		textPopup.SetTrigger("TextPopUp");
	}

	private void ShowQuestCheckmark(object _obj)
	{
		if (_obj is int questNumber)
		{
			checkmarks[questNumber].GetComponent<Animator>().SetTrigger("Show");
		}
	}

	private void ShowDeathScreen(object _obj)
	{
		deadPanel.GetComponent<Animator>().SetBool("isShowing", true);
	}

	public void HideDeathScreen()
	{
		deadPanel.GetComponent<Animator>().SetBool("isShowing", false);
	}
}
