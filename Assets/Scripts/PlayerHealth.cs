using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    const int MAX_HEALTH = 3;
    static int health = MAX_HEALTH;
    public Image[] hearts;
    PlayerAnimation playerAnimation;
    void Awake()
    {
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
    }

    void Start() {
        EmptyHeart();
    }
     public void SubstractHealth() {
        health--;
        if(health <= 0) {
            GameOver();
            return;
        }

        EmptyHeart();
        StartCoroutine(playerAnimation.HitAnimation());
    }

    public void GameOver() {
        playerAnimation.DieAnimation();
        GetComponent<PlayerController>().RestartData();
        RestartData();
        BackgroundMusic.Instance.gameObject.GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("GameOver");
    }


    public void AddHealth() {
        if(health < MAX_HEALTH) {
            health++;
            FillHeart();
        }
    }
    void FillHeart() {
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i <= health - 1) {
                hearts[i].gameObject.SetActive(true);
            }
        }
    }

    void EmptyHeart() {
        for (int i = 0; i < hearts.Length; i++)
        {
            if(health - 1 < i) {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }

    public void RestartData(){
        health = MAX_HEALTH;
    }
}
