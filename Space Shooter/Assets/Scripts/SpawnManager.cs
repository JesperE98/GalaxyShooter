using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{  
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _powerUpContainer;
    [SerializeField] private GameObject[] _powerUps; 

    private bool _stopSpawning = false;
    private bool _randomPowerUpSpawning = false;


    void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }


    private IEnumerator EnemySpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 _postToSpawn = new Vector3(Random.Range(-9.5f, 9.5f), 8.0f, 0f);
            GameObject _newEnemy = Instantiate(_enemyPrefab, _postToSpawn, Quaternion.identity);
            _newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.5f);         
        }
        
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (_randomPowerUpSpawning == false)
        {
            Vector3 _postToSpawn = new Vector3(Random.Range(-9.5f, 9.5f), 8.0f, 0f);
            int _randomPowerUp = Random.Range(1,3);
            GameObject _newRandomPowerUp = Instantiate(_powerUps[_randomPowerUp], _postToSpawn, Quaternion.identity);
            _newRandomPowerUp.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(8, 20));
        }
    }

    public void StopTheGame()
    {
        _stopSpawning = true;
        _randomPowerUpSpawning = true;
    }
}
