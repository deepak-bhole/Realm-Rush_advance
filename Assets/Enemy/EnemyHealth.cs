using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))] 

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    [Tooltip("Add amount to MaxHitPoint when enemy die")]
    [SerializeField] int difficultyRamp = 1;
    int currentHitPoint = 0;
    
    Enemy enemy;

    void OnEnable()
    {
        currentHitPoint = maxHitPoints;    
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (currentHitPoint <= 0)
        {
            KillEnemy();
        }   
    }

    void ProcessHit()
    {
        currentHitPoint -= 1;
    }

    void KillEnemy()
    {
        gameObject.SetActive(false);
        maxHitPoints += difficultyRamp;
        enemy.RewardGold();
    }
}
