using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(pauseMenuUI.activeInHierarchy) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume (){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void Pause (){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void QuitGame() {
        PlayGame.QuitGame();
    }
}
