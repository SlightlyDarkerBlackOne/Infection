using UnityEngine;

public class KnockbackApplier : MessagingBehaviour
{
    [SerializeField] private Player m_player;
    
    private void Awake()
    {
        Subscribe(MessageType.EnemyHit, OnWeaponHit);
    }

    // Call this when weapon hits an enemy
    private void OnWeaponHit(object _enemyHitInfo)
    {
        if(_enemyHitInfo is EnemyHitInfo enemyHitInfo)
        {
            // Calculate direction from player to enemy
            Vector2 knockbackDirection = (enemyHitInfo.enemy.transform.position - transform.position).normalized;
            
            // Apply knockback
            m_player.ApplyKnockbackToTarget(enemyHitInfo.enemy, enemyHitInfo.damage, knockbackDirection);
        }
    }
} 