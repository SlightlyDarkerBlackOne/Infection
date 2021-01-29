using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Heals the player mana for points if mana isn't full
public class MushroomConsumable : MonoBehaviour
{
    [SerializeField]
    private float hpHeal = 0;
    [SerializeField]
    private int hpDamage = 0;
    [SerializeField]
    private int manaHeal = 0;
    [SerializeField]
    private int speedBuffDuration = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            GameObject player = PlayerController2D.Instance.gameObject;
            PlayerHealthManager pHm = PlayerHealthManager.Instance;
            if (manaHeal != 0) {
                PlayerManaManager pMm = player.GetComponent<PlayerManaManager>();

                if (pMm.playerCurrentMana != pMm.playerMaxMana) {
                    pMm.HealMana(manaHeal);
                    gameObject.SetActive(false);
                }
            } else if(hpHeal != 0) {
                if (pHm.playerCurrentHealth != pHm.playerMaxHealth)
                    pHm.Heal(hpHeal);
            } else if(speedBuffDuration != 0) {
                PlayerController2D pC = PlayerController2D.Instance;
                pC.SetMoveSpeedBonuses(2, speedBuffDuration, 0);
            } else if(hpDamage != 0) {
                pHm.HurtPlayer(hpDamage);
            }               
        }
    }
}
