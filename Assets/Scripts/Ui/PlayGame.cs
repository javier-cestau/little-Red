using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    AudioSource playButtonAudio;
    bool started = false;

    void Awake() {
        playButtonAudio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    public void StartGame () {
        if(!started) {
            started = true;
            StartCoroutine(WaitForSound());
        }
    }

    public void QuitGameButton (){
        QuitGame();
    }
     public static void QuitGame () {
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    IEnumerator WaitForSound()
    {
        playButtonAudio.Play();
        yield return new WaitForSeconds(playButtonAudio.clip.length - .5f);
        SceneManager.LoadScene("1-Level");
    }
}
