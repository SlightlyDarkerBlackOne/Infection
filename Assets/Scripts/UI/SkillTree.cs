using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
public class SkillTree : MonoBehaviour
{
    [SerializeField] private Material skillLockedMaterial;
    [SerializeField] private Material skillUnlockableMaterial;
    [SerializeField] private SkillUnlockPath[] skillUnlockPathArray;
    [SerializeField] private Sprite lineSprite;
    [SerializeField] private Sprite lineGlowSprite;
    [SerializeField] private Sprite unlockedBackgroundImage;

    private PlayerSkills playerSkills;
    private List<SkillButton> skillButtonList;

    public GameObject skillTreePanel;

    public static SkillTree instance;

    private void Start() {
        instance = this;
    }
    public void SetPlayerSkills(PlayerSkills playerSkills){
        this.playerSkills = playerSkills;
        
        skillButtonList = new List<SkillButton>();
        skillButtonList.Add(new SkillButton(skillTreePanel.transform.Find("PotionConsumingBtn"), playerSkills, SkillType.PotionConsuming, skillLockedMaterial, skillUnlockableMaterial, unlockedBackgroundImage));
        skillButtonList.Add(new SkillButton(skillTreePanel.transform.Find("HealthRegenBtn"), playerSkills, SkillType.HealthRegen, skillLockedMaterial, skillUnlockableMaterial, unlockedBackgroundImage));
        skillButtonList.Add(new SkillButton(skillTreePanel.transform.Find("ManaRegenBtn"), playerSkills, SkillType.ManaRegen, skillLockedMaterial, skillUnlockableMaterial, unlockedBackgroundImage));
        skillButtonList.Add(new SkillButton(skillTreePanel.transform.Find("DashBtn"), playerSkills, SkillType.Dash, skillLockedMaterial, skillUnlockableMaterial, unlockedBackgroundImage));
        skillButtonList.Add(new SkillButton(skillTreePanel.transform.Find("MoveSpeedBtn"), playerSkills, SkillType.MoveSpeed, skillLockedMaterial, skillUnlockableMaterial, unlockedBackgroundImage));

        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
        UpdateVisuals();
    }

    private void PlayerSkills_OnSkillUnlocked(object sender, SkillType _skill)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals(){
        foreach(SkillButton skillButton in skillButtonList){
            skillButton.UpdateVisual();
        }

        foreach(SkillUnlockPath skillUnlockPath in skillUnlockPathArray){
            foreach(Image linkImage in skillUnlockPath.linkImageArray){
                linkImage.color = new Color(.5f, .5f, .5f);
                linkImage.sprite = lineSprite;
            }
        }

        foreach(SkillUnlockPath skillUnlockPath in skillUnlockPathArray){
            if(playerSkills.IsSkillUnlocked(skillUnlockPath.skillType) || playerSkills.CanUnlock(skillUnlockPath.skillType)){
                foreach(Image linkImage in skillUnlockPath.linkImageArray){
                    linkImage.color = Color.white;
                    linkImage.sprite = lineGlowSprite;
                }
            }
        }
    }

    private class SkillButton
    {
        private Transform transform;
        private Image image;
        private Image backgroundImage;
        private Sprite unlockedBackgroundImage;
        private PlayerSkills playerSkills;
        private SkillType skillType;
        private Material skillLockedMaterial;
        private Material skillUnlockableMaterial;

        public SkillButton(Transform transform, PlayerSkills playerSkills, SkillType skillType,
             Material skillLockedMaterial, Material skillUnlockableMaterial, Sprite unlockedBackgroundImage){
            this.transform = transform;
            this.playerSkills = playerSkills;
            this.skillType = skillType;
            this.skillLockedMaterial = skillLockedMaterial;
            this.skillUnlockableMaterial = skillUnlockableMaterial;
            this.unlockedBackgroundImage = unlockedBackgroundImage;

            image = transform.Find("image").GetComponent<Image>();	
            backgroundImage = transform.Find("background").GetComponent<Image>();

            transform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                playerSkills.TryUnlockSkill(skillType);
            };
        }

        public void UpdateVisual(){
            if(playerSkills.IsSkillUnlocked(skillType)){
                image.material = null;
                backgroundImage.material = null;
                backgroundImage.sprite = unlockedBackgroundImage;
            } else{
                if(playerSkills.CanUnlock(skillType)){
                    image.material = skillUnlockableMaterial;
                    backgroundImage.material = skillUnlockableMaterial;
                } else{
                    image.material = skillLockedMaterial;
                    backgroundImage.material = skillUnlockableMaterial;
                }
            }
        }
    }

    [System.Serializable]
    public class SkillUnlockPath{
        public SkillType skillType;
        public Image[] linkImageArray;
    }
    
}
