using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class SkillTree : MonoBehaviour
{
    private PlayerSkills playerSkills;
        void Awake(){
        transform.Find("PotionConsumingBtn").GetComponent<Button_UI>().ClickFunc = () => {
            playerSkills.TryUnlockSKill(PlayerSkills.Skilltype.PotionConsuming);
        };
        transform.Find("HealthRegenBtn").GetComponent<Button_UI>().ClickFunc = () => {
            playerSkills.TryUnlockSKill(PlayerSkills.Skilltype.HealthRegen);
        };
        transform.Find("ManaRegenBtn").GetComponent<Button_UI>().ClickFunc = () => {
            playerSkills.TryUnlockSKill(PlayerSkills.Skilltype.ManaRegen);
        };
        transform.Find("DashBtn").GetComponent<Button_UI>().ClickFunc = () => {
            playerSkills.TryUnlockSKill(PlayerSkills.Skilltype.Dash);
        };
        transform.Find("MoveSpeedBtn").GetComponent<Button_UI>().ClickFunc = () => {
            playerSkills.TryUnlockSKill(PlayerSkills.Skilltype.MoveSpeed);
        };
    }

    public void SetPlayerSkills(PlayerSkills playerSkills){
        this.playerSkills = playerSkills;
    }
}
