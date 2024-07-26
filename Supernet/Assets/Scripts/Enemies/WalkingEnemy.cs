using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingEnemy : EnemyBase
{
    [SerializeField] float enemySpeed, walkRange, idleTime;
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

    void PatrolBehaviour()
    {
        if (currentIdleTime < idleTime)
        {
            currentIdleTime += Time.deltaTime;
            return;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            targetPlace = playerInRange ? EnemyManager.Instance.PlayerControllerTransform.position : transform.position + RandomDirection() * walkRange;
            agent.SetDestination(targetPlace);
        }
        else
            currentIdleTime = 0;
    }


    Vector3 RandomDirection()
    {
        float x = Random.Range(-10, 10);
        float y = Random.Range(-10, 10);
        return new Vector3(x, 0, y).normalized;
    }

}
