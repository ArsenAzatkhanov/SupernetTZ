using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] int sceneIndex;

    public void StartGame()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
