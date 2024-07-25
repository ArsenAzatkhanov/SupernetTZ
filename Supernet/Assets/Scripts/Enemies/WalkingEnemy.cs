using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingEnemy : EnemyBase
{
    [SerializeField] float enemySpeed;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemySpeed;
    }

    private void Update()
    {
        agent.SetDestination(EnemyManager.Instance.PlayerTransform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
    }
}
