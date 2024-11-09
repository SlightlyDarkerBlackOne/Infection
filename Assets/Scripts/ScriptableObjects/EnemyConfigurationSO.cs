using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfiguration", menuName = "ScriptableObjects/EnemyConfiguration")]
public class EnemyConfigurationSO : ScriptableObject
{
    [Header("Movement Configuration")]
    public float moveSpeed;
    public float moveSpeedChaseModifier;
    public float rotationSpeed;
    public float timeBetweenMove;
    public float timeToMove;
    public float chasingDistance;
    public float unChasingDistance;

    [Header("Combat Configuration")]
    public float attackRange;
    public int attackDelay;
    public float startTimeBetweenAttack;
    public int damageToGive;
    public int critChance;
    public int critMultiplier;
    public float knockbackMultiplier = 5f;
    public float coolDownBetweenHits = 1f;
} 