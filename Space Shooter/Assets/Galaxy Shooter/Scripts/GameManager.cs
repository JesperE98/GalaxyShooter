using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private SpawnManager _spawnManager;
    private UIManager _uiManager;

    private Animator _pauseAnimator;

    public bool _isCoopMode = false;

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _coopPlayers;
    [SerializeField] private GameObject _pausMenu;
    [SerializeField] private bool _isGameOver = false;

    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _pauseAnimator = GameObject.Find("PausMenu").GetComponent<Animator>();

        switch(_isCoopMode)
        {
            case false:
                Instantiate(_player, Vector3.zero, Quaternion.identity);

                break;

            case true:
                Instantiate(_coopPlayers, Vector3.zero, Quaternion.identity);

                break;
        }
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
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
                    Instantiate(_player, Vector3.zero, Quaternion.identity);
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
                    Instantiate(_coopPlayers, Vector3.zero, Quaternion.identity);
                }
                break;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            _pausMenu.SetActive(true);
            _pauseAnimator.SetBool("PausMenuActivated", true);
            PauseTheGame();
        }
    }

    public void PauseTheGame()
    {

        Time.timeScale = 0f;      
    }

    public void ResumePlay()
    {
        _pausMenu.SetActive(false);
        _pauseAnimator.SetBool("PausMenuActivated", false);
        Time.timeScale = 1f;
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
