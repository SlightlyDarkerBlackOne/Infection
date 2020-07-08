using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {

    public Image healthBar;
    public Image manaBar;
    public Text HPText;
    public Text ManaText;
    private PlayerHealthManager playerHealth;
    private PlayerManaManager playerMana;

    private PlayerStats thePS;
    public Text levelText;
    public Slider xpBar;

    #region Singleton
    public static UIManager Instance {get; private set;}

    void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else{
            Destroy(gameObject);
        }
    }
    #endregion
    
    // Use this for initialization
    void Start () {
        playerHealth = PlayerHealthManager.Instance;
        playerMana = PlayerManaManager.Instance;
        thePS = GetComponent<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateUIElements();
    }

    private void UpdateUIElements(){
        healthBar.fillAmount = playerHealth.playerCurrentHealth / playerHealth.playerMaxHealth;
        manaBar.fillAmount = playerMana.playerCurrentMana / playerMana.playerMaxMana;

        HPText.text = "HP: " + playerHealth.playerCurrentHealth + "/" + playerHealth.playerMaxHealth;
        ManaText.text = "Mana: " + playerMana.playerCurrentMana + "/" + playerMana.playerMaxMana;
        levelText.text = "Lvl: " + thePS.currentLevel;

        xpBar.value = thePS.currentExp / thePS.toLevelUp[thePS.currentLevel];
    }
}
