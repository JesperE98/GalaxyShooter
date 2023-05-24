using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _powerUpSpeed = 3.0f;
    [SerializeField] private int _powerUpID;

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
        if (other.tag == "Player")
        {
            switch (_powerUpID)
            {
                case 0:
                    _player.TripleShotActive();
                    print("You got the Triple Shot Power Up!");
                    Destroy(this.gameObject);
                    break;

                case 1:
                    _player.SpeedBoostActive();
                    print("You got the Speed Boost Power Up!");
                    Destroy(this.gameObject);
                    break;

                case 2:
                    _player.ShieldActive();
                    print("You got the Shield Power Up!");
                    Destroy(this.gameObject);
                    break;

                default:
                    Debug.Log("Default Value");
                    break;
            }
        }
    }
}
