using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{  
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _tripleShotPowerUpPrefab;
    [SerializeField] private GameObject _powerUpContainer;

    private bool _stopSpawning = false;
    public bool _tripleShotPowerUpSpawning = false;

    void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
       
    }


    private IEnumerator EnemySpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            GameObject _newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-9.5f, 9.5f), 8.0f, 0f), Quaternion.identity);
            _newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.5f);         
        }
        
    }

    private IEnumerator PowerUpSpawnRoutine()
    {
        while (_tripleShotPowerUpSpawning == false)
        {
            GameObject _newPowerUp = Instantiate(_tripleShotPowerUpPrefab, new Vector3(Random.Range(-9.5f, 9.5f), 8.0f, 0f), Quaternion.identity);
            _newPowerUp.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(10, 20));
        }
    }

    public void StopTheGame()
    {
        _stopSpawning = true;
        _tripleShotPowerUpSpawning = true;
    }
}
