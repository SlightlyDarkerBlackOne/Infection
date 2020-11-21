using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills{
   
   public event EventHandler<OnSkillUnlockedEventArgs> OnSkillUnlocked;
   public class OnSkillUnlockedEventArgs : EventArgs{
       public Skilltype skillType;
   }
   public enum Skilltype {
       None,
       Dash,
       PotionConsuming,
       HealthRegen,
       ManaRegen,
       MoveSpeed,
   }
   private List<Skilltype> unlockedSkillTypeList;

   public PlayerSkills(){
       unlockedSkillTypeList = new List<Skilltype>();
   }

   private void UnlockSkill(Skilltype skilltype){
       if(!IsSkillUnlocked(skilltype)){
            unlockedSkillTypeList.Add(skilltype);
            OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs{skillType = skilltype});
            Debug.Log("Unlocked skill " + skilltype);
       }
   }

   public bool IsSkillUnlocked(Skilltype skilltype){
       return unlockedSkillTypeList.Contains(skilltype);
   }

   public bool CanUnlock(Skilltype skilltype){
       Skilltype skillRequirement = GetSkillRequirement(skilltype);

       if(skillRequirement != Skilltype.None){
           if(IsSkillUnlocked(skillRequirement)){
               return true;
           } else {
               return false;
           }
       } else{
              return true;
       }
   }

   public Skilltype GetSkillRequirement(Skilltype skilltype){
       switch(skilltype){
           case Skilltype.HealthRegen:  return Skilltype.PotionConsuming;
           case Skilltype.ManaRegen:    return Skilltype.PotionConsuming;
           case Skilltype.Dash:         return Skilltype.MoveSpeed;
       }
       return Skilltype.None;
   }

   public bool TryUnlockSKill(Skilltype skilltype){
       if(CanUnlock(skilltype)){
           UnlockSkill(skilltype);
           return true;
       } else{
           return false;
       }
   }
}
