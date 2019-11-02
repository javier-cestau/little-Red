using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    const string STATE_IS_WALKING = "isWalking", STATE_IS_ON_THE_GROUND = "isOnTheGround", STATE_IS_DEAD = "isDead";

    Animator playerAnimator;

    SpriteRenderer playerSpriteRenderer;

    PlayerController playerController;

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Horizontal")) {
            playerAnimator.SetBool(STATE_IS_WALKING, true);
            playerSpriteRenderer.flipX = (Input.GetAxisRaw("Horizontal") < 0);
        } else {
            playerAnimator.SetBool(STATE_IS_WALKING, false);
        }

        playerAnimator.SetBool(STATE_IS_ON_THE_GROUND, playerController.IsOnTheGround());
    }

    public void DieAnimation() {
        playerSpriteRenderer.flipX = false;
        playerAnimator.SetBool(STATE_IS_DEAD, true);
    }


    public IEnumerator HitAnimation()
    {

        transform.parent.gameObject.layer = LayerMask.NameToLayer("PlayerHit");
        float minOpacity = 0.2f, maxOpacity = 1f;
        float timeNow = Time.time;
        while((timeNow + 4) > Time.time) {
            playerSpriteRenderer.color = new Color(1f,1f,1f,minOpacity);
            yield return new WaitForSeconds(.2f);
            playerSpriteRenderer.color = new Color(1f,1f,1f,maxOpacity);
            yield return new WaitForSeconds(.2f);
        }
        transform.parent.gameObject.layer = LayerMask.NameToLayer("Default");
    }


}
