using System;
using UnityEngine;

public struct EnemyInfo
{
	public int ExpToGive;
	public string EnemyQuestName;
}

public class EnemyHealthManager : MonoBehaviour
{
	public int MaxHealth;
	public int CurrentHealth;
	public int expToGive;
	public string enemyQuestName;
	public event EventHandler OnEnemyDeath;

	private EnemyInfo m_enemyInfo;

	void Start()
	{
		CurrentHealth = MaxHealth;

		m_enemyInfo.ExpToGive = expToGive;
		m_enemyInfo.EnemyQuestName = enemyQuestName;
	}

	void Update()
	{
		if (CurrentHealth <= 0)
		{
			OnEnemyDeath?.Invoke(this, EventArgs.Empty);
			MessagingSystem.Publish(MessageType.EnemyKilled, m_enemyInfo);
			gameObject.SetActive(false);
			//Destroy(gameObject);
			//PlayerStats.Instance.AddExperience(expToGive); handle this in player script or playerstatsSO
		}
	}

	public void HurtEnemy(int damageToGive)
	{
		CurrentHealth -= damageToGive;
	}

	public void SetMaxHealth()
	{
		CurrentHealth = MaxHealth;
	}
}
