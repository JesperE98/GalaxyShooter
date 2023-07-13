using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour 
{
    public TMP_Text _scoreText;
    public TMP_Text _scoreText2;
    public Sprite[] _livesSprite;
    public Sprite[] _livesSprite2;
    public Image _livesImg;
    public Image _livesImg2;

    public TMP_Text[] _gameOverText;
    public TMP_Text[] _powerUpText;
    public TMP_Text[] _powerUpText2;

    private GameManager _gameManager;

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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

    public void PlayerOneUIScoreManager(int playerOneScore)
    {       
        _scoreText.text = "Score: " + playerOneScore.ToString();
    }

    public void PlayerTwoUIScoreManager(int playerTwoScore)
    {
        _scoreText2.text = "Score: " + playerTwoScore.ToString();
    }

    public void UpdatePlayerOneLives(int playerOneLives)
    {
        _livesImg.sprite = _livesSprite[playerOneLives];


        if (playerOneLives == 0 )
        {
            GameOverSequence();
            _gameManager.GameOver();
        }
    }

    public void UpdatePlayerTwoLives(int playerTwoLives)
    {
        _livesImg2.sprite = _livesSprite2[playerTwoLives];


        if (playerTwoLives == 0)
        {
            GameOverSequence();
            _gameManager.GameOver();
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
