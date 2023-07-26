using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private Player _player;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player._isPlayerOne == true)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _anim.SetBool("Turn_Left", true);
                _anim.SetBool("Turn_Right", false);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                _anim.SetBool("Turn_Left", false);
                _anim.SetBool("Turn_Right", false);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                _anim.SetBool("Turn_Left", false);
                _anim.SetBool("Turn_Right", true);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                _anim.SetBool("Turn_Left", false);
                _anim.SetBool("Turn_Right", false);
            }
        }
        else if (_player._isPlayerTwo == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _anim.SetBool("Turn_Left", true);
                _anim.SetBool("Turn_Right", false);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                _anim.SetBool("Turn_Left", false);
                _anim.SetBool("Turn_Right", false);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _anim.SetBool("Turn_Left", false);
                _anim.SetBool("Turn_Right", true);
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                _anim.SetBool("Turn_Left", false);
                _anim.SetBool("Turn_Right", false);
            }
        }

    }
}
