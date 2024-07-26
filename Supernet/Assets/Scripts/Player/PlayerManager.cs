using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] int maxPlayerHealth;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] PlayerController playerController;
    [SerializeField] float invincibilityTime;

    [SerializeField] UnityEvent deathEvent;

    int currentHealth;
    public int CurrentHealth => currentHealth;

    int currentCoins;
    public int CurrentCoins => currentCoins;

    static PlayerManager _instance;
    public static PlayerManager Instance => _instance;

    float currentInvincibilityTime;
    bool duringInvincibility => currentInvincibilityTime > 0;
    

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

    private void Update()
    {
        if (currentInvincibilityTime > 0)
            currentInvincibilityTime -= Time.deltaTime;
    }

    public void ChangeHealth(int value)
    {
        if(value < 0)
        {
            if (duringInvincibility)
                return;
            else
                currentInvincibilityTime = invincibilityTime;
        }

        currentHealth += value;

        if (currentHealth <= 0)
            deathEvent.Invoke();


        UpdatePlayerUI();
    }

    public void ChangeCoins(int value) 
    {
        currentCoins += value;
        UpdatePlayerUI();
    }
    
    public void TriggerPushback( GameObject pushbackPoint )
    {
        Vector3 direction = (playerController.transform.position - pushbackPoint.transform.position).normalized;
        direction.y = 0;

        playerController.EnablePushback(direction);
    }

    void UpdatePlayerUI()
    {
        healthText.text = "Health: " + currentHealth.ToString(); 
        coinsText.text = "Coins: " + currentCoins.ToString(); 
    }
    
}