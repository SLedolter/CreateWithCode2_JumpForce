using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  private Animator playerAnim;
  public ParticleSystem explosionParticle;
  public ParticleSystem dirtParticle;
  public AudioClip jumpSound;
  public AudioClip crashSounnd;
  public float jumpForce;
  public float gravityModifier;
  public bool isOnGround = true;
  public bool doubleJump = true;

  private AudioSource playerAudio;
  private GameManager gameManagerScript;
  private Rigidbody playerRb;

  // Start is called before the first frame update
  void Start() {
    playerRb = GetComponent<Rigidbody>();
    playerAnim = GetComponent<Animator>();
    playerAudio = GetComponent<AudioSource>();
    Physics.gravity *= gravityModifier;
    gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
  }

  // Update is called once per frame
  void Update() {
    if(Input.GetKeyDown(KeyCode.Space) && (isOnGround || doubleJump) & !gameManagerScript.gameOver){
      DoPlayerJump();
    }

    if(Input.GetKeyDown(KeyCode.E)) {
      gameManagerScript.obstacleSpeed = 40;
      gameManagerScript.scoreIncrease = 10;
    }

    if(Input.GetKeyUp(KeyCode.E)) {
      gameManagerScript.obstacleSpeed = 20;
      gameManagerScript.scoreIncrease = 5;
    }
  }

  private void DoPlayerJump() {
    playerRb.velocity = Vector3.zero;
    playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    if(!isOnGround) {
      doubleJump = false;
    }
    isOnGround = false;
    playerAudio.PlayOneShot(jumpSound, 1.0f);
    playerAnim.SetTrigger("Jump_trig");
    dirtParticle.Stop();
  }

  private void OnCollisionEnter(Collision collision) {
    Debug.Log($"Collision:{collision.gameObject.name}");
    if(collision.gameObject.CompareTag("Ground")){
      isOnGround = true;
      doubleJump = true;
      dirtParticle.Play();
    } else if(collision.gameObject.CompareTag("Obstacle")) {
      gameManagerScript.gameOver = true;
      explosionParticle.Play();
      playerAnim.SetBool("Death_b", true);
      playerAnim.SetInteger("DeathType_int", 1);
      playerAudio.PlayOneShot(crashSounnd, 1.0f);
      dirtParticle.Stop();
    }
  }
}
