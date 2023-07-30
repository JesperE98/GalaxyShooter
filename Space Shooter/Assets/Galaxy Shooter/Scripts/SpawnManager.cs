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
    [SerializeField] private GameObject _playerContainer;
    [SerializeField] private GameObject[] _powerUps;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _player2;

    private bool _stopSpawning = false;
    private bool _randomPowerUpSpawning = false;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        switch (_gameManager._isCoopMode)
        {
            case false:
                GameObject _newPlayer = Instantiate(_player, Vector3.zero, Quaternion.identity);
                _newPlayer.transform.parent = _playerContainer.transform;
                break;

            case true:
                GameObject _newPlayer1 = Instantiate(_player, Vector3.zero, Quaternion.identity);
                GameObject _newPlayer2 = Instantiate(_player2, Vector3.zero, Quaternion.identity);

                _newPlayer1.transform.parent = _playerContainer.transform;
                _newPlayer2.transform.parent = _playerContainer.transform;
                break;
        }
    }

    public void SinglePlayerMode()
    {
        GameObject _newPlayer = Instantiate(_player, Vector3.zero, Quaternion.identity);
        _newPlayer.transform.parent = _playerContainer.transform;
    }

    public void CoopMode()
    {
        GameObject _newPlayer1 = Instantiate(_player, Vector3.zero, Quaternion.identity);
        GameObject _newPlayer2 = Instantiate(_player2, Vector3.zero, Quaternion.identity);

        _newPlayer1.transform.parent = _enemyContainer.transform;
        _newPlayer2.transform.parent = _enemyContainer.transform;
    }

    public void StartSpawning()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    private IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(3.0f);
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
        yield return new WaitForSeconds(4.0f);
        while (_randomPowerUpSpawning == false)
        {
            Vector3 _postToSpawn = new Vector3(Random.Range(-9.5f, 9.5f), 8.0f, 0f);
            int _randomPowerUp = Random.Range(0,3);
            GameObject _newRandomPowerUp = Instantiate(_powerUps[_randomPowerUp], _postToSpawn, Quaternion.identity);
            _newRandomPowerUp.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(7, 17));
        }
    }

    public void StopTheGame()
    {
        switch(_gameManager._isCoopMode)
        {
            case false:
                if (_gameManager._player1AreDead == true)
                {
                    _stopSpawning = true;
                    _randomPowerUpSpawning = true;
                }
                break;

            case true:
                if (_gameManager._player1AreDead == true && _gameManager._player2AreDead == true)
                {
                    _stopSpawning = true;
                    _randomPowerUpSpawning = true;
                }
                else
                {
                    _stopSpawning = false;
                    _randomPowerUpSpawning = false;
                }
                break;                
        }
    }
}
