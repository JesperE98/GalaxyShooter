using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _enemyLaserPrefab;
    [SerializeField] private GameObject _enemyThrusterPrefab;

    private float _movementSpeed = 3.0f;
    private float _laserSpeed = 8.0f;
    private float _fireRate = 3.0f;
    private float _nextFire = -1f;
    private bool _isEnemyDestroyed = false;

    private GameManager _gameManager;
    private Player _player;
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;
    private AudioSource _audioSource;

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        switch(_gameManager._isCoopMode)
        {
            case false: 
                _player = GameObject.Find("Player").GetComponent<Player>();
                break;

            case true:
                _player = GameObject.Find("Player").GetComponent<Player>();
                _player = GameObject.Find("Player_2").GetComponent<Player>();
                break;
        }

        _audioSource = GetComponent<AudioSource>();

        if (_player == null )
            {
                Debug.Log("The Player is NULL");
            }

        _animator = GetComponent<Animator>();

        if (_animator == null )
            {
                Debug.Log("The Player is NULL");
            }

        _boxCollider2D = GetComponent<BoxCollider2D>();

        if (_boxCollider2D == null )
        {
            Debug.Log("The Collider is NULL");
        }
    }


    void Update()
    {
        EnemyMovement();

        if (Time.time > _nextFire)
        {
            _nextFire = Random.Range(3f, 7f);
            _nextFire = Time.time + _fireRate;
            EnemyFire();
        }
    }

    private void EnemyMovement()
    {
        transform.Translate(Vector3.down * _movementSpeed * Time.deltaTime);
        if (transform.position.y <= -5.8f)
        {
            Object.Destroy(this.gameObject);
        }
    }

    void EnemyFire ()
    {
        if (_isEnemyDestroyed == false)
        {
            transform.Translate(Vector3.down * _laserSpeed * Time.deltaTime);
            GameObject _enemyLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
            LaserBehaviour[] _lasers = _enemyLaser.GetComponentsInChildren<LaserBehaviour>();

            for (int i = 0; i < _lasers.Length; i++)
            {
                _lasers[i].AssignEnemyLaser();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (_gameManager._isCoopMode)
        {
            case false:
                if (other.CompareTag("Player"))
                {
                    Destroy(_boxCollider2D);
                    _isEnemyDestroyed = true;
                    _enemyThrusterPrefab.SetActive(false);

                    if (_player != null)
                    {
                        _player.Damage();
                    }

                    _audioSource.Play();
                    _animator.SetTrigger("OnEnemyDeath");
                    _movementSpeed = 1.5f;
                    Destroy(this.gameObject, 3.0f);
                }
                else if (other.CompareTag("Laser"))
                {
                    Destroy(_boxCollider2D);
                    Destroy(other.gameObject);
                    _isEnemyDestroyed = true;
                    _enemyThrusterPrefab.SetActive(false);

                    if (_player != null)
                    {
                        _player.ScoreManager(10);
                    }

                    _audioSource.Play();
                    _animator.SetTrigger("OnEnemyDeath");
                    _movementSpeed = 1.5f;
                    Destroy(this.gameObject, 3.0f);
                }
                break;

            case true:
                if (other.CompareTag("Player"))
                {
                    Destroy(_boxCollider2D);
                    _isEnemyDestroyed = true;
                    _enemyThrusterPrefab.SetActive(false);

                    if (_player != null)
                    {
                        _player.Damage();
                    }

                    _audioSource.Play();
                    _animator.SetTrigger("OnEnemyDeath");
                    _movementSpeed = 1.5f;
                    Destroy(this.gameObject, 3.0f);
                }
                else if (other.CompareTag("Laser"))
                {
                    Destroy(_boxCollider2D);
                    Destroy(other.gameObject);
                    _isEnemyDestroyed = true;
                    _enemyThrusterPrefab.SetActive(false);

                    if (_player != null)
                    {
                        _player.ScoreManager(10);
                    }

                    _audioSource.Play();
                    _animator.SetTrigger("OnEnemyDeath");
                    _movementSpeed = 1.5f;
                    Destroy(this.gameObject, 3.0f);
                }

                if (other.CompareTag("PlayerTwo"))
                {
                    Destroy(_boxCollider2D);
                    _isEnemyDestroyed = true;
                    _enemyThrusterPrefab.SetActive(false);

                    if (_player != null)
                    {
                        _player.Damage();
                    }

                    _audioSource.Play();
                    _animator.SetTrigger("OnEnemyDeath");
                    _movementSpeed = 1.5f;
                    Destroy(this.gameObject, 3.0f);
                }
                else if (other.CompareTag("Laser"))
                {
                    Destroy(_boxCollider2D);
                    Destroy(other.gameObject);
                    _isEnemyDestroyed = true;
                    _enemyThrusterPrefab.SetActive(false);

                    if (_player != null)
                    {
                        _player.ScoreManager(10);
                    }

                    _audioSource.Play();
                    _animator.SetTrigger("OnEnemyDeath");
                    _movementSpeed = 1.5f;
                    Destroy(this.gameObject, 3.0f);
                }
                break;
        }



    }
}
