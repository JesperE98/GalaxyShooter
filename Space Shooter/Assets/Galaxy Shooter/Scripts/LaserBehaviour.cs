using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    private GameManager _gameManager;
    private Player _player;
    private Enemy _enemy;

    private float _speed = 8.0f;
    private bool _isEnemyLaser = false;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = GameObject.Find("Player(Clone)").GetComponent<Player>();
    }

    void Update()
    {
        if (_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    private void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 7f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void MoveDown()
    {
        _speed = 6.0f;
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5.8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(_gameManager._isCoopMode)
        {
            case false:
                if (other.CompareTag("Player") && _isEnemyLaser == true)
                {
                    Player _player = other.GetComponent<Player>();
                    Enemy _enemy = other.GetComponent<Enemy>();

                    if (_player != null)
                    {
                        _player.PlayerOneDamage();
                    }
                    Destroy(this.gameObject);

                    if (_enemy == null)
                    {
                        this.gameObject.SetActive(false);
                    }
                }
                break;

            case true:
                if (other.CompareTag("Player") && _isEnemyLaser == true)
                {
                    Player _player = other.GetComponent<Player>();
                    Enemy _enemy = other.GetComponent<Enemy>();

                    if (_player != null)
                    {
                        _player.PlayerOneDamage();
                    }
                    Destroy(this.gameObject);

                    if (_enemy == null)
                    {
                        this.gameObject.SetActive(false);
                    }
                }
                else if (other.CompareTag("PlayerTwo") && _isEnemyLaser == true)
                {
                    Player _player = other.GetComponent<Player>();
                    Enemy _enemy = other.GetComponent<Enemy>();

                    if (_player != null)
                    {
                        _player.PlayerTwoDamage();
                    }
                    Destroy(this.gameObject);

                    if (_enemy == null)
                    {
                        this.gameObject.SetActive(false);
                    }
                }
                break;
        }

    }
}
