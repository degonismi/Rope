using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Скорость шарика")]
    public float RopeSpeed;
    
    private Vector3 _targetPos;
    private Rigidbody2D _rb;

    private int _color;
    public Coroutine Shooting;
    
    [SerializeField] private GameObject Rope;

    public bool CanJump;
    public bool IsRun;
    public bool CanShoot;

    public GameObject Part;
    
    public UnityEvent OnDead;
    
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
                //if (!Part)
               // {
                    Part.SetActive(true);
               //}
                SetTarget(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                Part.SetActive(false);
                Shoot();    
            }
        }
        
    }

    private void OnDisable()
    {
        OnDead?.Invoke();
    }

    public void SetTarget(Vector3 pos)
    {
        if (!CanJump)
        {
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos.z = 0;
            _targetPos = pos;
        
            Vector3 diff = pos - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
        }
       
    }

    public void Shoot()
    {
        Rope.transform.position = transform.position;
        CanJump = true;
        Rope.GetComponent<Rope>().IsActivated = true;
        Shooting = StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        while (CanJump)
        {
            Rope.transform.Translate(Vector3.up * Time.deltaTime * RopeSpeed);
            yield return null;
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
        _rb.velocity = Vector3.zero;
        _rb.gravityScale = 0;
        while (!IsRun && transform.position.y < pos.y)
        {
            Rope.transform.parent = null;
           //transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * 3 );
           _rb.AddForce((pos - transform.position).normalized * 3, ForceMode2D.Force);
           yield return null ;
        }
        IsRun = true;
        _rb.gravityScale = 0.35f;
        Rope.transform.parent = this.transform;
        Rope.transform.localPosition = Vector3.zero;
        Rope.GetComponent<Rope>().IsActivated = false;
        //Time.fixedTime
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Block>() && other.GetComponent<Block>().IsActive)
        {
            IsRun = true;
            Rope.transform.parent = this.transform;
            Rope.transform.localPosition = Vector3.zero;
            Rope.GetComponent<Rope>().IsActivated = false;
            
            Color c = other.GetComponent<SpriteRenderer>().color;
            Destroy(other.GetComponent<Collider2D>());
            other.GetComponent<Block>().Hide();
            Destroy(other.gameObject, 0.5f);
            GetComponent<SpriteRenderer>().color = c;
            GetComponent<TrailRenderer>().startColor = c;
            GetComponent<TrailRenderer>().endColor = c;

            _color = other.GetComponent<Block>().Color;
        }

        if (other.GetComponent<MadBlock>())
        {
            if (_color != other.GetComponent<MadBlock>().Color)
            {
                Destroy(gameObject);
            }
            else if(other.GetComponent<MadBlock>().IsActivated)
            {
                Destroy(other.GetComponent<Collider2D>());
                other.GetComponent<MadBlock>().Hide();
                Destroy(other.gameObject, 0.5f);
            }
        }
        
    }
}