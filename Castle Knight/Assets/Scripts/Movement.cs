using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float MovementSpeed = 8;
    public float JumpSpeed = 8;
    public bool Grounded;

    private Rigidbody2D rb;
    private Animator animator;
    private bool turnedRight;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        MovementAnimation(movement);
        transform.Translate(new Vector3(movement * MovementSpeed, 0) * Time.deltaTime);
        if(Input.GetKeyUp(KeyCode.Z) && !Grounded)
        {
            rb.velocity = new Vector2(0, 0);
            Grounded = true;
        }
        CheckGrounded();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Grounded)
            {
                rb.velocity = new Vector2(0, JumpSpeed);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
    }

    void OnTriggerExit2D(Collider2D collider)
    {
    }

    void CheckGrounded()
    {
        RaycastHit2D[] rayHit;
        Vector2 position = transform.position - new Vector3(0, -0.5f);
        rayHit = Physics2D.RaycastAll(position, new Vector2(0, -1), 1.3f);

        Grounded = false;
        foreach (RaycastHit2D ray in rayHit)
        {
            if (ray.collider != null && !ray.transform.name.Equals("Player"))
            {
                Grounded = true;
            }
        }

    }

    void MovementAnimation(float movement)
    {
        if (movement > 0)
        {
            animator.Play("Walk_Right");
            turnedRight = true;
        }
        else if (movement < 0)
        {
            animator.Play("Walk_Left");
            turnedRight = false;
        }
        else if (movement == 0)
        {
            if (turnedRight)
            {
                animator.Play("Idle_Right");
            }
            else
            {
                animator.Play("Idle_Left");
            }
        }
    }
}
