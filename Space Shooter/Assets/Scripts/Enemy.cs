using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 4.0f;

    //private int _playerHealth = 100;
    //private int _enemyChrashDamage = 25;


    void Update()
    {
        EnemyMovement();
    }

    public void EnemyMovement()
    {
        
        transform.Translate(Vector3.down * _movementSpeed * Time.deltaTime);

        if (transform.position.y <= -5.8f)
        {
            float RandomX = Random.Range(-9.5f, 9.5f);
            transform.position = new Vector3(RandomX, 8f, 0f);
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
