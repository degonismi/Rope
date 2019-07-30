using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
    public Text Current;
    public Text Best;
    
    private void OnEnable()
    {
        Current.text = PlayerPrefs.GetInt("CurrentScore").ToString();
        Best.text = "Best " + PlayerPrefs.GetInt("BestScore").ToString();
    }
}
