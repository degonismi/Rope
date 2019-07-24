using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text _text;
    private Camera _cam;
    private int _score;
    private void Start()
    {
        _text = GetComponent<Text>();
        _cam = Camera.main;
    }

    private void Update()
    {
        _score = Convert.ToInt32(_cam.transform.position.y * 10);
        _text.text = _score.ToString();
    }
}
