using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UniversalEnemy : EnemyBase
{
    [SerializeField] float enemySpeed, walkRange, idleTime;
    [SerializeField] Transform shotTip;
    [SerializeField] WeaponSO enemyWeapon;
    [SerializeField] float shotRotationSpeed;
    
    float currentInterval;
    NavMeshAgent agent;
    Vector3 targetPlace;
    float currentIdleTime;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemySpeed;
    }

    private void Update()
    {
        PatrolBehaviour();
    }


    void ShootPlayer()
    {
        if (!playerInRange)
        {
            currentInterval = 0;
            return;
        }

        Vector3 targetRotation = EnemyManager.Instance.PlayerControllerTransform.position - transform.position;
        targetRotation.y = 0;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetRotation), shotRotationSpeed * Time.deltaTime * 100);

        currentInterval += Time.deltaTime;

        if (currentInterval > enemyWeapon.shotInterval)
        {
            Bullet.Shot(EnemyManager.Instance.PlayerControllerTransform, shotTip, enemyWeapon, BulletType.EnemyBullet);
            currentInterval = 0;
        }
    }

    void PatrolBehaviour()
    {
        bool agentInPlace = agent.remainingDistance <= agent.stoppingDistance;

        if (currentIdleTime < idleTime)
        {
            if(agentInPlace)
            {
                ShootPlayer();
                agent.isStopped = true;
            }

            currentIdleTime += Time.deltaTime;
            return;
        }

        if (agentInPlace)
        {
            targetPlace = transform.position + RandomCalmDirection() * walkRange;
            agent.SetDestination(targetPlace);
        }
        else
        {
            currentIdleTime = 0;
            agent.isStopped = false;
        }
    }

    Vector3 RandomCalmDirection()
    {
        float x = Random.Range(-10, 10);
        float y = Random.Range(-10, 10);
        return new Vector3(x, 0, y).normalized;
    }
    

}
