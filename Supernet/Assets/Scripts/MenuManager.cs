using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject afterGameMenu, pauseMenu;

    GameObject currentMenu;

    public void OpenPauseMenu()
    {
        currentMenu = pauseMenu;
        OpenMenu();
    }

    public void OpenAfterGameMenu()
    {
        currentMenu = afterGameMenu;
        OpenMenu();
    }

    void OpenMenu()
    {
        currentMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        currentMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}