using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour {

    public float playerMaxHealth;
    public float playerCurrentHealth;

    private bool flashActive;
    public float flashLength;
    private float flashCounter;

    private SpriteRenderer playerSprite;

    #region Singleton
    public static PlayerHealthManager Instance {get; private set;}

    void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
    #endregion
	void Start () {
        playerCurrentHealth = playerMaxHealth;
        playerSprite = GetComponent<SpriteRenderer>();
	}

	
	// Update is called once per frame
	void Update () {
		if(playerCurrentHealth <= 0)
        {
            Dead();
        }

        Flash();
	}

    //Flashing the player sprite with white when he takes damage
    private void Flash(){
        if (flashActive)
        {
            if (flashCounter > flashLength * 0.66f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            } else if(flashCounter > flashLength * 0.33f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            } else if(flashCounter > 0)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            } else
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                flashActive = false;
            }

            flashCounter -= Time.deltaTime;
        }
    }
    private void Dead(){
        SFXManager.Instance.PlaySound(SFXManager.Instance.playerDead);

        SceneManager.LoadScene(LevelManager.Instance.levels[0].levelName);
        LevelManager.Instance.SetToLevelOne();

        SetToMaxHealth();
        PlayerManaManager.Instance.SetMaxMana();
    }

    public void HurtPlayer(int damageToGive)
    {
        playerCurrentHealth -= damageToGive;

        SFXManager.Instance.PlaySound(SFXManager.Instance.playerHurt);
        flashActive = true;
        flashCounter = flashLength;
    }

    public void SetToMaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    public void IncreaseMaxHealth(float newMaxHealth){
        playerMaxHealth = newMaxHealth;

        SetToMaxHealth();
    }

    public void Heal(float healAmount){
        playerCurrentHealth += healAmount;
        if(playerCurrentHealth >= playerMaxHealth){
            playerCurrentHealth = playerMaxHealth;
        }

        SFXManager.Instance.PlaySound(SFXManager.Instance.playerHealed);
    }
}
