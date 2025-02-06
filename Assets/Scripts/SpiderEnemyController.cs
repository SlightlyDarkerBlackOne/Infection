using UnityEngine;
using System.Collections;

public class SpiderEnemyController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float m_baseMovementSpeed = 3f;
    [SerializeField] private float m_lungeSpeed = 8f;
    [SerializeField] private float m_retreatImpulseForce = 15f;
    
    [Header("Behavior Settings")]
    [SerializeField] private float m_detectionRange = 6f;
    [SerializeField] private float m_lungeRange = 4f;
    [SerializeField] private float m_attackRange = 1f;
    [SerializeField] private float m_erraticMovementInterval = 0.3f;
    [SerializeField] private float m_preLungeDelay = 0.5f;
    [SerializeField] private float m_postAttackCooldown = 1.5f;
    
    [Header("Attack Settings")]
    [SerializeField] private int m_attackDamage = 10;
    [SerializeField] private Transform m_attackPoint;
    [SerializeField] private LayerMask m_playerLayer;
    [SerializeField] private PlayerHealthManager m_playerHealthManager;

    private Rigidbody2D m_rb;
    private Animator m_animator;
    private Transform m_playerTransform;
    
    private Vector2 m_moveDirection;
    private SpiderState m_currentState;
    private float m_erraticTimer;
    private float m_attackCooldown;
    private bool m_isLunging;
    private Vector2 m_lungeTarget;
    private BoxCollider2D m_collider;

    private enum SpiderState
    {
        Idle,
        Patrolling,
        Stalking,
        Lunging,
        Attacking,
        Retreating
    }

    private static readonly int s_attackTrigger = Animator.StringToHash("Attack");
    private static readonly int s_prepareTrigger = Animator.StringToHash("Idle");
    private static readonly int s_moveTrigger = Animator.StringToHash("Move");

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_collider = GetComponent<BoxCollider2D>();
        m_playerTransform = FindObjectOfType<PlayerController2D>().transform;
        m_currentState = SpiderState.Idle;
        
        if (m_animator != null)
            m_animator.SetTrigger(s_prepareTrigger);
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, m_playerTransform.position);
        UpdateSpiderState(distanceToPlayer);
        UpdateTimers();
    }

    private void FixedUpdate()
    {
        switch (m_currentState)
        {
            case SpiderState.Patrolling:
                HandlePatrolling();
                break;
            case SpiderState.Stalking:
                HandleStalking();
                break;
            case SpiderState.Lunging:
                HandleLunging();
                break;
            case SpiderState.Retreating:
                HandleRetreating();
                break;
        }
    }

    private void UpdateSpiderState(float _distanceToPlayer)
    {
        if (m_attackCooldown > 0) return;

        if (_distanceToPlayer <= m_attackRange && m_currentState == SpiderState.Lunging && m_currentState != SpiderState.Attacking)
        {
            StartCoroutine(PerformAttack());
        }
        else if (_distanceToPlayer <= m_lungeRange && m_currentState != SpiderState.Lunging)
        {
            StartCoroutine(PrepareLunge()); 
        }
        else if (_distanceToPlayer <= m_detectionRange && m_currentState != SpiderState.Stalking)
        {
            m_currentState = SpiderState.Stalking;
        }
        else if (_distanceToPlayer > m_detectionRange && m_currentState != SpiderState.Patrolling)
        {
            m_currentState = SpiderState.Patrolling;
        }
    }

    private void HandlePatrolling()
    {
        if (m_erraticTimer <= 0)
        {
            m_moveDirection = Random.insideUnitCircle.normalized;
            m_erraticTimer = m_erraticMovementInterval;
        }
        
        m_rb.velocity = m_moveDirection * m_baseMovementSpeed;
        
        if (m_animator != null)
            m_animator.SetTrigger(s_moveTrigger);
    }

    private void HandleStalking()
    {
        if (m_erraticTimer <= 0)
        {
            Vector2 directionToPlayer = ((Vector2)m_playerTransform.position - m_rb.position).normalized;
            m_moveDirection = directionToPlayer + (Random.insideUnitCircle * 0.5f);
            m_moveDirection.Normalize();
            m_erraticTimer = m_erraticMovementInterval * 0.5f;
        }

        m_rb.velocity = m_moveDirection * m_baseMovementSpeed;
        
        if (m_animator != null)
            m_animator.SetTrigger(s_moveTrigger);
    }

    private void HandleLunging()
    {
        if (m_isLunging)
        {
            m_rb.velocity = (m_lungeTarget - m_rb.position).normalized * m_lungeSpeed;
            
            if (m_animator != null)
                m_animator.SetTrigger(s_moveTrigger);
        }
    }

    private void HandleRetreating()
    {
        // Apply retreat impulse
        Vector2 retreatDirection = (m_rb.position - (Vector2)m_playerTransform.position).normalized;
        m_rb.velocity = Vector2.zero; // Clear any existing velocity
        m_rb.AddForce(retreatDirection * m_retreatImpulseForce, ForceMode2D.Impulse);

        // Continue retreating
        m_rb.velocity = retreatDirection * m_baseMovementSpeed; // Optional: maintain movement speed while retreating
        
        if (m_animator != null)
            m_animator.SetTrigger(s_moveTrigger);
    }

    private IEnumerator PrepareLunge()
    {
        m_currentState = SpiderState.Lunging;
        m_rb.velocity = Vector2.zero;
        
        // Enable trigger to prevent collision during lunge
        if (m_collider != null)
            m_collider.isTrigger = true;
        
        // Play prepare animation
        if (m_animator != null)
            m_animator.SetTrigger(s_prepareTrigger);

        yield return new WaitForSeconds(m_preLungeDelay);

        m_lungeTarget = m_playerTransform.position;
        m_isLunging = true;
    }

    private IEnumerator PerformAttack()
    {
        m_currentState = SpiderState.Attacking;
        m_isLunging = false;
        m_rb.velocity = Vector2.zero;

        if (m_animator != null)
            m_animator.SetTrigger(s_attackTrigger);

        // Perform damage
        Collider2D hitPlayer = Physics2D.OverlapCircle(m_attackPoint.position, m_attackRange, m_playerLayer);
        if (hitPlayer != null)
        {
            m_playerHealthManager.TakeDamage(m_attackDamage);
        }

        // Start retreating state
        m_attackCooldown = m_postAttackCooldown;
        m_currentState = SpiderState.Retreating;

        // Disable trigger to restore normal collisions
        if (m_collider != null)
            m_collider.isTrigger = false;

        // Wait for retreat duration
        yield return new WaitForSeconds(m_postAttackCooldown);
        
        if (m_currentState == SpiderState.Retreating)
        {
            m_currentState = SpiderState.Stalking;
        }
    }

    private void UpdateTimers()
    {
        m_erraticTimer -= Time.deltaTime;
        m_attackCooldown -= Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_detectionRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_lungeRange);
        
        if (m_attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(m_attackPoint.position, m_attackRange);
        }
    }
} 