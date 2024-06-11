using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private float timer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Start()
    {
        timer = Time.time;
        rb.velocity = Vector2.left * speed;
    }
    private void Update()
    {
        if (Time.time - timer >= 6f)
        {
            Destroy(gameObject);
        }
    }
}
