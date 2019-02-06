using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float MovementSpeed = 8;
    public float JumpSpeed = 8;
    public bool Grounded;
    public bool Falling;
    public bool TurnedRight;
    public float AttackTime;
    public float JumpAttackSpeed = 5;
    public bool Attacking;
    public AudioClip AxeSwing;
    public float KnockBackTime;
    public float KnockBackForce;
    public float PostHitInvinvibility;
    private Rigidbody2D rb;
    private Animator animator;
    private bool airAttack;
    private float attackTimeCounter;
    private float knockBackTimeCounter;
    public int health;
    private AudioSource audioSource;
    public bool hit;
    private float postHitInvinvibilityCounter;
    public bool invincible;
    private Collider2D lastEnemyCollider;
    public Health heartUI;
    // Use this for initialization
    void Start() {
        Falling = false;
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
        TurnedRight = true;
        animator.SetBool("hit", false);
        hit = false;
        health = 100;
        audioSource = GetComponent<AudioSource>();
        }

    // Update is called once per frame
    void Update() {

        CheckGrounded();
        if (Input.GetKeyDown(KeyCode.Z) && Grounded) {
            rb.velocity = new Vector2(0, JumpSpeed);
            Falling = true;
            }

        }

    void LateUpdate() {
        UpdateHealth();
        if (!Attacking && !hit) {
            float movement = Input.GetAxis("Horizontal");
            Animation(movement);
            if (Input.GetKeyDown(KeyCode.X)) {
                if (!Grounded) {
                    if (!airAttack) {
                        JumpAttackAnimation();
                        airAttack = true;
                        animator.SetBool("airAttack", true);
                        rb.velocity = new Vector2(JumpAttackSpeed * movement, rb.velocity.y / 2);
                        }
                    }
                else {
                    AttackAnimation();
                    }
                }
            if (Grounded) {
                rb.velocity = new Vector3(movement * MovementSpeed, rb.velocity.y);
                }
            else {
                rb.velocity = new Vector3((movement * MovementSpeed), rb.velocity.y);
                }
            if (Input.GetKeyUp(KeyCode.Z) && !Grounded) {
                if (Falling && !airAttack) {
                    rb.velocity = new Vector2(0, rb.velocity.y / 2);
                    Falling = false;
                    }
                }
            }
        else {
            if (attackTimeCounter > 0) {
                attackTimeCounter -= Time.deltaTime;
                }
            else {
                Attacking = false;
                animator.SetBool("attacking", false);
                }
            }
        if (knockBackTimeCounter > 0) {
            knockBackTimeCounter -= Time.deltaTime;
            }
        else {
            animator.SetBool("hit", false);
            hit = false;
            }
        if (postHitInvinvibilityCounter > 0) {
            postHitInvinvibilityCounter -= Time.deltaTime;
            GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
            }
        else {
            invincible = false;
            GetComponent<Renderer>().enabled = true;
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), lastEnemyCollider, false);
            }
        }



    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Invisible Ground")) {
            rb.transform.position = new Vector3(1.61f, 1.50f, 0);
            }
        if (collider.CompareTag("Sword_Pickup")) {
            transform.Find("Weapon").gameObject.SetActive(false);
            transform.Find("Sword").gameObject.SetActive(true);
            }

        }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Enemy")) {
            if (!invincible) {
                print("enemy collision");
                int damage = collision.collider.gameObject.GetComponent<Enemy>().Damage;
                print("damage: " + damage);
                health -= damage;
                UpdateHealth();
                float direction = collision.transform.position.x - GetComponent<Collider2D>().transform.position.x;
                KnockBack(direction);
                postHitInvinvibilityCounter = PostHitInvinvibility;
                invincible = true;
                }
            else if (invincible) {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
                lastEnemyCollider = collision.collider;
                }
            }
        }

    void CheckGrounded() {
        RaycastHit2D[] rayHit;
        Vector2 position = transform.position;
        rayHit = Physics2D.BoxCastAll(position, rb.GetComponent<BoxCollider2D>().size, 90, new Vector2(0, -1), 0.50f);
        foreach (RaycastHit2D ray in rayHit) {
            if (ray.collider != null && (ray.transform.tag.Equals("Invisible Wall") || ray.transform.tag.Equals("Sword_Pickup"))) {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), ray.collider);
                }
            else if (ray.collider != null && !ray.transform.name.Equals("Player")) {
                Grounded = true;
                Falling = false;
                airAttack = false;
                animator.SetBool("airAttack", false);
                if (knockBackTimeCounter <= 0) {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    }
                
                
                }
            else {
                Falling = true;
                Grounded = false;
                }
            }
        }

    void MovementAnimation(float movement) {
        if (movement > 0) {
            animator.Play("Walk_Right");
            TurnedRight = true;
            animator.SetBool("turnedRight", true);
            }
        else if (movement < 0) {
            animator.Play("Walk_Left");
            TurnedRight = false;
            animator.SetBool("turnedRight", false);
            }
        else if (movement == 0 && !Attacking) {
            if (TurnedRight) {
                animator.Play("Idle_Right");
                }
            else {
                animator.Play("Idle_Left");
                }
            }
        }

    void JumpAnimation(float movement) {
        if (movement > 0) {
            animator.Play("Jump_Right");
            TurnedRight = true;
            animator.SetBool("turnedRight", true);
            }
        else if (movement < 0) {
            animator.Play("Jump_Left");
            TurnedRight = false;
            animator.SetBool("turnedRight", false);
            }
        else {
            if (TurnedRight) {
                animator.Play("Jump_Right");
                }
            else {
                animator.Play("Jump_Left");
                }
            }
        }

    void AttackAnimation() {
        attackTimeCounter = AttackTime;
        animator.SetBool("attacking", true);
        Attacking = true;
        rb.velocity = Vector2.zero;
        audioSource.PlayOneShot(AxeSwing);
        }

    void JumpAttackAnimation() {
        attackTimeCounter = AttackTime;
        animator.SetBool("attacking", true);
        Attacking = true;
        JumpAnimation(Input.GetAxis("Horizontal"));
        audioSource.PlayOneShot(AxeSwing);
        }

    void Animation(float movement) {
        if (Grounded) {
            MovementAnimation(movement);
            }
        else {
            if (movement > 0) {
                animator.Play("Jump_Right");
                TurnedRight = true;
                animator.SetBool("turnedRight", true);
                }
            else if (movement < 0) {
                animator.Play("Jump_Left");
                TurnedRight = false;
                animator.SetBool("turnedRight", false);
                }
            else {
                JumpAnimation(movement);
                }
            }
        }

    void ResetVelocity() {
        if (airAttack) {
            rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * 3, rb.velocity.y / 2));
            }
        }

    void UpdateHealth() {
        animator.SetFloat("health", health);
        if (health <= 0) {
            //health = 100;
            //rb.transform.position = new Vector3(1.61f, 1.50f, 0);
            animator.Play("Death_Right");
            rb.velocity = Vector2.zero;
            }
        heartUI.UpdateHearts(health);
        }

    void KnockBack(float distance) {
        float direction = 0;
        if (distance > 0)
            direction = 1;
        else
            direction = -1;
        knockBackTimeCounter = KnockBackTime;
        animator.SetBool("hit", true);
        rb.velocity = new Vector2(-1 * direction   * KnockBackForce, 2);
        hit = true;
        }
    }