using UnityEngine;

public class PlayerStats : MonoBehaviour
{

	public int currentLevel;
	public float currentExp;

	public int[] toLevelUp;

	public int[] HPLevels;
	public int[] attackLevels;
	public int[] defenseLevels;

	public int currentHP;
	public int currentAttack;
	public int currentDefense;

	[SerializeField] private PlayerHealthManager m_playerHealthManager;

	void Start()
	{
		currentHP = HPLevels[1];
		currentAttack = attackLevels[1];
		currentDefense = defenseLevels[1];
	}

	void Update()
	{
		if (currentExp >= toLevelUp[currentLevel])
		{
			LevelUp();
		}
	}

	public void AddExperience(int experienceToAdd)
	{
		currentExp += experienceToAdd;
	}

	public void LevelUp()
	{
		currentLevel++;
		currentHP = HPLevels[currentLevel];

		m_playerHealthManager.IncreaseMaxHealth(currentHP);

		//Ako zelimo da mu se doda samo razlika na HP koliko se povecava maxhealth
		//PlayerHealthManager.Instance.playerCurrentHealth += currentHP - HPLevels(currentLevel - 1);

		currentAttack = attackLevels[currentLevel];
		currentDefense = defenseLevels[currentLevel];

		if (currentLevel > 1)
		{
			MessagingSystem.Publish(MessageType.LevelUp);
		}
	}
}
