using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

    public int MaxHealth;
    public int CurrentHealth;

    private PlayerStats thePS;

    public int expToGive;

    public string enemyQuestName;
    private QuestManager theQM;

	// Use this for initialization
	void Start () {
        CurrentHealth = MaxHealth;

        thePS = FindObjectOfType<PlayerStats>();
        theQM = FindObjectOfType<QuestManager>();
    }
	
	// Update is called once per frame
	void Update () {
		if(CurrentHealth <= 0)
        {
            theQM.enemyKilled = enemyQuestName;

            Destroy(gameObject);

            thePS.AddExperience(expToGive);
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
