using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private SpawnManager _spawnManager;
    private UIManager _uiManager;

    public bool _isCoopMode = false;

    [SerializeField] private GameObject _pausMenu;
    [SerializeField] private Button _button;
    [SerializeField] private bool _isGameOver = false;

    private bool _paused = false;

    private void Start()
    {
        _button = GetComponent<Button>();
        _spawnManager = GetComponent<SpawnManager>();
    }

    private void Update()
    {
        switch(_isCoopMode)
        {
            case false:
                if (Input.GetKeyDown(KeyCode.Escape) && _isGameOver == true)
                {
                    SceneManager.LoadScene(0);
                }
                else if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
                {
                    SceneManager.LoadScene(1);
                }
                break;

            case true:
                if (Input.GetKeyDown(KeyCode.Escape) && _isGameOver == true)
                {
                    SceneManager.LoadScene(0);
                }
                else if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
                {
                    SceneManager.LoadScene(2);
                }
                break;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pausMenu.SetActive(isActiveAndEnabled);
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
