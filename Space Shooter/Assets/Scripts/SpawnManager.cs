using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    private bool _stopSpawning = false;

    
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    void Start()
    {
        StartCoroutine(SpawnRoutine());

    }


    public IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            GameObject _newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-9.5f, 9.5f), 8.0f, 0f), Quaternion.identity);
            _newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.5f);         
        }
        
    }

    public void StopTheGame()
    {
        _stopSpawning = true;
    }
}
