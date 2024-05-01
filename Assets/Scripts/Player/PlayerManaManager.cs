using System;
using UnityEngine;

public class PlayerManaManager : MessagingBehaviour
{
	public float playerMaxMana;
	public float playerCurrentMana;

	public float startManaRegenTime = 3;
	[SerializeField]
	private float manaRegenTime;
	public int manaRegen = 1;

	private void Awake()
	{
		Subscribe(MessageType.LevelUp, SetMaxMana);
		Subscribe(MessageType.PlayerDied, SetMaxMana);
	}

	private void Start()
	{
		playerCurrentMana = playerMaxMana;
		manaRegenTime = startManaRegenTime;
	}

	private void Update()
	{
		manaRegenTime -= Time.deltaTime;

		if (manaRegenTime <= 0)
		{
			manaRegenTime = startManaRegenTime;
			HealMana(manaRegen);
		}
	}

	public void HealMana(int manaToAdd)
	{
		playerCurrentMana += manaToAdd;

		if (playerCurrentMana >= playerMaxMana)
		{
			playerCurrentMana = playerMaxMana;
		}
	}

	public void TakeMana(int manaToTake)
	{
		playerCurrentMana -= manaToTake;

		if (playerCurrentMana <= 0)
		{
			playerCurrentMana = 0;

			MessagingSystem.Publish(MessageType.OutOfMana);
		}
	}

	public void SetMaxMana(object _obj)
	{
		playerCurrentMana = playerMaxMana;
	}
}
