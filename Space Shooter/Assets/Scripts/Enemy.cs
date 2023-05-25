using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 4.0f;
    private Player _player;

    void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
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
            if (_player != null)
            {
                _player.Damage();
            }

            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Projectile"))
        {           
            Destroy(other.gameObject);
            Destroy(this.gameObject);

            if (_player != null)
            {
                _player.ScoreManager(10);

            }
            
                                  
        }       
    }
}
