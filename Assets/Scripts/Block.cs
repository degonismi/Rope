using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool IsActive;
    public int Color;

    public void Hide()
    {
        GetComponent<Animator>().SetTrigger("Hide");
    }
    
    private void Start()
    {
        IsActive = false;
    }
}
