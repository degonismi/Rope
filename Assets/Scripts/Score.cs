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
    private int i;
    private void Start()
    {
        _text = GetComponent<Text>();
        _cam = Camera.main;
        i = -1;
    }

    private void Update()
    {
        _score = Convert.ToInt32(_cam.transform.position.y * 10);
        if (_score/100 > i)
        {
            i++;
            Debug.Log("Get");
        }
        _text.text = _score.ToString();
    }

    private void OnDisable()
    {
        int max = PlayerPrefs.GetInt("BestScore",0);
        if(max<_score)
        PlayerPrefs.SetInt("BestScore", _score);
        
        PlayerPrefs.SetInt("CurrentScore", _score);
    }
}
