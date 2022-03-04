using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerSkills playerSkills;
    public PlayerHealthManager playerHealthManager;
    void Awake()
    {
        playerSkills = new PlayerSkills();
        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillsUnlocked;
    }

    private void PlayerSkills_OnSkillsUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e){
        switch (e.skillType){
            case PlayerSkills.Skilltype.HealthRegen:
                //SetHealthRegen();
                break;
            case PlayerSkills.Skilltype.ManaRegen:
                //SetManaRegen();
                break;
            case PlayerSkills.Skilltype.MoveSpeed:
                PlayerController2D.Instance.IncreaseMoveSpeed();
                break;
            case PlayerSkills.Skilltype.PotionConsuming:
                //EnablePotionConsuming();
                break;
        }
    }

    public PlayerSkills GetPlayerSkills(){
        return playerSkills;
    }

    public bool CanUseDash(){
        return playerSkills.IsSkillUnlocked(PlayerSkills.Skilltype.Dash);
    }
}
