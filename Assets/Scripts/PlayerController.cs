using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Pause
public class PlayerController : MonoBehaviour
{
    const string STATE_IS_WALKING = "isWalking", STATE_IS_ON_THE_GROUND = "isOnTheGround";
    Rigidbody2D playerRigidbody2d;
    public float walkingSpeed = 1f;
    public float jumpSpeed = 4f;
    public LayerMask groundMask;

    ParticleSystem grassJumpParticle;
    bool didJump = false;
    Text DiamondsAmountText;

    public AudioClip jumpAudio, hitAudio;

    PlayerHealth playerHealthController;

    public AudioSource footstep;
    static int diamonds = 0;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        playerRigidbody2d = GetComponent<Rigidbody2D>();
        grassJumpParticle = GetComponentInChildren<ParticleSystem>();
        DiamondsAmountText = GameObject.Find("DiamondAmount").GetComponent<Text>();
        DiamondsAmountText.text = diamonds.ToString();
        playerHealthController = GetComponentInChildren<PlayerHealth>();
        AudioSource backgroundMusic = BackgroundMusic.Instance.gameObject.GetComponent<AudioSource>();
        if(!backgroundMusic.isPlaying){
            backgroundMusic.Play();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Horizontal")) {
            playerRigidbody2d.velocity = new Vector2(walkingSpeed * Input.GetAxisRaw("Horizontal"), playerRigidbody2d.velocity.y);
            if(!footstep.isPlaying) {
                footstep.Play();
            } else if (!IsOnTheGround()) {
                footstep.Stop();
            }
        } else {
            footstep.Stop();
            playerRigidbody2d.velocity = new Vector2(0, playerRigidbody2d.velocity.y);
        }

        if(Input.GetButtonDown("Jump") && IsOnTheGround()) {
            didJump = true;
            playSound(jumpAudio);
            playerRigidbody2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }

        Debug.DrawRay(new Vector2(this.transform.position.x,this.transform.position.y + 1), Vector2.down * 1.15f, Color.red);
        Debug.DrawRay(new Vector2(this.transform.position.x + .4f,this.transform.position.y + 1), Vector2.down * 1.15f, Color.red);
        Debug.DrawRay(new Vector2(this.transform.position.x - .4f,this.transform.position.y + 1), Vector2.down * 1.15f, Color.red);
    }

    public bool IsOnTheGround() {
        return (Physics2D.Raycast(new Vector2(this.transform.position.x,this.transform.position.y + 1), Vector2.down, 1.15f, groundMask) ||
            Physics2D.Raycast(new Vector2(this.transform.position.x + .4f,this.transform.position.y + 1), Vector2.down, 1.15f, groundMask) ||
            Physics2D.Raycast(new Vector2(this.transform.position.x - .4f,this.transform.position.y + 1), Vector2.down, 1.15f, groundMask));

    }

    public void IncreaseDiamonds(int value) {
        diamonds += value;
        DiamondsAmountText.text = diamonds.ToString();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Ground" && didJump) {
            grassJumpParticle.Play();
        }
    }

     void playSound(AudioClip clip){
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = clip;
        audio.Play();
    }

    public void ReceiveDamage() {
        playerHealthController.SubstractHealth();
        playSound(hitAudio);
    }

    public void RestartData(){
        diamonds = 0;
    }

}
