using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerHealthManagerScriptableObject", menuName = "ScriptableObjects/Player Health Manager")]
public class PlayerHealthManager : HealthManagerSO {

    [SerializeField]
    private float deathScreenDelay = 1f;

    public static event Action PlayerDead;

    protected override void Die(){
        SFXManager.Instance.PlaySound(SFXManager.Instance.playerDead);
        PlayerDead?.Invoke();

        SetToMaxHealth();
        PlayerManaManager.Instance.SetMaxMana();
    }

    public override void TakeDamage(int damageToGive){
        base.TakeDamage(damageToGive);
        SFXManager.Instance.PlaySound(SFXManager.Instance.playerHurt);
    }

    public override void Heal(float healAmount){
        base.Heal(healAmount);
        SFXManager.Instance.PlaySound(SFXManager.Instance.playerHealed);
    }
}
