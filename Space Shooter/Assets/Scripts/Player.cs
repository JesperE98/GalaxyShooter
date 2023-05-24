using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
  
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldPrefab;

    private int _lives = 3;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;
    private float _nextFire = -1.0f;
    [SerializeField] private float _movementSpeed = 5f;
    private int _speedMultiplier = 2;
    private Vector3 _laserOffsetPosition = new Vector3(0f, 0.75f, 0);
    private Vector3 _tripleLaserOffsetPosition = new Vector3(0f, 0.75f, 0);

    private SpawnManager _spawnManager;



    void Start()
    {
        transform.position = new Vector3(0, -2.5f, 0);    
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
    }

    void Update()
    {
        _playerMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;

            if (_isTripleShotActive == false)
            {
                Instantiate(_laserPrefab, transform.position + _laserOffsetPosition, Quaternion.identity);
            }
            else if (_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position + _tripleLaserOffsetPosition, Quaternion.identity);
            }
        }


    }

    void _playerMovement()
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

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        _isTripleShotActive = false;

    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true; 
        _movementSpeed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _movementSpeed /= _speedMultiplier;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;       
        _shieldPrefab.SetActive(true);       
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldPrefab.SetActive(false);
            return;
        }

        _lives--;
        Debug.Log("Health Remaining: " + _lives);
        if (_lives < 1)
        {
            _spawnManager.StopTheGame();
            Destroy(this.gameObject);
            Debug.Log("Game Over");
            
        }
    }


}
