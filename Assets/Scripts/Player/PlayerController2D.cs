using System;
using UnityEngine;

public enum PlayerState
{
	Idle,
	Normal,
	Rolling,
	Frozen,
}

public class PlayerController2D : MessagingBehaviour
{
	public PlayerState PlayerState { get; private set; }
	public float MoveBonusCooldown { get; private set; }

	[SerializeField] private float m_moveSpeed = 2.64f;
	[SerializeField] private float m_speedBonusModifier = 1;
	[SerializeField] private float m_dashSpeed = 2;
	[SerializeField] private int m_dashManaCost = 5;
	[SerializeField] private float m_startDashTime = 0.72f;
	[SerializeField] private float m_rollSpeed = 19;
	[SerializeField] private float m_startTimeBtwTrail = 0.1f;
	[SerializeField] private LayerMask m_dashLayerMask;
	[SerializeField] private GameObject m_trailEffect;
	[Space(20)]
	[SerializeField] private Rigidbody2D m_rigidBody2D;
	[SerializeField] private Animator m_animator;

	[SerializeField] private PlayerManaManager m_playerManaManager;

	private float m_rollSpeedOngoing;
	private float m_timeBtwTrail;
	private float m_dashTime;
	private float m_moveBonusDuration;
	private float m_startMoveBonusCooldown;

	private Vector3 m_moveDir;
	private Vector3 m_lastMoveDir;
	private Vector3 m_rollDir;

	private bool m_playerMoving;
	private bool m_playerFrozen = false;
	private bool m_grantMoveBonus;
	private bool m_isDashButtonDown;

	private void Awake()
	{
		Subscribe(MessageType.FreezePlayer, OnFreezePlayer);
		Subscribe(MessageType.PlayerDied, PlayerDied);
		Subscribe(MessageType.TeleportPlayer, OnTeleportPlayer);
	}

	private void Start()
	{
		PlayerState = PlayerState.Normal;
	}

	private void Update()
	{
		Move();
		SetAnimations();
		TrailEffect();
		SetMoveSpeedForADuration();

		if (Input.GetKeyDown(KeyCode.F))
		{
			m_isDashButtonDown = true;
		}

		if (m_dashTime >= 0 || m_playerManaManager.playerCurrentMana <= m_dashManaCost)
		{
			m_isDashButtonDown = false;
		}

		m_dashTime -= Time.deltaTime;

		if (m_playerFrozen)
		{
			PlayerState = PlayerState.Idle;
			m_playerMoving = false;
		}
	}

	private void FixedUpdate()
	{
		switch (PlayerState)
		{
			case PlayerState.Idle:
				m_rigidBody2D.velocity = Vector2.zero;
				break;
			case PlayerState.Normal:
				m_rigidBody2D.velocity = m_moveDir * m_moveSpeed;
				Dash();
				break;
			case PlayerState.Rolling:
				m_rigidBody2D.velocity = m_rollDir * m_rollSpeed;
				break;
		}
	}

	private void OnTeleportPlayer(object _position)
	{
		if (_position is Vector3 position)
		{
			transform.position = position;
		}
	}

	private void Move()
	{
		switch (PlayerState)
		{
			case PlayerState.Idle:
				if (!m_playerFrozen)
					PlayerState = PlayerState.Normal;
				break;
			case PlayerState.Normal:
				float moveX = 0f;
				float moveY = 0f;

				if (Input.GetKey(KeyCode.W))
				{
					moveY = +1f;
				}
				if (Input.GetKey(KeyCode.S))
				{
					moveY = -1f;
				}
				if (Input.GetKey(KeyCode.A))
				{
					moveX = -1f;
				}
				if (Input.GetKey(KeyCode.D))
				{
					moveX = +1f;
				}
				if (moveX != 0 || moveY != 0)
				{
					m_playerMoving = true;
					m_lastMoveDir = m_moveDir;
				}
				else
				{
					m_playerMoving = false;
				}
				m_moveDir = new Vector3(moveX, moveY).normalized;

				if (Input.GetKeyDown(KeyCode.Space))
				{
					m_rollDir = m_lastMoveDir;
					m_rollSpeedOngoing = m_rollSpeed;
					if (m_dashTime <= 0 && m_playerManaManager.playerCurrentMana >= m_dashManaCost)
					{
						m_playerManaManager.TakeMana(m_dashManaCost);
						MessagingSystem.Publish(MessageType.PlayerDash);
						PlayerState = PlayerState.Rolling;
					}
					else if (m_playerManaManager.playerCurrentMana < m_dashManaCost)
					{
						MessagingSystem.Publish(MessageType.NotEnoughMana);
					}
				}
				break;
			case PlayerState.Rolling:
				Roll();
				break;
		}
	}

