using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;

    private float _rotateSpeed = 19.0f;
    //private float _speed = 1.5f;

    private SpawnManager _spawnManager;

    void Awake()
    {

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        //AsteroidMovement();

        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

   /* void AsteroidMovement ()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
   */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.3f);
            
        }
        
    }
}
