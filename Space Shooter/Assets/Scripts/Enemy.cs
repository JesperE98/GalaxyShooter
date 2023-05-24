using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 4.0f;

    void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        
        transform.Translate(Vector3.down * _movementSpeed * Time.deltaTime);

        if (transform.position.y <= -5.8f)
        {
            Object.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();

            Debug.Log("HIT! : " + other);  

            if (player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Projectile"))
        {          
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
