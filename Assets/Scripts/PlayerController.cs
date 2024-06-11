using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public bool isFinish;

    public int score;

    private void Awake()
    {
        score = 0;
    }
    private void Start()
    {
        isFinish = false;
    }
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 pointTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (pointTouch.x < 0f)
            {
                Vector2.MoveTowards(transform.position, new Vector2(0, -1.5f), Time.deltaTime * speed);
            }
            else if (pointTouch.x > 0f)
            {
                Vector2.MoveTowards(transform.position, new Vector2(0, -0.5f), Time.deltaTime * speed);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Trap"))
        {
            isFinish=true;
        }
        if (collision.collider.CompareTag("Fruit"))
        {
            score++;
            Destroy(collision.gameObject);
        }
    }
}
