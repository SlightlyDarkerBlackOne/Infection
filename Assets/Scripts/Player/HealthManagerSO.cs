using UnityEngine;

public struct HealthInfo
{
	[HideInInspector]
	public float CurrentHealth;
	public float MaxHealth;
}

public abstract class HealthManagerSO : ScriptableObject
{
	[SerializeField]
	protected HealthInfo m_healthInfo;

	private void OnEnable()
	{
		m_healthInfo.CurrentHealth = m_healthInfo.MaxHealth;
	}

	public void DecreaseHealth(float amount)
	{
		m_healthInfo.CurrentHealth -= amount;
		MessagingSystem.Publish(MessageType.PlayerHealthChanged, m_healthInfo);
	}

	protected void SetToMaxHealth()
	{
		m_healthInfo.CurrentHealth = m_healthInfo.MaxHealth;
		MessagingSystem.Publish(MessageType.PlayerHealthChanged, m_healthInfo);
	}

	public void IncreaseMaxHealth(float newMaxHealth)
	{
		m_healthInfo.MaxHealth = newMaxHealth;
		SetToMaxHealth();
	}

	public bool CanHeal()
	{
		if (m_healthInfo.CurrentHealth <= m_healthInfo.MaxHealth)
		{
			return true;
		}

		return false;
	}

	public virtual void Heal(float healAmount)
	{
		m_healthInfo.CurrentHealth += healAmount;

		if (m_healthInfo.CurrentHealth >= m_healthInfo.MaxHealth)
		{
			SetToMaxHealth();
		}

		MessagingSystem.Publish(MessageType.PlayerHealthChanged, m_healthInfo);
	}

	public virtual void TakeDamage(int damageToGive)
	{
		m_healthInfo.CurrentHealth -= damageToGive;

		if (m_healthInfo.CurrentHealth <= 0)
		{
			Die();
		}

		MessagingSystem.Publish(MessageType.PlayerHealthChanged, m_healthInfo);
	}

	protected abstract void Die();
}
