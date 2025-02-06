using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using CodeMonkey.Utils;

public class SkillTreeUI : MonoBehaviour
{
    [Header("Visual Settings")]
    [SerializeField] private Material m_SkillLockedMaterial;
    [SerializeField] private Material m_SkillUnlockableMaterial;
    [SerializeField] private Sprite m_LineSprite;
    [SerializeField] private Sprite m_LineGlowSprite;
    [SerializeField] private Sprite m_UnlockedBackgroundSprite;

    [Header("References")]
    [SerializeField] private SkillUnlockPath[] m_SkillUnlockPaths;
    [SerializeField] private GameObject m_SkillTreePanel;

    private PlayerSkills m_PlayerSkills;
    private readonly List<SkillButton> m_SkillButtons = new List<SkillButton>();

    public void Initialize(PlayerSkills _playerSkills)
    {
        m_PlayerSkills = _playerSkills;
        InitializeSkillButtons();
        m_PlayerSkills.OnSkillUnlocked += HandleSkillUnlocked;
        UpdateVisuals();
    }

    private void OnDisable()
    {
        if (m_PlayerSkills != null)
        {
            m_PlayerSkills.OnSkillUnlocked -= HandleSkillUnlocked;
        }
    }

    private void InitializeSkillButtons()
    {
        m_SkillButtons.Clear();
        AddSkillButton("PotionConsumingBtn", SkillType.PotionConsuming);
        AddSkillButton("HealthRegenBtn", SkillType.HealthRegen);
        AddSkillButton("ManaRegenBtn", SkillType.ManaRegen);
        AddSkillButton("DashBtn", SkillType.Dash);
        AddSkillButton("MoveSpeedBtn", SkillType.MoveSpeed);
    }

    private void AddSkillButton(string _buttonName, SkillType _skillType)
    {
        Transform buttonTransform = m_SkillTreePanel.transform.Find(_buttonName);
        if (buttonTransform != null)
        {
            m_SkillButtons.Add(new SkillButton(
                buttonTransform,
                m_PlayerSkills,
                _skillType,
                m_SkillLockedMaterial,
                m_SkillUnlockableMaterial,
                m_UnlockedBackgroundSprite
            ));
        }
        else
        {
            Debug.LogError($"Could not find skill button: {_buttonName}");
        }
    }

    private void HandleSkillUnlocked(object _sender, SkillType _skill)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        foreach (SkillButton skillButton in m_SkillButtons)
        {
            skillButton.UpdateVisual();
        }
        UpdateSkillConnectionLines();
    }

    private void UpdateSkillConnectionLines()
    {
        foreach (SkillUnlockPath skillPath in m_SkillUnlockPaths)
        {
            bool isUnlockedOrUnlockable = m_PlayerSkills.IsSkillUnlocked(skillPath.SkillType) ||
                                        m_PlayerSkills.CanUnlock(skillPath.SkillType);

            foreach (Image linkImage in skillPath.LinkImages)
            {
                linkImage.color = isUnlockedOrUnlockable ? Color.white : new Color(.5f, .5f, .5f);
                linkImage.sprite = isUnlockedOrUnlockable ? m_LineGlowSprite : m_LineSprite;
            }
        }
    }

    [System.Serializable]
    private class SkillUnlockPath
    {
        public SkillType SkillType;
        public Image[] LinkImages;
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
                 Material skillLockedMaterial, Material skillUnlockableMaterial, Sprite unlockedBackgroundImage)
        {
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

        public void UpdateVisual()
        {
            if (playerSkills.IsSkillUnlocked(skillType))
            {
                image.material = null;
                backgroundImage.material = null;
                backgroundImage.sprite = unlockedBackgroundImage;
            }
            else
            {
                if (playerSkills.CanUnlock(skillType))
                {
                    image.material = skillUnlockableMaterial;
                    backgroundImage.material = skillUnlockableMaterial;
                }
                else
                {
                    image.material = skillLockedMaterial;
                    backgroundImage.material = skillUnlockableMaterial;
                }
            }
        }
    }
}