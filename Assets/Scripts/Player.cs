using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Скорость шарика")]
    public float RopeSpeed;
    
    private Vector3 _targetPos;
    private Rigidbody2D _rb;

    public Coroutine Shooting;
    
    [SerializeField] private GameObject Rope;

    public bool CanJump;
    public bool IsRun;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        IsRun = true;
    }

    private void Update()
    {
        if (IsRun)
        {
            if (Input.GetMouseButton(0))
            {
                SetTarget(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                Shoot();    
            }
        }
        
    }

    
    
    public void SetTarget(Vector3 pos)
    {
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos.z = 0;
        _targetPos = pos;
        
        Vector3 diff = pos - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
    }

    public void Shoot()
    {
        Rope.transform.position = transform.position;
        CanJump = true;
        Shooting = StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        while (CanJump)
        {
            Rope.transform.Translate(Vector3.up * Time.deltaTime * RopeSpeed);
            yield return new WaitForEndOfFrame();
        }
    }

    public void StartMove(Vector3 posForMove)
    {
       StopAllCoroutines();
       StartCoroutine(Move(posForMove));
    }
    
    IEnumerator Move(Vector3 pos)
    {
        IsRun = false;
        while ( Mathf.Abs(transform.position.y - pos.y) > 0)
        {
            Rope.transform.parent = null;
           transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * 3 );
           yield return null ;
       }

        IsRun = true;
        Rope.transform.parent = this.transform;
        Rope.transform.localPosition = Vector3.zero;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Block>() && other.GetComponent<Block>().IsActive)
        {
            Color c = other.GetComponent<SpriteRenderer>().color;
            Destroy(other.gameObject);
            GetComponent<SpriteRenderer>().color = c;
            GetComponent<TrailRenderer>().startColor = c;
            GetComponent<TrailRenderer>().endColor = c;
        }
        
    }
}