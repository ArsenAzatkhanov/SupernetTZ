using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int contactDamage;

    public int ContactDamage => contactDamage;

    int currentHealth;

    protected bool playerInRange => EnemyManager.Instance.PlayerInRange(gameObject);

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth == 0)
        {
            FindObjectOfType<ExitHandler>().ChangeEnemiesCount(-1);
            PlayerManager.Instance.ChangeCoins(1);
            Destroy(gameObject);
        }
    }
}