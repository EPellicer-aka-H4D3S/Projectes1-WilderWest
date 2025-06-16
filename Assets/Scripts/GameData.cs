using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    [Header("Player")]
    public float jumpForce;
    
    [Header("GameSpeed")]
    public float speed;
    
    [Header("PlatformSpawning")]
    public float firstPlatformHeight;
    public float secondPlatformHeight;
    public int spawningLatitude;
    
    [Header("EnemySpawning")]
    public float enemyUp;
    public float enemyMiddle;
    public float enemyDown;
    
    [Header("GameScaling")]
    public float timeScaling;
    public float timeScalingMax;
    public int hitPenaltyBase;
    
    [Header("PlatformPrefabs")]
    public GameObject empty;
    public GameObject nuts;
    public GameObject platform;
    public GameObject nplatform;
    
    [Header("EnemyPrefabs")]
    public GameObject bison;
    public GameObject bulture;
    public GameObject bandit;
    public GameObject cactus;
    
    [Header("Pools")]
    public int[] Chances;
    public GameObject[] BasicPool;
    public GameObject[] NutlessPool;
}
