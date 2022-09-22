using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Enemy targetEnemy;
    [SerializeField]private float targetRadius = 20f;
    private float lookForTargetDelay = .2f;
    [SerializeField]private float shootDelay = .3f;
    private Vector3 projectileSpawnPos;

    private void Start()
    {
        projectileSpawnPos = transform.Find("projectileSpawnPos").position;
        InvokeRepeating("LookForTargets", 0, lookForTargetDelay); // look for target
        InvokeRepeating("HandleShooting", .001f, shootDelay); // handle shooting
    }

    private void HandleShooting()
    {
        if (targetEnemy != null)
        {
            ArrowProjectile.Create(projectileSpawnPos, targetEnemy);
        }
    }

    private void LookForTargets()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, targetRadius);
        foreach (Collider2D collider2D in collider2Ds)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (targetEnemy == null)
                {
                    targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        targetEnemy = enemy;
                    }
                }
            }
        }
    }
}
