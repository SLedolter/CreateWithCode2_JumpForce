using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  private Rigidbody playerRb;
  private Animator playerAnim;
  public ParticleSystem explosionParticle;
  public ParticleSystem dirtParticle;
  public AudioClip jumpSound;
  public AudioClip crashSounnd;
  private AudioSource playerAudio;
  public float jumpForce;
  public float gravityModifier;
  public bool isOnGround = true;
  public bool gameOver = false;
  public bool doubleJump = true;

  // Start is called before the first frame update
  void Start() {
    playerRb = GetComponent<Rigidbody>();
    playerAnim = GetComponent<Animator>();
    playerAudio = GetComponent<AudioSource>();
    Physics.gravity *= gravityModifier;
  }

  // Update is called once per frame
  void Update() {
    if(Input.GetKeyDown(KeyCode.Space) && (isOnGround || doubleJump) & !gameOver){
      playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
      if(!isOnGround) {
        doubleJump = false;  
      }
      isOnGround = false;
      playerAudio.PlayOneShot(jumpSound, 1.0f);
      playerAnim.SetTrigger("Jump_trig");
      dirtParticle.Stop();
    }
  }

  private void OnCollisionEnter(Collision collision) {
    Debug.Log($"Collision:{collision.gameObject.name}");
    if(collision.gameObject.CompareTag("Ground")){
      isOnGround = true;
      doubleJump = true;
      dirtParticle.Play();
    } else if(collision.gameObject.CompareTag("Obstacle")) {
      gameOver = true;
      Debug.Log("Game over!");
      explosionParticle.Play();
      playerAnim.SetBool("Death_b", true);
      playerAnim.SetInteger("DeathType_int", 1);
      playerAudio.PlayOneShot(crashSounnd, 1.0f);
      dirtParticle.Stop();
    }
  }
}
