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
            PlayerHealthManager pHm = player.GetComponent<Player>().playerHealthManager;
            PlayerController2D pC = PlayerController2D.Instance;
            if (manaHeal != 0) {
                PlayerManaManager pMm = player.GetComponent<PlayerManaManager>();

                if (pMm.playerCurrentMana != pMm.playerMaxMana) {
                    pMm.HealMana(manaHeal);
                    SFXManager.Instance.PlaySound(SFXManager.Instance.manaPotion);
                    gameObject.SetActive(false);
                }
            } else if(hpHeal != 0) {
                if (pHm.CanHeal()) {
                    pHm.Heal(hpHeal);
                    gameObject.SetActive(false);
                }
            } else if(pC.SpeedNotOnCooldown() && speedBuffDuration != 0) {
                
                pC.SetMoveSpeedBonuses(2, speedBuffDuration, 2);
                gameObject.SetActive(false);
            } else if(hpDamage != 0) {
                pHm.TakeDamage(hpDamage);
                gameObject.SetActive(false);
            }               
        }
    }
}
