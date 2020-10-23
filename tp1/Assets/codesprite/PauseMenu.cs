using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUY;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
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
        pauseMenuUY.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUY.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }


    public void quit()
    {
        Application.Quit();
    }
    public void menu()
    {
        SceneManager.LoadScene("Main");
    }
}
