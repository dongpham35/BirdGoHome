using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public bool isFinish;

    public int score;

    private Rigidbody2D rb;

    private void Awake()
    {
        score = 0;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isFinish = false;
        
    }
    private void Update()
    {
        if (score > PlayerPrefs.GetInt("Score"))
        {
            PlayerPrefs.SetInt("Score", score);
        }
        if (MenuController.isPlay)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pointTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (pointTouch.x > 0f)
                {
                    rb.velocity = Vector2.down * speed;
                }
                else if (pointTouch.x < 0f)
                {
                    rb.velocity = Vector2.up * speed;

                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Trap"))
        {
            rb.velocity = Vector2.zero;
            isFinish=true;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruit"))
        {
            score++;
            Destroy(collision.gameObject);
            rb.velocity = Vector2.zero;
        }
    }
}
