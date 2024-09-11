using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool gameisPaused = false;
    public GameObject pauseMenuUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameisPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
       gameisPaused = false;
       pauseMenuUI.SetActive(false);
       Time.timeScale = 1.0f;
    }
    public void Pause()
    {
        gameisPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Menu()
    {
        Debug.Log("Loading Menu ...");
    }
    public void Quit()
    {
        Debug.Log("Quittng Game");
        Application.Quit();
    }

}
