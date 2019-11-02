using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    AudioSource heartSound;
    SpriteRenderer heartSpriteRenderer;
    BoxCollider2D heartBoxCollider2D;

    void Awake() {
        heartSound = GetComponent<AudioSource>();
        heartBoxCollider2D = GetComponent<BoxCollider2D>();
        heartSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            heartSpriteRenderer.enabled = false;
            heartBoxCollider2D.enabled = false;
            other.GetComponent<PlayerHealth>().AddHealth();
            heartSound.Play();
            Destroy(gameObject, heartSound.time + 1);
        }
    }
}
