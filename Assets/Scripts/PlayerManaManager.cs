using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManaManager : MonoBehaviour {

    public float playerMaxMana;
    public float playerCurrentMana;

    public float startManaRegenTime = 3;
    [SerializeField]
    private float manaRegenTime;
    public int manaRegen = 1;
    
    // Use this for initialization
    void Start() {

        playerCurrentMana = playerMaxMana;
        manaRegenTime = startManaRegenTime;
    }


    // Update is called once per frame
    void Update() {
        manaRegenTime -= Time.deltaTime;
        if(manaRegenTime <= 0){
            manaRegenTime = startManaRegenTime;
            HealMana(manaRegen);
        }
    }

    public void HealMana(int manaToAdd){
        playerCurrentMana += manaToAdd;
        if(playerCurrentMana >= playerMaxMana){
            playerCurrentMana = playerMaxMana;
        }
    }

    public void TakeMana(int manaToTake) {
        playerCurrentMana -= manaToTake;
        if (playerCurrentMana <= 0) {
            playerCurrentMana = 0;
            Debug.Log("Out of mana!");
            //sounds "out of mana"
        }
    }

    public void SetMaxMana() {
        playerCurrentMana = playerMaxMana;
    }
}
