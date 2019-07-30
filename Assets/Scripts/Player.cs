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
    private Vector3 _endPos;
    private Rigidbody2D _rb;

    private int _color;
    public Coroutine Shooting;
    
    [SerializeField] private GameObject Rope;

    public bool CanJump;
    public bool IsRun;
    public bool CanShoot;

    public GameObject Part;
    public GameObject Target;
    
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
            if (Input.GetMouseButtonDown(0))
            {
                _targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);;
                _targetPos.z = 0;
                Target.transform.position = _targetPos;
                Target.SetActive(true);
            }
            
            if (Input.GetMouseButton(0))
            {
                    Part.SetActive(true);
               
                SetTarget(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                Part.SetActive(false);
                Shoot();    
                Target.SetActive(false);
            }
        }
        
    }

    private void OnDisable()
    {
        Handheld.Vibrate();
        OnDead?.Invoke();
    }

    public void SetStartPose(Vector3 pos)
    {
        if (IsRun)
        {
            _targetPos = pos;
            _targetPos.z = 0;
            Target.transform.position = _targetPos;
            Target.SetActive(true);
            Part.SetActive(true);
        }
        
    }
    
    public void SetTarget(Vector3 pos)
    {
        if (!CanJump)
        {
            //pos.z = 0;
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos.z = 0;
            _endPos = pos;
        
            Vector3 diff = pos - _targetPos;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
        }
    }
    
    

    public void Shoot()
    {
        Target.SetActive(false);
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
           _rb.AddForce((pos - transform.position).normalized * 3, ForceMode2D.Force);
           yield return null ;
        }
        IsRun = true;
        _rb.gravityScale = 0.35f;
        Rope.transform.parent = this.transform;
        Rope.transform.localPosition = Vector3.zero;
        Rope.GetComponent<Rope>().IsActivated = false;
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