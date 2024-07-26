using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ExitHandler : MonoBehaviour
{
    static int enemiesCount;

    bool doorIsOpened;

    [SerializeField] Material doorOpenMaterial;

    [SerializeField] TextMeshProUGUI enemyCountText;

    [SerializeField] UnityEvent endGameEvent;

    private void Start()
    {
        enemiesCount = FindObjectsOfType<EnemyBase>().Length;
        UpdateUI();
    }

    public void ChangeEnemiesCount(int value)
    {
        enemiesCount += value;
        CheckDoor();
        UpdateUI();
    }

    void CheckDoor()
    {
        if (enemiesCount <= 0)
        {
            doorIsOpened = true;
            GetComponent<Renderer>().material = doorOpenMaterial; 
        }
    }

    void UpdateUI()
    {
        enemyCountText.text = $"Enemies left: {enemiesCount}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if(doorIsOpened)
        {
            endGameEvent.Invoke();
        }
    }

}
