﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {

    public Image healthBar;
    public Image manaBar;
    public Text HPText;
    public Text ManaText;
    public PlayerHealthManager playerHealth;
    public PlayerManaManager playerMana;

    private PlayerStats thePS;
    public Text levelText;
    public Slider xpBar;

    private static bool UIExists;

    // Use this for initialization
    void Start () {
        /* Usefull for scene changing 
        if (!UIExists)
        {
            UIExists = true;
            DontDestroyOnLoad(transform.gameObject);
        } else
        {
            Destroy(gameObject);
        }*/

        playerHealth = FindObjectOfType<PlayerHealthManager>();
        playerMana = FindObjectOfType<PlayerManaManager>();
        thePS = GetComponent<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.fillAmount = playerHealth.playerCurrentHealth / playerHealth.playerMaxHealth;
        manaBar.fillAmount = playerMana.playerCurrentMana / playerMana.playerMaxMana;

        HPText.text = "HP: " + playerHealth.playerCurrentHealth + "/" + playerHealth.playerMaxHealth;
        ManaText.text = "Mana: " + playerMana.playerCurrentMana + "/" + playerMana.playerMaxMana;
        levelText.text = "Lvl: " + thePS.currentLevel;

        xpBar.value = thePS.currentExp / thePS.toLevelUp[thePS.currentLevel];
    }
}
