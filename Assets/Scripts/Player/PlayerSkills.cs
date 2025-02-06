using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the player's skill system including unlocking and skill dependencies.
/// </summary>
public class PlayerSkills
{
    private readonly HashSet<SkillType> m_unlockedSkills;
    
    public event EventHandler<SkillType> OnSkillUnlocked;

    public PlayerSkills()
    {
        m_unlockedSkills = new HashSet<SkillType>();
    }

    /// <summary>
    /// Attempts to unlock a skill if its requirements are met.
    /// </summary>
    /// <param name="_skillType">The skill to unlock</param>
    /// <returns>True if the skill was successfully unlocked</returns>
    public bool TryUnlockSkill(SkillType _skillType)
    {
        if (!CanUnlock(_skillType)) return false;
        
        UnlockSkill(_skillType);
        return true;
    }

    /// <summary>
    /// Checks if a skill is currently unlocked.
    /// </summary>
    public bool IsSkillUnlocked(SkillType _skillType) => 
        m_unlockedSkills.Contains(_skillType);

    /// <summary>
    /// Checks if a skill can be unlocked based on its requirements.
    /// </summary>
    public bool CanUnlock(SkillType _skillType)
    {
        SkillType requirement = GetSkillRequirement(_skillType);
        return requirement == SkillType.None || IsSkillUnlocked(requirement);
    }

    /// <summary>
    /// Gets the prerequisite skill required to unlock the specified skill.
    /// </summary>
    public SkillType GetSkillRequirement(SkillType _skillType) => _skillType switch
    {
        SkillType.HealthRegen => SkillType.PotionConsuming,
        SkillType.ManaRegen => SkillType.PotionConsuming,
        SkillType.Dash => SkillType.MoveSpeed,
        SkillType.Knockback => SkillType.None,
        _ => SkillType.None
    };

    private void UnlockSkill(SkillType _skillType)
    {
        if (m_unlockedSkills.Add(_skillType))
        {
            OnSkillUnlocked?.Invoke(this, _skillType);
            Debug.Log($"Unlocked skill: {_skillType}");
        }
    }
}

public enum SkillType
{
    None,
    Dash,
    PotionConsuming,
    HealthRegen,
    ManaRegen,
    MoveSpeed,
    Knockback,
}
