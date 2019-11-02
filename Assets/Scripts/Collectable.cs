using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType {
    diamond
}
public class Collectable : MonoBehaviour
{
    public CollectableType type = CollectableType.diamond;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;

    AudioSource audio;

    ParticleSystem collectableParticle;


    public int value = 1;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        collectableParticle = GetComponentInChildren<ParticleSystem>();
        audio = GetComponent<AudioSource>();
    }

    void Start() {
        Show();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Collect(other.gameObject.GetComponent<PlayerController>());
        }
    }

    void Show() {
         spriteRenderer.enabled = true;
         boxCollider2D.enabled = true;
     }

     void Hide() {
         spriteRenderer.enabled = false;
         boxCollider2D.enabled = false;
         collectableParticle.Play();
     }

    void Collect(PlayerController playerController) {
        Hide();
        audio.Play();

        switch (type)
        {
            case CollectableType.diamond:
                playerController.IncreaseDiamonds(value);
                break;
        }
     }
}
