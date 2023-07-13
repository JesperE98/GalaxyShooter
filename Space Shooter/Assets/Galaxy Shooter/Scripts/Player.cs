using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private AudioSource _audioSource;

    public bool _isPlayerOne = false;
    public bool _isPlayerTwo = false;

    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldPrefab;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private AudioClip _laserSoundClip;
    [SerializeField] private GameObject[] _playerEngineDamagedPrefabs;

    private float _movementSpeed = 5f;
    private int _playerOneScore;
    private int _playerTwoScore;
    private int _playerOneLives = 3;
    private int _playerTwoLives = 3;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;
    private float _nextFire = -1.0f;
    private int _speedMultiplier = 2;
    private Vector3 _laserOffsetPosition = new Vector3(0f, 0.75f, 0);
    private Vector3 _tripleLaserOffsetPosition = new Vector3(0f, 0.75f, 0);

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        switch(_gameManager._isCoopMode)
        {
            case false:
                if(_isPlayerOne != false)
                {
                    transform.position = new Vector3(0, -2.5f, 0);
                }
                break;

            case true:
                if (_isPlayerOne != false)
                {
                    transform.position = new Vector3(-5f, -2.5f, 0);
                }

                if (_isPlayerTwo != false)
                {
                    transform.position = new Vector3(5, -2.5f, 0);
                }
                break;
        }

        if (_spawnManager == null) 
        { 
            Debug.LogError("The Spawn Manager is NULL."); 
        }
        
        if (_uiManager == null) 
        {
            Debug.LogError("The UIManager is NULL.");
        }

        if (_audioSource == null) 
        {
            Debug.Log(" The AudioSource is NULL"); 
        }
        else 
        { 
            _audioSource.clip = _laserSoundClip; 
        }

        _playerEngineDamagedPrefabs[0].SetActive(false);
        _playerEngineDamagedPrefabs[1].SetActive(false);
    }

    void Update()
    {
        switch(_gameManager._isCoopMode)
        {
            case false:
                PlayerOneMovement();

                if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
                {
                    PlayerOneShoot();
                }
                break;

            case true:
                PlayerOneMovement();
                PlayerTwoMovement();

                if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
                {
                    PlayerOneShoot();
                }

                if (Input.GetKeyDown(KeyCode.KeypadEnter) && Time.time > _nextFire)
                {
                    PlayerTwoShoot();
                }
                break;
        }

    }

    void PlayerOneShoot()
    {
        if (_isPlayerOne == true)
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
    }

    void PlayerTwoShoot()
    {
        if (_isPlayerTwo == true)
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
    }

    void PlayerOneMovement()
    {
        if (_isPlayerOne == true)
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
    }

    void PlayerTwoMovement()
    {
        if (_isPlayerTwo == true)
        {
            float horizontalInput = Input.GetAxis("Horizontal 2");
            float verticallInput = Input.GetAxis("Vertical 2");
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
        if (_isPlayerOne == true)
        {
            _uiManager._powerUpText[0].gameObject.SetActive(false);
        }
        else if (_isPlayerTwo == true)
        {
            _uiManager._powerUpText2[0].gameObject.SetActive(false);

        }

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
        if (_isPlayerOne != false)
        {
            yield return new WaitForSeconds(5.0f);
            _uiManager._powerUpText[1].gameObject.SetActive(false);
            _isSpeedBoostActive = false;
            _movementSpeed /= _speedMultiplier;
        }
        else if (_isPlayerTwo != false)
        {
            yield return new WaitForSeconds(5.0f);
            _uiManager._powerUpText2[1].gameObject.SetActive(false);
            _isSpeedBoostActive = false;
            _movementSpeed /= _speedMultiplier;
        }

    }

    public void ShieldActive()
    {
        _isShieldActive = true;       
        _shieldPrefab.SetActive(true);       
    }

    public void PlayerOneScoreManager(int playerOnePoints)
    {
        _playerOneScore += playerOnePoints;

        _uiManager.PlayerOneUIScoreManager(_playerOneScore);
    }

    public void PlayerTwoScoreManager(int playerTwoPoints)
    {
        _playerTwoScore += playerTwoPoints;

        _uiManager.PlayerTwoUIScoreManager(_playerTwoScore);
    }

    public void PlayerOneDamage()
    {
        if (_isPlayerOne == true)
        {
            if (_isShieldActive == true)
            {
                _isShieldActive = false;
                _shieldPrefab.SetActive(false);
                _uiManager._powerUpText[2].gameObject.SetActive(false);
                return;
            }

            _playerOneLives--;
            _uiManager.UpdatePlayerOneLives(_playerOneLives);

            if (_playerOneLives == 2) { _playerEngineDamagedPrefabs[0].SetActive(true); }

            if (_playerOneLives == 1) { _playerEngineDamagedPrefabs[1].SetActive(true); }

            if (_playerOneLives < 1)
            {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _spawnManager.StopTheGame();
                Destroy(this.gameObject, 0.3f);
            }
        }

    }

    public void PlayerTwoDamage()
    {
        if (_isPlayerTwo == true)
        {
            if (_isShieldActive == true)
            {
                _isShieldActive = false;
                _shieldPrefab.SetActive(false);
                _uiManager._powerUpText2[2].gameObject.SetActive(false);
                return;
            }

            _playerTwoLives--;
            _uiManager.UpdatePlayerTwoLives(_playerTwoLives);

            if (_playerTwoLives == 2) { _playerEngineDamagedPrefabs[0].SetActive(true); }

            if (_playerTwoLives == 1) { _playerEngineDamagedPrefabs[1].SetActive(true); }

            if (_playerTwoLives < 1)
            {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _spawnManager.StopTheGame();
                Destroy(this.gameObject, 0.3f);
            }
        }
    }
}
