using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
	[SerializeField] private RangedEnemyConfig m_rangedEnemyConfig;
	[SerializeField] private bool m_boss;

	private float m_timeBtwShots;
	private Transform m_playerTransform;
	private bool m_bossMusicStarted = false;

	private void Start()
	{
		m_playerTransform = FindObjectOfType<PlayerController2D>().transform;
		m_timeBtwShots = m_rangedEnemyConfig.startTimeBtwShots;
	}

	private void Update()
	{
		HandleMovement();
		HandleShooting();
	}

	private void HandleMovement()
	{
		float distanceToPlayer = Vector2.Distance(transform.position, m_playerTransform.position);

		if (IsInChasingRange(distanceToPlayer))
		{
			ChasePlayer();
		}
		else if (IsInStoppingRange(distanceToPlayer))
		{
			StopMoving();
			if (m_boss)
			{
				StartBossFight();
			}
		}
		else if (IsInRetreatRange(distanceToPlayer))
		{
			RetreatFromPlayer();
		}
	}

	private void HandleShooting()
	{
		float distanceToPlayer = Vector2.Distance(transform.position, m_playerTransform.position);
		
		if (m_timeBtwShots <= 0 && IsInStoppingRange(distanceToPlayer))
		{
			Shoot();
		}
		else
		{
			m_timeBtwShots -= Time.deltaTime;
		}
	}

	private bool IsInChasingRange(float _distance) =>
		_distance > m_rangedEnemyConfig.stoppingDistance && _distance < m_rangedEnemyConfig.chasingDistance;

	private bool IsInStoppingRange(float _distance) =>
		_distance < m_rangedEnemyConfig.stoppingDistance && _distance > m_rangedEnemyConfig.retreatDistance;

	private bool IsInRetreatRange(float _distance) =>
		_distance < m_rangedEnemyConfig.retreatDistance;

	private void ChasePlayer() =>
		transform.position = Vector2.MoveTowards(transform.position,
			m_playerTransform.position, m_rangedEnemyConfig.speed * Time.deltaTime);

	private void StopMoving() =>
		transform.position = transform.position;

	private void RetreatFromPlayer() =>
		transform.position = Vector2.MoveTowards(transform.position,
			m_playerTransform.position, -m_rangedEnemyConfig.speed * Time.deltaTime);

	private void Shoot()
	{
		Instantiate(m_rangedEnemyConfig.projectile, transform.position, Quaternion.identity);
		m_timeBtwShots = m_rangedEnemyConfig.startTimeBtwShots;
	}

	private void StartBossFight()
	{
		//Start boss music
		if (m_bossMusicStarted != true)
		{
			MessagingSystem.Publish(MessageType.BossSpawned);
			m_bossMusicStarted = true;
		}

	}

	//Showing chasing and unchasing distance gizmos
	void OnDrawGizmosSelected()
	{
		if (m_rangedEnemyConfig == null) return;
		
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, m_rangedEnemyConfig.retreatDistance);

		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(transform.position, m_rangedEnemyConfig.chasingDistance);

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, m_rangedEnemyConfig.stoppingDistance);
	}
}
