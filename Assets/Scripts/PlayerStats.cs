using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public int currentLevel;
    public float currentExp;

    public int[] toLevelUp;

    public int[] HPLevels;
    public int[] attackLevels;
    public int[] defenseLevels;

    public int currentHP;
    public int currentAttack;
    public int currentDefense;

    #region Singleton
    public static PlayerStats Instance{get; private set;}
    void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
    #endregion

	// Use this for initialization
	void Start () {
        currentHP = HPLevels[1];
        currentAttack = attackLevels[1];
        currentDefense = defenseLevels[1];
	}
	
	// Update is called once per frame
	void Update () {
        if (currentExp >= toLevelUp[currentLevel])
        {
            //currentLevel++;

            LevelUp();
        }

	}

    public void AddExperience(int experienceToAdd)
    {
        currentExp += experienceToAdd;
    }

    // 
    public void LevelUp()
    {
        currentLevel++;
        currentHP = HPLevels[currentLevel];

        PlayerHealthManager.Instance.IncreaseMaxHealth(currentHP);
        PlayerManaManager.Instance.SetMaxMana();

        //U slucaju kada lvlupamo lika da mu doda samo razliku na HP koliko se povecava maxhealth
        //PlayerHealthManager.Instance.playerCurrentHealth += currentHP - HPLevels(currentLevel - 1);

        currentAttack = attackLevels[currentLevel];
        currentDefense = defenseLevels[currentLevel];

        SFXManager.Instance.PlaySound(SFXManager.Instance.levelUP);
    }
}
