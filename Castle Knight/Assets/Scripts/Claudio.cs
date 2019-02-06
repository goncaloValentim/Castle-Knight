using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Claudio : MonoBehaviour {
    private bool turnedRight;
    private Animator animator;
    private Rigidbody2D rb;
    private float attackTimeCounter;
    private double health;

    public bool StartTurningRight;
    public float MovementSpeed;
    public float AttackTime;
    public GameObject Penguin;
    public float AttackDistance;
    public float AttackInterval;
    public List<GameObject> penguins;
    // Use this for initialization
    void Start() {
        turnedRight = StartTurningRight;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Attack();
        penguins = new List<GameObject>();
        }

    // Update is called once per frame
    void Update() {
        health = GetComponent<Enemy>().health;
        if (health > 0) {
            animator.SetBool("turnedRight", turnedRight);
            if (!animator.GetBool("attacking")) {
                int movement;
                if (turnedRight) {
                    movement = 1;
                    }
                else {
                    movement = -1;
                    }
                rb.velocity = new Vector3(movement * MovementSpeed, 0, 0);
                }
            else {
                rb.velocity = Vector3.zero;
                }

            if (attackTimeCounter > 0) {
                attackTimeCounter -= Time.deltaTime;
                }
            else {
                animator.SetBool("attacking", false);
                }
            List<GameObject> deadPenguins = new List<GameObject>();
            foreach (GameObject penguin in penguins) {
                if (!penguin.activeInHierarchy) {
                    deadPenguins.Add(penguin);
                    GetComponent<Enemy>().health -= 10;
                    }
                }
            foreach (GameObject penguin in deadPenguins) {
                penguins.Remove(penguin);
                }
            }
        else {
            if (animator.GetBool("attacking")) {
                StopAllCoroutines();
                animator.SetBool("attacking", false);
                }
            StartCoroutine(Death());
            }
        }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Wall")) {
            turnedRight = !turnedRight;
            animator.SetBool("attacking", true);
            Attack();
            attackTimeCounter = AttackTime;
            }
        }

    void Attack() {
        Vector3 position = transform.position;
        if (turnedRight) {
            position.x += AttackDistance;
            }
        else {
            position.x -= AttackDistance;
            }
        //Instantiate(Penguin, position, transform.rotation);
        StartCoroutine(Attacking(position));
        }

    IEnumerator Attacking(Vector3 position) {
        while (animator.GetBool("attacking")) {
            penguins.Add(Instantiate(Penguin, position, transform.rotation));
            yield return new WaitForSeconds(AttackInterval);
            }
        }

    IEnumerator Death() {
        float t = 0;
        animator.SetBool("death", true);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        while (t < 1.0f) {
            t += Time.deltaTime * (Time.timeScale / 5f);
            yield return 0;
            }
        SceneManager.LoadScene(4);
        }


    }