	private void Roll()
	{
		float rollSpeedDropMultiplier = 5f;
		m_rollSpeedOngoing -= m_rollSpeedOngoing * rollSpeedDropMultiplier * Time.deltaTime;
		float rollSpeedMinimum = 10f;
		if (m_rollSpeedOngoing < rollSpeedMinimum)
		{
			PlayerState = PlayerState.Normal;
		}
	}

	private void Dash()
	{
		if (m_isDashButtonDown && m_dashTime <= 0 &&
				m_playerManaManager.playerCurrentMana >= m_dashManaCost)
		{
			m_dashTime = m_startDashTime;
			Vector3 dashPosition = transform.position + m_lastMoveDir * m_dashSpeed;

			RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, m_lastMoveDir,
					m_dashSpeed, m_dashLayerMask);
			if (raycastHit2D.collider != null)
			{
				dashPosition = raycastHit2D.point;
			}

			m_rigidBody2D.MovePosition(dashPosition);

			m_playerManaManager.TakeMana(m_dashManaCost);
			MessagingSystem.Publish(MessageType.PlayerDash);
			m_isDashButtonDown = false;
		}
	}
	private void SetAnimations()
	{
		m_animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
		m_animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
		m_animator.SetBool("PlayerMoving", m_playerMoving);
		m_animator.SetFloat("LastMoveX", m_lastMoveDir.x);
		m_animator.SetFloat("LastMoveY", m_lastMoveDir.y);
	}

	private void TrailEffect()
	{
		if (m_playerMoving)
		{
			if (m_timeBtwTrail <= 0)
			{
				GameObject effect = Instantiate(m_trailEffect, transform.position, Quaternion.identity);
				Destroy(effect, 2f);
				m_timeBtwTrail = m_startTimeBtwTrail;
			}
			else
			{
				m_timeBtwTrail -= Time.deltaTime; ;
			}
		}
	}

	private void SetMoveSpeedForADuration()
	{
		if (m_grantMoveBonus)
		{
			if (!IsSpeedBonusOnCD())
			{
				MoveBonusCooldown = m_startMoveBonusCooldown;
				m_moveSpeed *= m_speedBonusModifier;
			}
			if (m_moveBonusDuration <= 0)
			{
				m_moveSpeed /= m_speedBonusModifier;
				m_grantMoveBonus = false;
			}
			m_moveBonusDuration -= Time.deltaTime;
		}
		MoveBonusCooldown -= Time.deltaTime;
	}

	public bool IsSpeedBonusOnCD()
	{
		if (MoveBonusCooldown <= 0)
			return false;
		else return true;
	}

	private void OnFreezePlayer(object _obj)
	{
		if(_obj is bool shouldFreezePlayer)
		{
			m_playerFrozen = shouldFreezePlayer;
		}
	}

	private void FrezePlayer()
	{
		m_playerFrozen = true;
	}

	private void UnFreezePlayer()
	{
		m_playerFrozen = false;
	}

	public void IncreaseMoveSpeed()
	{
		m_moveSpeed += m_moveSpeed / 10;
	}

	public void SetMoveSpeedBonuses(float speedModifier, float duration, float cooldown)
	{
		m_grantMoveBonus = true;
		m_speedBonusModifier = speedModifier;
		m_moveBonusDuration = duration;
		m_startMoveBonusCooldown = cooldown;
	}

	private void PlayerDied(object _obj)
	{
		FrezePlayer();
		m_animator.SetBool("Dead", true);
	}

	public void PlayerAlive()
	{
		UnFreezePlayer();
		Time.timeScale = 1f;
		m_animator.SetBool("Dead", false);
	}
}
