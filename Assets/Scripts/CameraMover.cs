using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{

    [SerializeField] private Transform _player;
    private Vector3 pos;

    private void Update()
    {
        if (_player)
        {
            if (transform.position.y-3  < _player.position.y)
            {
                pos = new Vector3(0, _player.position.y +3, -10);
                transform.position = Vector3.Lerp(transform.position, pos, 3 * Time.deltaTime);
            }
        }
        
           
    }
}
