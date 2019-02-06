using UnityEngine;
using System.Collections;
public class PauseGame : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject helpMenu;
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
            }
        }
    public void Pause() {
        if (helpMenu.activeInHierarchy) {
            helpMenu.SetActive(false);
            pauseMenu.SetActive(true);
            }
        else if (!pauseMenu.activeInHierarchy) {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            }
        else {
            unPause();
            }
        }

    public void unPause() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        }
    }