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
	void Start () {
        currentHP = HPLevels[1];
        currentAttack = attackLevels[1];
        currentDefense = defenseLevels[1];
	}
	void Update () {
        if (currentExp >= toLevelUp[currentLevel]){
            LevelUp();
        }
	}
    public void AddExperience(int experienceToAdd){
        currentExp += experienceToAdd;
    }
    public void LevelUp()
    {
        currentLevel++;
        currentHP = HPLevels[currentLevel];

        PlayerController2D.Instance.GetComponent<Player>().playerHealthManager.IncreaseMaxHealth(currentHP);
        PlayerManaManager.Instance.SetMaxMana();

        //Ako zelimo da mu se doda samo razlika na HP koliko se povecava maxhealth
        //PlayerHealthManager.Instance.playerCurrentHealth += currentHP - HPLevels(currentLevel - 1);

        currentAttack = attackLevels[currentLevel];
        currentDefense = defenseLevels[currentLevel];
        if(currentLevel > 1)
            SFXManager.Instance.PlaySound(SFXManager.Instance.levelUP);
    }
}
