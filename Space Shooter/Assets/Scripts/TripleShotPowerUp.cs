using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotPowerUp : MonoBehaviour
{
    [SerializeField] private float _powerUpSpeed = 3.0f;

    private Player _player;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        PowerUpMovement();
    }

    void PowerUpMovement()
    {
        transform.Translate(Vector3.down * _powerUpSpeed * Time.deltaTime);

        if (transform.position.y <= -5.8f)
        {
            Object.Destroy(gameObject);
            _player._tripleShotActive = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {          
            _player._tripleShotActive = true;
            print("You got the Triple Shot Power Up!");
            Destroy(gameObject);

        }
    }
}
