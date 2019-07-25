using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private Player _player;
    public bool IsActivated;
    private void OnTriggerEnter2D(Collider2D other)
    {
        _player.CanJump = false;
        if (IsActivated)
        {
            if (!other.GetComponent<Block>())
            {
                transform.position = transform.parent.position;
            }

            if (other.GetComponent<Block>())
            {
                transform.position = other.transform.position;
                _player.StartMove(transform.position);
                other.GetComponent<Block>().IsActive = true;
            }

            if (other.GetComponent<MadBlock>())
            {
                other.GetComponent<MadBlock>().IsActivated = true;
                transform.position = other.transform.position;
                
                _player.StartMove(transform.position);
            }
        }
        
    }

    private void Update()
    {
        //if (Vector3.Distance(transform.position, _player.transform.position) >)
      //  {
        //    transform.position = _player.gameObject.transform.position;
       // }
    }
}
