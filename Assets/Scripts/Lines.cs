using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    [SerializeField] private Transform _player;
    Vector3[] pos = new Vector3[2];
    
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
       
    }

    private void Update()
    {
        pos[0] = transform.position;
        pos[1] = _player.position;
        _lineRenderer.SetPositions(pos);
    }
}
