using UnityEngine;

[CreateAssetMenu(fileName = "RangedEnemyConfig", menuName = "ScriptableObjects/RangedEnemyConfig")]
public class RangedEnemyConfig : ScriptableObject
{
    public float speed = 5f;
    public float chasingDistance = 10f;
    public float stoppingDistance = 5f;
    public float retreatDistance = 3f;
    public float startTimeBtwShots = 1f;
    public GameObject projectile;
} 