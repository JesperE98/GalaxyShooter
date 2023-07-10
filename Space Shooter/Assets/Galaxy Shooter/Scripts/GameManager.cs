using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject _player1;
    public GameObject _player2;
    public bool _isCoopMode = false;

    private SpawnManager _spawnManager;

    [SerializeField] private bool _isGameOver = false;

    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_isCoopMode == false)
        {
            Instantiate(_player1, new Vector3(0, -2.5f, 0), Quaternion.identity);
        }
        else if (_isCoopMode == true)
        {
            Instantiate(_player1, new Vector3(-5f, -2.5f, 0), Quaternion.identity);
            Instantiate(_player2, new Vector3(5f, -2.5f, 0), Quaternion.identity);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
