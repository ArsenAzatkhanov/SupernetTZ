using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] int maxHealth;

    int currentHealth;

    protected bool playerInRange => EnemyManager.Instance.PlayerInRange(gameObject);

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            PlayerManager.Instance.ChangeCoins(1);
        }
            
    }
}