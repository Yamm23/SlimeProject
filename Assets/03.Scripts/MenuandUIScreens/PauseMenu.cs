using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool gameisPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;
    public GameObject levelComplete;
    public CoinManager coinManager;
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
    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
       gameisPaused = false;
        AudioManager.instance.Play("Unpause");
       pauseMenuUI.SetActive(false);
       Time.timeScale = 1.0f;
    }
    public void Pause()
    {
        gameisPaused = true;
        AudioManager.instance.Play("Pause");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOverUI.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void Menu()
    {
        Debug.Log("Loading Menu ...");
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1.0f;
    }
    public void Quit()
    {
        Debug.Log("Quittng Game");
        Application.Quit();
    }
    public void LevelComplete()
    {
        levelComplete.SetActive(true);
        coinManager.OnGameComplete();
    }

}
