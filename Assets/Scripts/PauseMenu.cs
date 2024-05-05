using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public bool isPaused = false;
    public bool isOptions = false;
    public bool isSelectionScreen;

    public GameObject pauseMenuUi;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (isPaused && !isOptions && !isSelectionScreen)
            {
                Resume();
            }
            else if (!isPaused && !isOptions)
            {
                Pause();
            }
        }

    }

    public void OptionsMenu()
    {
        isOptions = true;
    }

    public void LeaveOptions()
    {
        isOptions = false;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
