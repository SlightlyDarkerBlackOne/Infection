using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

    public int MaxHealth;
    public int CurrentHealth;
    public int expToGive;
    public string enemyQuestName;
    public event EventHandler OnEnemyDeath;

    // Use this for initialization
    void Start () {
        CurrentHealth = MaxHealth;
}
	
	// Update is called once per frame
	void Update () {
		if(CurrentHealth <= 0)
        {
            OnEnemyDeath?.Invoke(this, EventArgs.Empty);
            QuestManager.Instance.enemyKilled = enemyQuestName;
            gameObject.SetActive(false);
            //Destroy(gameObject);
            PlayerStats.Instance.AddExperience(expToGive);
            SFXManager.Instance.PlaySound(SFXManager.Instance.enemyDead);
            UIManager.Instance.MonsterKilled();
        }
	}

    public void HurtEnemy(int damageToGive)
    {
        CurrentHealth -= damageToGive;
    }

    public void SetMaxHealth()
    {
        CurrentHealth = MaxHealth;
    }
}
