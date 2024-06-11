using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private float timer;
    private bool isScored;
    private GameObject character;

    private void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        if (character == null)
        {
            character = GameObject.FindGameObjectWithTag("Player");
        }
        isScored = false;
        timer = Time.time;
        rb.velocity = Vector2.left * speed;
    }

    private void Update()
    {
        if (Time.time - timer >= 6f)
        {
            Destroy(gameObject);
        }
        if(transform.position.x < character.transform.position.x && !isScored)
        {
            isScored = true;
            PlayerController script = character.GetComponent<PlayerController>();
            script.score++;

        }
    }
}
