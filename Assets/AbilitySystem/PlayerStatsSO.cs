using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsSO", menuName = "Scriptable Objects/Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
	public int currentLevel;
	public float currentExp;
	public int currentHP;
	public int currentAttack;
	public int currentDefense;
	public int numberOfSlainBugs;
}
