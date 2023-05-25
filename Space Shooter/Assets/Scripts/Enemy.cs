using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    private float _movementSpeed = 3.0f;

    private Player _player;
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;
    private AudioSource _audioSource;

    void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
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
    }

    private void EnemyMovement()
    {
        
        transform.Translate(Vector3.down * _movementSpeed * Time.deltaTime);

        if (transform.position.y <= -5.8f)
        {
            Object.Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(_boxCollider2D);

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
            
            if (_player != null)
            {
                _player.ScoreManager(10);
            }

            _audioSource.Play();
            _animator.SetTrigger("OnEnemyDeath");
            _movementSpeed = 1.5f;
            Destroy(this.gameObject, 3.0f);
        }       
    }
}
