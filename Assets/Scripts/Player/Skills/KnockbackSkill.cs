using UnityEngine;
using System.Collections;
public class KnockbackSkill : MonoBehaviour
{
    [Header("Knockback Settings")]
    [SerializeField] private float m_baseKnockbackForce = 5f;
    [SerializeField] private float m_damageKnockbackMultiplier = 0.5f;
    [SerializeField] private float m_knockbackDuration = 0.2f;

    private bool m_isKnockbackEnabled;
    private float m_knockbackMultiplier = 1f;

    public void EnableKnockback()
    {
        m_isKnockbackEnabled = true;
    }

    public void SetKnockbackMultiplier(float _multiplier)
    {
        m_knockbackMultiplier = _multiplier;
    }

    public void ApplyKnockback(GameObject _target, float _damage, Vector2 _direction)
    {
        if (!m_isKnockbackEnabled) return;

        if (_target.TryGetComponent<Rigidbody2D>(out var rb))
        {
            Vector2 knockbackDirection = _direction.normalized;
            StartCoroutine(ApplyKnockbackForce(rb, knockbackDirection * CalculateKnockbackForce(_damage)));
        }
    }

    private float CalculateKnockbackForce(float _damage)
    {
        return (m_baseKnockbackForce + (_damage * m_damageKnockbackMultiplier)) * m_knockbackMultiplier;
    }

    private IEnumerator ApplyKnockbackForce(Rigidbody2D _rb, Vector2 _force)
    {
        Vector2 originalVelocity = _rb.velocity;
        
        _rb.velocity = _force;
        
        yield return new WaitForSeconds(m_knockbackDuration);
        
        _rb.velocity = originalVelocity;
    }
} 