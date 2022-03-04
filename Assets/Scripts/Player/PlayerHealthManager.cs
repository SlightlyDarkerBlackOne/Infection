using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "PlayerHealthManagerScriptableObject", menuName = "ScriptableObjects/Player Health Manager")]
public class PlayerHealthManager : HealthManagerSO {

    private SpriteRenderer playerSprite;

	void Start () {
        playerSprite = PlayerController2D.Instance.transform.Find("Animation").GetComponent<SpriteRenderer>();
    }

	// Update is called once per frame
	void Update () {
		if(currentHealth <= 0){
            Dead();
        }
        Flash(playerSprite);
	}

    private void Dead(){
        SFXManager.Instance.PlaySound(SFXManager.Instance.playerDead);

        SceneManager.LoadScene(LevelManager.Instance.levels[0].levelName);
        LevelManager.Instance.SetToLevelOne();

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
