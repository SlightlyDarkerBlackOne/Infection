using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtPlayer : MonoBehaviour 
{
    [Header("Configuration")]
    [SerializeField] private EnemyConfigurationSO m_enemyConfig;
    [SerializeField] private PlayerStatsSO m_playerStatsSO;
    [SerializeField] private GameObject m_damageNumberPrefab;
    [SerializeField] private bool m_isShurikenTrap;

    [Header("Runtime Variables")]
    private float m_currentCooldown;
    private int m_currentDamage;

    private void Update()
    {
        UpdateCooldown();
    }

    private void UpdateCooldown()
    {
        if (m_currentCooldown > 0)
        {
            m_currentCooldown -= Time.deltaTime;
            m_currentCooldown = Mathf.Max(0f, m_currentCooldown);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        
        if (m_currentCooldown <= 0)
        {
            ApplyDamageToPlayer(collision.gameObject);
            SpawnDamageNumber(collision.transform.position);
            ResetCooldown();
        }

        if (m_isShurikenTrap)
        {
            ApplyKnockback(collision);
        }
    }

    private void ApplyDamageToPlayer(GameObject player)
    {
        if (!player.TryGetComponent<Player>(out var playerComponent)) return;
        
        m_currentDamage = CalculateDamage();
        playerComponent.PlayerHealthManager.TakeDamage(m_currentDamage);
    }

    private void SpawnDamageNumber(Vector3 position)
    {
        GameObject damageNumber = Instantiate(m_damageNumberPrefab, position, Quaternion.identity);
        if (damageNumber.TryGetComponent<FloatingNumbers>(out var floatingNumber))
        {
            floatingNumber.damageNumber = m_currentDamage;
        }
        Destroy(damageNumber, 2f);
    }

    private void ResetCooldown()
    {
        m_currentCooldown = m_enemyConfig.coolDownBetweenHits;
    }

    private void ApplyKnockback(Collider2D collision)
    {
        if (!collision.TryGetComponent<Rigidbody2D>(out var playerRb)) return;

        Vector2 knockbackDirection = (playerRb.transform.position - transform.position).normalized;
        Vector2 knockbackForce = knockbackDirection * m_enemyConfig.knockbackMultiplier;
        playerRb.AddForce(knockbackForce, ForceMode2D.Impulse);
    }

    public int CalculateDamage()
    {
        int baseDamage = CalculateCriticalDamage(m_enemyConfig.damageToGive);
        int finalDamage = baseDamage - m_playerStatsSO.currentDefense;
        return Mathf.Max(1, finalDamage);
    }

    private int CalculateCriticalDamage(int baseDamage)
    {
        bool isCritical = Random.Range(0, 100) > (100 - m_enemyConfig.critChance);
        return isCritical ? baseDamage * m_enemyConfig.critMultiplier : baseDamage;
    }
}
