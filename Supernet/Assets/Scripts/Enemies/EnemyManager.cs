using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] float enemyDetectRange;
    public float EnemyDetectRange => enemyDetectRange;

    [SerializeField] Transform playerControllerTransform;
    public Transform PlayerControllerTransform => playerControllerTransform;

    [SerializeField] GameObject damageEffect;

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

    public bool PlayerInRange( GameObject enemy ) => Vector3.Distance( enemy.transform.position, playerControllerTransform.position ) < enemyDetectRange;

    public void DamageEffect(Vector3 position)
    {
        StartCoroutine(CreateDamageEffect(position));
    }

    IEnumerator CreateDamageEffect(Vector3 position)
    {
        GameObject effect = Instantiate(damageEffect);
        effect.transform.position = position;

        ParticleSystem particles = effect.GetComponent<ParticleSystem>();

        particles.Play();
        
        yield return new WaitForSeconds(particles.main.duration);

        Destroy(effect);
    }
}
