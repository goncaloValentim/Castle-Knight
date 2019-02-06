using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Pickup : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            print("sword pickup");
            transform.Find("Sword").gameObject.SetActive(true);
            transform.Find("Weapon").gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
