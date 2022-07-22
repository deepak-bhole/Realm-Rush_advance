using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{

    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] float Range = 15f;
    Transform target;
    

    void Update()
    {
        FindClosestTarget();
        AimWeapon(); 
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxdistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxdistance)
            {
                closestTarget = enemy.transform;
                maxdistance = targetDistance;
            }
        }
        target = closestTarget;
    }

    void Attack( bool isActive)
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }

    void AimWeapon()
    {
        float targetDistance =Vector3.Distance(transform.position, target.position);

        weapon.LookAt(target);

        if(targetDistance < Range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }
}
