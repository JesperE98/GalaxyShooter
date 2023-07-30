using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadSinglePlayerMode()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void LoadCoOpMode()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
