using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    public double health;
    private float hitTimeCounter;

    public int Damage;
    public float HitTime;
    public bool Hit;
    public bool StopWhileHit;

    // Use this for initialization
    void Start() {
        StopWhileHit = true;
        }

    // Update is called once per frame
    void Update() {
        if (health <= 0 && !this.gameObject.name.Equals("Claudio")) {
            health = 0;
            this.gameObject.SetActive(false);
            }
        if (hitTimeCounter > 0) {
            hitTimeCounter -= Time.deltaTime;
            GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
            if (StopWhileHit) {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }
            }
        else {
            GetComponent<Renderer>().enabled = true;
            Hit = false;
            }
        if (!GameObject.Find("Player").GetComponent<Player>().invincible) {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Player").GetComponent<Collider2D>(), false);
            }
        }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Axe")) {
            health -= 35;
            print(health);
            hitTimeCounter = HitTime;
            Hit = true;
            }
        else if (collider.CompareTag("Sword")) {
            health -= 50;
            print(health);
            hitTimeCounter = HitTime;
            Hit = true;
            }
        }
    

void OnCollisionEnter2D(Collision2D collision) {
    if (GameObject.Find("Player").GetComponent<Player>().invincible && collision.collider.CompareTag("Player")) {

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
        }
    }
}
