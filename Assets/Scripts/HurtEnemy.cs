﻿using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
	//weapon dmg
	public int damageToGive;

	//full dmg
	private int currentDamage;

	public int critChance;
	public int critMultiplier;
	private int crit;

	public GameObject damageBurst;
	public GameObject arrowBreak;
	public Transform hitPoint;
	public GameObject damageNumber;

	public bool yoyoEquiped;
	public bool bowEquipped;
	public bool bladeVortex;

	[SerializeField] private PlayerStatsSO m_playerStatsSO;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			if (yoyoEquiped || bowEquipped || bladeVortex)
			{
				currentDamage = damageToGive + m_playerStatsSO.currentAttack;
				currentDamage = Crit(currentDamage); //Critical Strike

				other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(currentDamage);
				MessagingSystem.Publish(MessageType.EnemyHit);

				GameObject burst = Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
				Destroy(burst, 2f);
				var clone = (GameObject)Instantiate(damageNumber, hitPoint.position, Quaternion.Euler(Vector3.zero));
				clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
				clone.transform.position = new Vector2(hitPoint.position.x, hitPoint.position.y);
				Destroy(clone, 2f);
				if (bowEquipped)
				{
					Destroy(gameObject, 0.001f);
				}
			}
		}
		else if (other.gameObject.tag == "Breakable")
		{
			if (bowEquipped && other.gameObject.GetComponent<Sign>().isBroken == false)
			{
				Destroy(gameObject, 0.001f);
			}

			other.gameObject.GetComponent<Sign>().Break();
		}
		else if (other.gameObject.tag == "Solid" && bowEquipped)
		{
			MessagingSystem.Publish(MessageType.BowHitSolid);
			GameObject arrowBreakEffect = Instantiate(arrowBreak, hitPoint.position, hitPoint.rotation);
			Destroy(arrowBreakEffect, 2f);
			Destroy(gameObject, 0.001f);
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			if (yoyoEquiped || bowEquipped)
			{
				currentDamage = damageToGive + m_playerStatsSO.currentAttack;
				currentDamage = Crit(currentDamage); //Critical Strike

				other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(currentDamage);
				MessagingSystem.Publish(MessageType.EnemyHit);

				GameObject burst = Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
				Destroy(burst, 2f);
				var clone = (GameObject)Instantiate(damageNumber, hitPoint.position, Quaternion.Euler(Vector3.zero));
				clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
				clone.transform.position = new Vector2(hitPoint.position.x, hitPoint.position.y);
				Destroy(clone, 2f);
				if (bowEquipped)
				{
					Destroy(gameObject, 0.001f);
				}
			}
		}
	}

	private int Crit(int damage)
	{
		crit = Random.Range(0, 100);
		if (crit > (100 - critChance))
			return currentDamage *= critMultiplier;
		else
		{
			return damage;
		}
	}
}
