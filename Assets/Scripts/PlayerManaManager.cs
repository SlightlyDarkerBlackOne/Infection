using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManaManager : MonoBehaviour {

    public float playerMaxMana;
    public float playerCurrentMana;
    
    // Use this for initialization
    void Start() {

        playerCurrentMana = playerMaxMana;
    }


    // Update is called once per frame
    void Update() {
        if (playerCurrentMana <= 0) {
            Debug.Log("Out of mana!");
            //sounds "out of mana"
        }
    }

    public void TakeMana(int manaToTake) {
        playerCurrentMana -= manaToTake;
    }

    public void SetMaxMana() {
        playerCurrentMana = playerMaxMana;
    }
}
