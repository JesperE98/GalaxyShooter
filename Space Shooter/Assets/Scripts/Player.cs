using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 2f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = -1.0f;
    private Vector3 _laserOffsetPosition = new Vector3(0f, 1.3f, 0);
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
            _playerShoot();
        }

    }

    void _playerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticallInput = Input.GetAxis("Vertical");

        Vector3 _direction = new Vector3(horizontalInput, verticallInput, 0f);

        transform.Translate(_direction * _movementSpeed * Time.deltaTime);
/*
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0f, 0f);
        }
        else if (transform.position.y <= -3.6f)
        {
            transform.position = new Vector3(transform.position.x, -3.6f, 0f);
        }
*/
        // Alternativ kodrad för samma funktion som if/else if koden ovanför.
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

    void _playerShoot()
    {

        _nextFire = Time.time + _fireRate;

        // Skapar (i detta fall) min laser prefab vid spelarens position. identity läser av rotationen.
        Instantiate(_laserPrefab, transform.position + _laserOffsetPosition, Quaternion.identity);

    }

    public void Damage()
    {
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
