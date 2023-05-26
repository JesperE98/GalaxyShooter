using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _powerUpSpeed = 5.0f;
    [SerializeField] private int _powerUpID;
    [SerializeField] private AudioClip _clip;

    private UIManager _uiManager;

    void Awake()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
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
            Object.Destroy(this.gameObject);

        } 
        else if (_powerUpID == 1 && transform.position.y <= -5.8f)
        {
            Object.Destroy(this.gameObject);            
        }
        else if (_powerUpID == 2 && transform.position.y <= -5.8f)
        {
            Object.Destroy(this.gameObject);

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        Player _player = other.transform.GetComponent<Player>();
        if (_player != null)
        {
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            switch (_powerUpID)
            {
                case 0:
                    _player.TripleShotActive();
                    _uiManager._text[2].gameObject.SetActive(true);
                    _uiManager._text[3].gameObject.SetActive(false);
                    Destroy(this.gameObject);
                    break;

                case 1:
                    _player.SpeedBoostActive();
                    _uiManager._text[3].gameObject.SetActive(true);
                    _uiManager._text[2].gameObject.SetActive(false);
                    Destroy(this.gameObject);
                    break;

                case 2:
                    _player.ShieldActive();
                    _uiManager._text[4].gameObject.SetActive(true);
                    Destroy(this.gameObject);
                    break;

                default:
                    Debug.Log("Default Value");
                    break;
            }
        }
    }
}
