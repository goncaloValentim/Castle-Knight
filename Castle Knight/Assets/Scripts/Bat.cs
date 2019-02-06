using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour {
    public bool StartDirectionRight;
    public float MovementSpeed;

    private Animator animator;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        if (StartDirectionRight) {
            animator.SetBool("turnedRight", true);
            }
        else {
            animator.SetBool("turnedRight", false);
            }
        rb = GetComponent<Rigidbody2D>();
        }

    // Update is called once per frame
    void Update() {

        }

    void FixedUpdate() {
        if (animator.GetBool("turnedRight")) {
            rb.velocity = new Vector3(MovementSpeed, 0, 0);
            }
        else {
            rb.velocity = new Vector3(-1 * MovementSpeed, 0, 0);
            }
        }

    void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.collider.gameObject.tag.Equals("Player") || collision.collider.gameObject.tag.Equals("Jumping Cunt")) {
            
            animator.SetBool("turnedRight", !animator.GetBool("turnedRight"));
            }
           
        }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Invisible Wall") || collider.CompareTag("Invisible Ground")) {
            animator.SetBool("turnedRight", !animator.GetBool("turnedRight"));
            }
        }
    }
