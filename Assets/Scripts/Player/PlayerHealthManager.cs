﻿using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHealthManagerScriptableObject", menuName = "ScriptableObjects/Player Health Manager")]
public class PlayerHealthManager : HealthManagerSO
{
	[SerializeField]
	private float deathScreenDelay = 1f;

	private void Awake()
	{
		m_healthInfo.MaxHealth = 70f;
	}

	protected override void Die()
	{
		MessagingSystem.Publish(MessageType.PlayerDied);

		SetToMaxHealth();
	}

	public override void TakeDamage(int damageToGive)
	{
		base.TakeDamage(damageToGive);
		MessagingSystem.Publish(MessageType.PlayerDamaged);
	}

	public override void Heal(float healAmount)
	{
		base.Heal(healAmount);
		MessagingSystem.Publish(MessageType.PlayerHealed);
	}
}
