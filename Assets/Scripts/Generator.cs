using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Generator : MonoBehaviour
{
    public GameObject[] AllBlocks;
    public GameObject ParentForBlock;
    [SerializeField] private int Count;

    private void Start()
    {
        GenStage();
    }

    [ContextMenu("Gen")]
    public void GenStage()
    {
        int numForBlock = Count;

        int num = -10;
        
        for (int k = 0; k < 500; k++)
        {
            int m = Random.Range(0, numForBlock);
            if (m == 0)
            {
                GameObject newBlock;
                numForBlock = Count;
                int j = Random.Range(-2, 3);
                int l = Random.Range(0, 3);
                Vector3 pos;
                if (l == 0)
                {
                    pos = new Vector3(j+0.5f,k,0);
                }
                else
                {
                    pos = new Vector3(j,k,0);
                }
                
                if (num < 4)
                {
                    num++;
                    newBlock = Instantiate(AllBlocks[Random.Range(0, 3)], pos, Quaternion.identity);
                }
                else
                {
                    num = 0;
                    newBlock = Instantiate(AllBlocks[Random.Range(4, AllBlocks.Length)], pos, Quaternion.identity);
                }
                
                newBlock.transform.parent = ParentForBlock.transform;
            }
            else
            {
                numForBlock--;
            }
        }
    }

    [ContextMenu("Del")]
    public void Destr()
    {
        GameObject[]blocks = new GameObject[ParentForBlock.transform.childCount];
        
        for(int i=0; i< ParentForBlock.transform.childCount; i++)
        {
            blocks[i] = ParentForBlock.transform.GetChild(i).gameObject;
            
        }

        foreach (var f in blocks)
        {
           DestroyImmediate(f); 
        }
    }


}
