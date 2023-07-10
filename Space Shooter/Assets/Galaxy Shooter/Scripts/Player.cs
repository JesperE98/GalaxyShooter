using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{

    [SerializeField] private bool isPlayerOne = false;
    [SerializeField] private bool isPlayerTwo = false;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldPrefab;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private AudioClip _laserSoundClip;
    [SerializeField] private GameObject[] _playerEngineDamagedPrefabs;

    private float _movementSpeed = 5f;
    private int _score;
    private int _lives = 3;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;
    private float _nextFire = -1.0f;
    private int _speedMultiplier = 2;
    private Vector3 _laserOffsetPosition = new Vector3(0f, 0.75f, 0);
    private Vector3 _tripleLaserOffsetPosition = new Vector3(0f, 0.75f, 0);

    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private AudioSource _audioSource;

    void Awake()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
/*
        if (_gameManager._isCoopMode == false)
        {
            _gameManager._player1.transform.position = new Vector3(0, -2.5f, 0);
        } 
        else if (_gameManager._isCoopMode == true)
        {
            _gameManager._player1.transform.position = new Vector3(-5, -2.5f, 0);
            _gameManager._player2.transform.position = new Vector3(5, -2.5f, 0);
        }
*/
        _audioSource.clip = _laserSoundClip; 
        _playerEngineDamagedPrefabs[0].SetActive(false);
        _playerEngineDamagedPrefabs[1].SetActive(false);
    }

    void Update()
    {
        if (isPlayerOne == true)
        {
            PlayerMovement();

            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
            {
                PlayerShoot();
            }
        }

        if (isPlayerTwo == true)
        {
            Player2Movement();

            if (Input.GetKeyDown(KeyCode.KeypadEnter) && Time.time > _nextFire)
            {
                Player2Shoot();
            }
        }

    }

    void PlayerShoot()
    {
        _nextFire = Time.time + _fireRate;

            if (_isTripleShotActive == true) 
            { 
                Instantiate(_tripleShotPrefab, transform.position + _tripleLaserOffsetPosition, Quaternion.identity); 
            }
            else
            { 
                Instantiate(_laserPrefab, transform.position + _laserOffsetPosition, Quaternion.identity); 
            }       
        _audioSource.Play();
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticallInput = Input.GetAxis("Vertical");
        Vector3 _direction = new Vector3(horizontalInput, verticallInput, 0f);
        transform.Translate(_direction * _movementSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.6f, 0), 0);

        if (transform.position.x >= 11f)
        {
            transform.position = new Vector3(-11f, transform.position.y, 0f);
        }
        else if (transform.position.x <= -11f)
        {
            transform.position = new Vector3(11f, transform.position.y, 0f);
        }
    }

    void Player2Shoot()
    {
        _nextFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position + _tripleLaserOffsetPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + _laserOffsetPosition, Quaternion.identity);
        }
        _audioSource.Play();
    }

    void Player2Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal2");
        float verticallInput = Input.GetAxis("Vertical2");
        Vector3 _direction = new Vector3(horizontalInput, verticallInput, 0f);
        transform.Translate(_direction * _movementSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.6f, 0), 0);

        if (transform.position.x >= 11f)
        {
            transform.position = new Vector3(-11f, transform.position.y, 0f);
        }
        else if (transform.position.x <= -11f)
        {
            transform.position = new Vector3(11f, transform.position.y, 0f);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        _isTripleShotActive = false;
        _uiManager._text[2].gameObject.SetActive(false);
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        if (_isSpeedBoostActive == true)
        {
            _movementSpeed *= _speedMultiplier;
            StartCoroutine(SpeedBoostPowerDownRoutine());
        }
    }
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _uiManager._text[3].gameObject.SetActive(false);
        _isSpeedBoostActive = false;
        _movementSpeed /= _speedMultiplier;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;       
        _shieldPrefab.SetActive(true);       
    }

    public void ScoreManager(int points)
    {
        _score += points;
        _uiManager.UIScoreManager(_score);
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldPrefab.SetActive(false);
            _uiManager._text[4].gameObject.SetActive(false);
            return;
        }

        _lives--;
        _uiManager.UpdateLives(_lives);

        if (_lives == 2) { _playerEngineDamagedPrefabs[0].SetActive(true); }

        if (_lives == 1) { _playerEngineDamagedPrefabs[1].SetActive(true); }

        if (_lives < 1)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _spawnManager.StopTheGame();
            Destroy(this.gameObject, 0.3f);          
        }
    }
}
