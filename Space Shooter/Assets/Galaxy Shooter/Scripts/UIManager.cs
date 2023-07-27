using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour 
{
    public TMP_Text _scoreText, _scoreText2, _highscoreText;
    public Sprite[] _livesSprite, _livesSprite2;
    public Image _livesImg, _livesImg2;
    public TMP_Text[] _gameOverText;
    public TMP_Text[] _powerUpText, _powerUpText2;
    public int _player1Score, _player2Score, _highScore;

    [SerializeField] private GameObject _pausMenu;

    private GameManager _gameManager;
    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        _highscoreText.text = "HighScore: " + _highScore;
        _gameOverText[0].gameObject.SetActive(false);
        _gameOverText[1].gameObject.SetActive(false);

        switch(_gameManager._isCoopMode)
        {
            case false:
                _scoreText.text = "Score: " + 0;
                break;

            case true:
                _scoreText.text = "Score: " + 0;
                _scoreText2.text = "Score: " + 0;
                break;
        }
    }

    public void CheckForHighscore()
    {
        if (_player1Score > _highScore)
        {
            _highScore = _player1Score;
            PlayerPrefs.SetInt("HighScore", _highScore);
            _highscoreText.text = "HighScore: " + _player1Score;
        }
    }

    public void PlayerOneUIScoreManager(int playerOneScore)
    {
        _player1Score = playerOneScore;
        _scoreText.text = "Score: " + _player1Score.ToString();
    }

    public void PlayerTwoUIScoreManager(int playerTwoScore)
    {
        _player2Score = playerTwoScore;
        _scoreText2.text = "Score: " + _player2Score.ToString();
    }

    public void UpdatePlayerOneLives(int playerOneLives)
    {
        _livesImg.sprite = _livesSprite[playerOneLives];


        if (playerOneLives == 0 )
        {
            GameOverSequence();
        }
    }

    public void UpdatePlayerTwoLives(int playerTwoLives)
    {
        _livesImg2.sprite = _livesSprite2[playerTwoLives];


        if (playerTwoLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameOverText[0].gameObject.SetActive(true);
        _gameOverText[1].gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText[0].text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText[0].text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
