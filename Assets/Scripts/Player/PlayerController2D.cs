using UnityEngine;

public enum PlayerState
{
	Idle,
	Normal,
	Rolling,
	Frozen,
}

public class PlayerController2D : MonoBehaviour
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

	#region Singleton
	public static PlayerController2D Instance { get; private set; }

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
	#endregion

	private void Start()
	{
		PlayerState = PlayerState.Normal;
		PlayerHealthManager.PlayerDead += PlayerDied;
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

		if (m_dashTime >= 0 || PlayerManaManager.Instance.playerCurrentMana <= m_dashManaCost)
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
					if (m_dashTime <= 0 && PlayerManaManager.Instance.playerCurrentMana >= m_dashManaCost)
					{
						PlayerManaManager.Instance.TakeMana(m_dashManaCost);
						SFXManager.Instance.PlaySound(SFXManager.Instance.dash);
						PlayerState = PlayerState.Rolling;
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
				PlayerManaManager.Instance.playerCurrentMana >= m_dashManaCost)
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

			PlayerManaManager.Instance.TakeMana(m_dashManaCost);
			SFXManager.Instance.PlaySound(SFXManager.Instance.dash);
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
			if (SpeedNotOnCooldown())
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

	public bool SpeedNotOnCooldown()
	{
		if (MoveBonusCooldown <= 0)
			return true;
		else return false;
	}

	public void FrezePlayer()
	{
		m_playerFrozen = true;
	}

	public void UnFreezePlayer()
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

	private void PlayerDied()
	{
		FrezePlayer();
		m_animator.SetBool("Dead", true);
	}

	public void PlayerAlive()
	{
		UnFreezePlayer();
		m_animator.SetBool("Dead", false);
	}
}
