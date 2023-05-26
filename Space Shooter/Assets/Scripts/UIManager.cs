using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour 
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Sprite[] _livesSprite;
    [SerializeField] private Image _livesImg;

    public TMP_Text[] _text;

    private GameManager _gameManager;

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _text[0].gameObject.SetActive(false);
        _text[1].gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;
    }

    public void UIScoreManager(int playerScore)
    {       
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _livesSprite[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
            _gameManager.GameOver();
        }
    }

    void GameOverSequence()
    {
        _text[0].gameObject.SetActive(true);
        _text[1].gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _text[0].text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _text[0].text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
