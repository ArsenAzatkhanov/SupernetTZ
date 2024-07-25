using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform player;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform target;
    [Space(5)]

    [Header("Settings")]
    [SerializeField] float detectionRadius;
    [SerializeField] float targetSpeed;

    public GameObject NearestEnemy => nearestEnemy;

    GameObject nearestEnemy;

    private void Update()
    {
        if(DetectNearestEnemy(out GameObject enemy))
        {
            nearestEnemy = enemy;

            Vector3 newEnemyPos = new Vector3(enemy.transform.position.x, target.position.y, enemy.transform.position.z);
            if (target.gameObject.activeInHierarchy)
                target.position = Vector3.MoveTowards(target.position, newEnemyPos, targetSpeed * Time.deltaTime);
            else
            {
                target.gameObject.SetActive(true);
                target.position = newEnemyPos;
            }
        }
        else
        {
            target.gameObject.SetActive(false);
            nearestEnemy = null;
        }
    }

    public bool DetectNearestEnemy(out GameObject enemy)
    {
        enemy = null;

        Collider[] enemiesInRadius = Physics.OverlapSphere(player.position, detectionRadius, enemyLayer);

        if (enemiesInRadius.Length <= 0)
            return false;
        else
        {
            enemy = enemiesInRadius[0].gameObject;

            float currentLowestDistance = Vector3.Distance(player.position, enemy.transform.position);
            float newLowestDistance;
            foreach (Collider c in enemiesInRadius)
            {
                newLowestDistance = Vector3.Distance(player.position, c.gameObject.transform.position);
                if (newLowestDistance < currentLowestDistance)
                {
                    currentLowestDistance = newLowestDistance;
                    enemy = c.gameObject;
                }
            }

            return true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(player.position, detectionRadius);
    }
}
