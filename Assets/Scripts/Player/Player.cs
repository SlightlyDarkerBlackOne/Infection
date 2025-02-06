using UnityEngine;

public class Player : MonoBehaviour
{
	public PlayerHealthManager PlayerHealthManager;
	
	[SerializeField] private PlayerController2D m_playerController2D;
	private PlayerSkills m_playerSkills;
	[SerializeField] private HealthRegenSkill m_healthRegenSkill;
	[SerializeField] private ManaRegenSkill m_manaRegenSkill;
	[SerializeField] private SkillTreeUI m_skillTreeUI;
	[SerializeField] private KnockbackSkill m_knockbackSkill;

	private void Awake()
	{
		m_playerSkills = new PlayerSkills();
		m_playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillsUnlocked;
		
        //TODO remove this testing knockback skill (add scriptable object for skills for testing)
        m_playerSkills.TryUnlockSkill(SkillType.Knockback);

		if (m_skillTreeUI != null)
		{
			m_skillTreeUI.Initialize(m_playerSkills);
		}
	}

	private void OnDestroy()
	{
		if (m_playerSkills != null)
		{
			m_playerSkills.OnSkillUnlocked -= PlayerSkills_OnSkillsUnlocked;
		}
	}

	private void PlayerSkills_OnSkillsUnlocked(object sender, SkillType _skillType)
	{
		switch (_skillType)
		{
			case SkillType.HealthRegen:
				m_healthRegenSkill.StartRegeneration();
				break;
			case SkillType.ManaRegen:
				m_manaRegenSkill.StartRegeneration();
				break;
			case SkillType.MoveSpeed:
				m_playerController2D.IncreaseMoveSpeed();
				break;
			case SkillType.PotionConsuming:
				//EnablePotionConsuming();
				break;
			case SkillType.Knockback:
				m_knockbackSkill.EnableKnockback();
				break;
		}
	}

	public PlayerSkills GetPlayerSkills() => m_playerSkills;

	public bool CanUseDash() => m_playerSkills.IsSkillUnlocked(SkillType.Dash);

	public void ApplyKnockbackToTarget(GameObject _target, float _damage, Vector2 _direction)
	{
		m_knockbackSkill.ApplyKnockback(_target, _damage, _direction);
	}
}
