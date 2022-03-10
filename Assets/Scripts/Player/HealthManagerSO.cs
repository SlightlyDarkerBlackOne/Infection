using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthManagerSO : ScriptableObject
{
    protected float currentHealth;
    [SerializeField]
    private float maxHealth;

    private bool flashActive;
    public float flashLength;
    private float flashCounter;

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
        flashActive = true;
        flashCounter = flashLength;

        HealthChangedEvent?.Invoke(currentHealth, maxHealth);
    }
    //Flashing the unit sprite with white when it takes damage
    protected virtual void Flash(SpriteRenderer spriteRenderer) {
        if (flashActive) {
            if (flashCounter > flashLength * 0.66f) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
            } else if (flashCounter > flashLength * 0.33f) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            } else if (flashCounter > 0) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
            } else {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
                flashActive = false;
            }

            flashCounter -= Time.deltaTime;
        }
    }
}
