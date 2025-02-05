﻿using UnityEngine;

public class MushroomConsumable : MonoBehaviour
{
	[SerializeField] private float m_hpHeal = 0;
	[SerializeField] private int m_hpDamage = 0;
	[SerializeField] private int m_manaHeal = 0;
	[SerializeField] private int m_speedBuffDuration = 0;

	private string m_playerTag = "Player";

	private void OnTriggerEnter2D(Collider2D _collision)
	{
		if (_collision.CompareTag(m_playerTag))
		{
			if (_collision.TryGetComponent(out PlayerController2D playerController) 
				&& _collision.TryGetComponent(out Player player)
				&& _collision.TryGetComponent(out PlayerManaManager playerManaManager))
			{
				if (m_manaHeal != 0)
				{
					if (playerManaManager.playerCurrentMana != playerManaManager.playerMaxMana)
					{
						playerManaManager.HealMana(m_manaHeal);
						MessagingSystem.Publish(MessageType.ManaPotionPop);
						gameObject.SetActive(false);
					}
				}
				else if (m_hpHeal != 0)
				{
					if (player.PlayerHealthManager.CanHeal())
					{
						player.PlayerHealthManager.Heal(m_hpHeal);
						gameObject.SetActive(false);
					}
				}
				else if (!playerController.IsSpeedBonusOnCD() && m_speedBuffDuration != 0)
				{
					playerController.SetMoveSpeedBonuses(2, m_speedBuffDuration, 2);
					gameObject.SetActive(false);
				}
				else if (m_hpDamage != 0)
				{
					player.PlayerHealthManager.TakeDamage(m_hpDamage);
					gameObject.SetActive(false);
				}
			}
		}
	}
}
