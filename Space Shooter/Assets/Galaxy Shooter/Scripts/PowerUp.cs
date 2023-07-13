using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _powerUpSpeed = 5.0f;
    [SerializeField] private int _powerUpID;
    [SerializeField] private AudioClip _clip;

    private GameManager _gameManager;
    private UIManager _uiManager;
    private Player _player;
    private Player _player2;

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
         
        switch(_gameManager._isCoopMode)
        {
            case false:
                _player = GameObject.Find("Player").GetComponent<Player>();
                break;

            case true:
                _player = GameObject.Find("Player").GetComponent<Player>();
                _player2 = GameObject.Find("Player_2").GetComponent<Player>();
                break;
        }
    }
    void Update()
    {
        PowerUpMovement();
    }

    void PowerUpMovement()
    {
        transform.Translate(Vector3.down * _powerUpSpeed * Time.deltaTime);

        if (_powerUpID == 0 && transform.position.y <= -5.8f)
        {
            Destroy(this.gameObject);
        } 
        else if (_powerUpID == 1 && transform.position.y <= -5.8f)
        {
            Destroy(this.gameObject);            
        }
        else if (_powerUpID == 2 && transform.position.y <= -5.8f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(_gameManager._isCoopMode)
        {
            case false:
                if (other.CompareTag("Player"))
                {
                    AudioSource.PlayClipAtPoint(_clip, transform.position);
                    switch (_powerUpID)
                    {
                        case 0:
                            _player.TripleShotActive();
                            _uiManager._powerUpText[0].gameObject.SetActive(true);
                            _uiManager._powerUpText[1].gameObject.SetActive(false);
                            Destroy(this.gameObject);
                            break;

                        case 1:
                            _player.SpeedBoostActive();
                            _uiManager._powerUpText[1].gameObject.SetActive(true);
                            _uiManager._powerUpText[0].gameObject.SetActive(false);
                            Destroy(this.gameObject);
                            break;

                        case 2:
                            _player.ShieldActive();
                            _uiManager._powerUpText[2].gameObject.SetActive(true);
                            Destroy(this.gameObject);
                            break;

                        default:
                            Debug.Log("Default Value");
                            break;
                    }
                }
                break;

            case true:
                if (other.CompareTag("Player"))
                {
                    AudioSource.PlayClipAtPoint(_clip, transform.position);
                    switch (_powerUpID)
                    {
                        case 0:
                            _player.TripleShotActive();
                            _uiManager._powerUpText[0].gameObject.SetActive(true);
                            _uiManager._powerUpText[1].gameObject.SetActive(false);
                            Destroy(this.gameObject);
                            break;

                        case 1:
                            _player.SpeedBoostActive();
                            _uiManager._powerUpText[1].gameObject.SetActive(true);
                            _uiManager._powerUpText[0].gameObject.SetActive(false);
                            Destroy(this.gameObject);
                            break;

                        case 2:
                            _player.ShieldActive();
                            _uiManager._powerUpText[2].gameObject.SetActive(true);
                            Destroy(this.gameObject);
                            break;

                        default:
                            Debug.Log("Default Value");
                            break;
                    }
                }
                else if (other.CompareTag("PlayerTwo"))
                {
                    AudioSource.PlayClipAtPoint(_clip, transform.position);
                    switch (_powerUpID)
                    {
                        case 0:
                            _player2.TripleShotActive();
                            _uiManager._powerUpText2[0].gameObject.SetActive(true);
                            _uiManager._powerUpText2[1].gameObject.SetActive(false);
                            Destroy(this.gameObject);
                            break;

                        case 1:
                            _player2.SpeedBoostActive();
                            _uiManager._powerUpText2[1].gameObject.SetActive(true);
                            _uiManager._powerUpText2[0].gameObject.SetActive(false);
                            Destroy(this.gameObject);
                            break;

                        case 2:
                            _player2.ShieldActive();
                            _uiManager._powerUpText2[2].gameObject.SetActive(true);
                            Destroy(this.gameObject);
                            break;

                        default:
                            Debug.Log("Default Value");
                            break;
                    }
                }
                break;
        }

    }
}
