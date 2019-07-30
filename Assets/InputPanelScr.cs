using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputPanelScr : MonoBehaviour
{
    private Player _player;
    
    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    private void OnMouseDown()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _player.SetStartPose(pos);
        //Debug.Log();
    }

    private void OnMouseDrag()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _player.SetTarget(pos);
        Debug.Log("AS");
    }

    private void OnMouseUp()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _player.Shoot();
    }
    
    
}
