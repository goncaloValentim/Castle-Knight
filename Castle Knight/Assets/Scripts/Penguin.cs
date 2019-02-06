using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : MonoBehaviour
{
    private bool turnedRight;
    private Rigidbody2D rb;
    private Animator animator;

    public float MovementSpeed;
    public bool StartTurnedRight;

    // Use this for initialization
    void Start()
    {
        turnedRight = StartTurnedRight;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("turnedRight", turnedRight);
        int movement;
        if(turnedRight)
        {
            movement = 1;
        }
        else
        {
            movement = -1;
        }
        rb.velocity = new Vector3(movement * MovementSpeed, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Enemy")) {
            turnedRight = !turnedRight;
            }
        }
}
