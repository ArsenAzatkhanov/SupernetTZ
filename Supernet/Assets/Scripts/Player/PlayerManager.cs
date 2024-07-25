using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] int maxPlayerHealth;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI coinsText;
    int currentHealth;
    public int CurrentHealth => currentHealth;

    int currentCoins;
    public int CurrentCoins => currentCoins;

    static PlayerManager _instance;
    public static PlayerManager Instance => _instance;

    private void Awake()
    {
        currentHealth = maxPlayerHealth;

        if (_instance != null && _instance != this)
        {
            Debug.LogWarning($"Destroying {this.gameObject}.");
            Destroy(this.gameObject);
        }
        else
            _instance = this;

        UpdatePlayerUI();
    }

    public void ChangeHealth(int value)
    {
        currentHealth += value;
        UpdatePlayerUI();
    }

    public void ChangeCoins(int value) 
    {
        currentCoins += value;
        UpdatePlayerUI();
    }
    
    void UpdatePlayerUI()
    {
        healthText.text = "Health: " + currentHealth.ToString(); 
        coinsText.text = "Coins: " + currentCoins.ToString(); 
    }
}