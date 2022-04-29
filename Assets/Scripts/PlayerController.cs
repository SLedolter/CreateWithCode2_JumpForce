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

  public float speed = 0;
  public float walkSpeed = 5f;
  public float runSpeed = 10f;

  private AudioSource playerAudio;
  private GameManager gameManagerScript;
  private Rigidbody playerRb;

  private Vector3 target;

  // Start is called before the first frame update
  void Start() {
    playerRb = GetComponent<Rigidbody>();
    playerAnim = GetComponent<Animator>();
    playerAudio = GetComponent<AudioSource>();
    dirtParticle = GameObject.Find("FX_DirtSplatter").GetComponent<ParticleSystem>();
    gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    Physics.gravity *= gravityModifier;
    dirtParticle.Stop();
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

    if(transform.position.x != target.x) {
      transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
      if(transform.position.x > target.x) {
        transform.position = target;
        speed = 0;
        playerAnim.SetFloat("Speed_f", 0);
      }
    }
  }

  public void DoPlayerJump() {
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

  public void StartRunning() {
    playerAnim.Play("Run_Static");
    playerAnim.SetFloat("Speed_f", 0.5f);
    dirtParticle.Play();
  }

  public void StartWalking() {
    playerAnim.Play("Walk_Static");
    playerAnim.SetFloat("Speed_f", 0.5f);
  }

  public void GoTo(float xPos) {
    target = new Vector3(xPos, 0, 0);
    speed = walkSpeed;
  }

  private void OnCollisionEnter(Collision collision) {
    if(collision.gameObject.CompareTag("Ground")){
      isOnGround = true;
      doubleJump = true;;
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
