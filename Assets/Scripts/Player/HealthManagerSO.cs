using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthManagerSO : ScriptableObject
{
    protected float currentHealth;
    [SerializeField]
    private float maxHealth;

    public event Action<float, float> HealthChangedEvent;

    private void OnEnable() {
        currentHealth = maxHealth;
    }

    public void DecreaseHealth(float amount) {
        currentHealth -= amount;
        HealthChangedEvent?.Invoke(currentHealth, maxHealth);
    }
    protected void SetToMaxHealth() {
        currentHealth = maxHealth;
        HealthChangedEvent?.Invoke(currentHealth, maxHealth);
    }
    public void IncreaseMaxHealth(float newMaxHealth) {
        maxHealth = newMaxHealth;
        SetToMaxHealth();
    }
    public bool CanHeal() {
        if(currentHealth <= maxHealth) {
            return true;
        }
        return false;
    }
    public virtual void Heal(float healAmount) {
        currentHealth += healAmount;
        if (currentHealth >= maxHealth) {
            SetToMaxHealth();
        }
        HealthChangedEvent?.Invoke(currentHealth, maxHealth);
    }
    public virtual void TakeDamage(int damageToGive) {
        currentHealth -= damageToGive;
        FlashSprite.Instance.SetCounter();
        if(currentHealth <= 0) {
            Die();
        }

        HealthChangedEvent?.Invoke(currentHealth, maxHealth);
    }

    protected abstract void Die();
}
