using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public bool StartDirectionRight;
    public float MovementSpeed;
    public float DetectionDistance;
    public float AttackDistance;
    public Enemy enemy;
    private Animator animator;
    private Rigidbody2D rb;
    private float distanceToPlayer;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (StartDirectionRight)
        {
            animator.SetBool("turnedRight", true);
        }
        else
        {
            animator.SetBool("turnedRight", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("attacking", true);
            animator.SetBool("walking", false);
            rb.velocity = Vector3.zero;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !animator.GetBool("attacking"))
        {
            distanceToPlayer = Vector3.Distance(GameObject.Find("Player").transform.position, transform.position);
            if (distanceToPlayer <= DetectionDistance)
            {
                animator.SetBool("walking", true);
                if (GameObject.Find("Player").transform.position.x < transform.position.x)
                {
                    rb.velocity = new Vector3(-1 * MovementSpeed, 0, 0);
                    animator.SetBool("turnedRight", false);
                }
                else
                {
                    rb.velocity = new Vector3(MovementSpeed, 0, 0);
                    animator.SetBool("turnedRight", true);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy.Hit && !animator.GetBool("attacking"))
        {
            distanceToPlayer = Vector3.Distance(GameObject.Find("Player").transform.position, transform.position);
            if (distanceToPlayer <= DetectionDistance)
            {
                animator.SetBool("attacking", false);
                animator.SetBool("walking", true);
                if (GameObject.Find("Player").transform.position.x < transform.position.x)
                {
                    rb.velocity = new Vector3(-1 * MovementSpeed, 0, 0);
                    animator.SetBool("turnedRight", false);
                }
                else
                {
                    rb.velocity = new Vector3(MovementSpeed, 0, 0);
                    animator.SetBool("turnedRight", true);
                }
            }
        }
        else animator.SetBool("walking", false);
    }
    public void endAttack()
    {
        animator.SetBool("attacking", false);
        GetComponent<CircleCollider2D>().enabled = false;
    }


    public void attack()
    {
        GetComponent<CircleCollider2D>().enabled = true;

    }
}
