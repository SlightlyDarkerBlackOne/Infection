using UnityEngine;

public class Player : MonoBehaviour
{
	private PlayerSkills playerSkills;
	public PlayerHealthManager playerHealthManager;

	[SerializeField] private PlayerController2D m_playerController2D;

	private void Awake()
	{
		playerSkills = new PlayerSkills();
		playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillsUnlocked;
	}

	private void PlayerSkills_OnSkillsUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e)
	{
		switch (e.skillType)
		{
			case PlayerSkills.Skilltype.HealthRegen:
				//SetHealthRegen();
				break;
			case PlayerSkills.Skilltype.ManaRegen:
				//SetManaRegen();
				break;
			case PlayerSkills.Skilltype.MoveSpeed:
				m_playerController2D.IncreaseMoveSpeed();
				break;
			case PlayerSkills.Skilltype.PotionConsuming:
				//EnablePotionConsuming();
				break;
		}
	}

	public PlayerSkills GetPlayerSkills()
	{
		return playerSkills;
	}

	public bool CanUseDash()
	{
		return playerSkills.IsSkillUnlocked(PlayerSkills.Skilltype.Dash);
	}
}
