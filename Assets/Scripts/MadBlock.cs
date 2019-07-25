using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadBlock : MonoBehaviour
{
    public bool IsActivated;

    public void Hide()
    {
        GetComponent<Animator>().SetTrigger("Hide");
    }

    private void Start()
    {
        IsActivated = false;
    }

    public int Color;
}
