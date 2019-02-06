using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateBoss : MonoBehaviour {


    public GameObject boss;
    public AudioClip bossClip;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            boss.SetActive(true);
            collision.gameObject.GetComponentInChildren<Camera>().GetComponent<AudioSource>().clip = bossClip;
            collision.gameObject.GetComponentInChildren<Camera>().GetComponent<AudioSource>().Play();
            StartCoroutine(Transition(collision.gameObject.GetComponentInChildren<Camera>(), collision.gameObject));

            }
        }



    public float transitionDuration;
    IEnumerator Transition(Camera camera, GameObject player) {
        float t = 0.0f;
        Vector3 startingPos = camera.transform.position;
        while (t < 1.0f) {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);


            camera.transform.position = Vector3.Lerp(startingPos, new Vector3(player.transform.position.x, player.transform.position.y + 1, -10), t);
            yield return 0;
            }

        }
    }

    