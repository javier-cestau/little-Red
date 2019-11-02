using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    const string STATE_IS_DEAD = "isDead";
    Rigidbody2D enemyRigidbody2d;
    SpriteRenderer enemySpriteRenderer;
    Animator enemyAnimator;
    ParticleSystem deadParticle;
    CircleCollider2D headCollider;
    AudioSource audio;

    public float enemyVelocity = 2;
    bool isGoingRight = true;

    bool isDead = false;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        enemyRigidbody2d = GetComponent<Rigidbody2D>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        enemyAnimator = GetComponent<Animator>();
        headCollider = GetComponent<CircleCollider2D>();
        deadParticle = GetComponentInChildren<ParticleSystem>();
        audio = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        enemyRigidbody2d.velocity = new Vector2(enemyVelocity * EnemyDirection(), enemyRigidbody2d.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player") && headCollider == other.otherCollider) {
            enemyAnimator.SetBool(STATE_IS_DEAD, true);
            isDead = true; // avoid hit player during death animation
            StartCoroutine(Kill());
        } else if(other.gameObject.CompareTag("Player") && !isDead) {
            other.gameObject.GetComponent<PlayerController>().ReceiveDamage();
        }

    }

    int EnemyDirection () {
        return isGoingRight ? 1 : -1;
    }

    public void ChangeDirection() {
        isGoingRight = !isGoingRight;
        enemySpriteRenderer.flipX = !isGoingRight;
    }

    IEnumerator  Kill() {
        audio.Play();
        deadParticle.Play();
        enemyVelocity = 0;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
