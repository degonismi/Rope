using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private Player _player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<Block>())
        {
            _player.CanJump = false;
            transform.position = transform.parent.position;
        }

        if (other.GetComponent<Block>())
        {
           transform.position = other.transform.position;
           _player.StartMove(transform.position);
           other.GetComponent<Block>().IsActive = true;
           _player.CanJump = false;
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) > 7)
        {
            transform.position = transform.parent.position;
        }
    }
}
