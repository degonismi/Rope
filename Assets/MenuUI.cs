using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Text BestScore;
    private void OnEnable()
    {
        BestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
    }
}
