using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelStartDelay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;

    [SerializeField] GameObject[] showOnDelayEnd; 

    private void Start()
    {
        StartCoroutine(StartDelay());
        countdownText.text = 3.ToString();

        for (int i = 0; i < showOnDelayEnd.Length; i++)
            showOnDelayEnd[i].SetActive(false);
    }

    IEnumerator StartDelay()
    {
        Time.timeScale = 0f;

        float value = 0;

        for (int i = 0; i < 3; i++)
        {
            while (value < 1)
            {
                value += Time.unscaledDeltaTime;
                yield return null;
            }
            value = 0;
            countdownText.text = (2-i).ToString();
        }

        countdownText.gameObject.SetActive(false);

        for (int i = 0; i < showOnDelayEnd.Length; i++)
            showOnDelayEnd[i].SetActive(true);

        Time.timeScale = 1f;
    }
}
