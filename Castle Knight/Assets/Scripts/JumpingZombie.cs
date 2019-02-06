using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingZombie : MonoBehaviour
{
    public bool StartTurnedRight;
    public float JumpForce;
    public float MovementSpeed;
    public bool Grounded;

    private Rigidbody2D rb;
    private bool turnedRight;
    private Animator animator;

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
        Animation();
        int direction;
        if (turnedRight)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        if (Grounded)
        {
            rb.velocity = new Vector3(direction * MovementSpeed, JumpForce, 0);
            Grounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Grounded = true;
        }
        else if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Enemy"))
        {
            turnedRight = !turnedRight;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Invisible Wall"))
        {
            turnedRight = !animator.GetBool("turnedRight");
        }
    }

    void Animation()
    {
        animator.SetBool("turnedRight", turnedRight);
    }
}
