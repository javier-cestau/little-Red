using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{

    public bool finalLevel = false;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            NextScene();
        }
    }

    void NextScene() {
        if(finalLevel) {
            SceneManager.LoadScene("Win");
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
