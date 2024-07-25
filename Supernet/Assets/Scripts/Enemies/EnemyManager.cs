using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] float enemyDetectRange;
    public float EnemyDetectRange => enemyDetectRange;

    [SerializeField] Transform playerTransform;
    public Transform PlayerTransform => playerTransform;
    
    static EnemyManager _instance;
    public static EnemyManager Instance => _instance;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Debug.LogWarning($"Destroying {this.gameObject}.");
            Destroy(this.gameObject);
        }
        else
            _instance = this;
    }

    public bool PlayerInRange( GameObject enemy ) => Vector3.Distance( enemy.transform.position, playerTransform.position ) < enemyDetectRange;
    
}
