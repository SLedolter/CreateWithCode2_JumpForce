using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
  private int playerScore = 0;
  public int scoreIncrease = 5;

  public bool isIntroPlayed = false;
  public bool startSpawning = false;
  public bool isPlayerRunning = false;
  public bool gameOver = false;
  public float obstacleSpeed = 20;

  private PlayerController playerController;
  private SpawnManager spawnManagerScript;

  private bool isGameMechanicStarted = false;

  // Start is called before the first frame update
  void Start() {
    spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    playerController = GameObject.Find("Player").GetComponent<PlayerController>();
  }

  // Update is called once per frame
  void Update() {
    if(gameOver) {
      Debug.Log($"Game over! Score: {playerScore}");
      return;
    }

    if(isGameMechanicStarted) {
      playerScore += scoreIncrease;
      Debug.Log(playerScore);
    }

    if(Time.time <= 5 && !isIntroPlayed) {
      PlayIntro();
    }

    if(Time.time > 5 && !isGameMechanicStarted) {
      startSpawning = true;
      isPlayerRunning = true;
      isGameMechanicStarted = true;

      StartRunning();
    }
  }

  private void PlayIntro() {
    playerController.StartWalking();
    playerController.GoTo(0);
    isIntroPlayed = true;
  }

  private void StartRunning() {
    GameObject.Find("Background").AddComponent<MoveLeft>(); 
    var temp = spawnManagerScript.obstaclePrefabs.Length;
    foreach(GameObject obstacle in spawnManagerScript.obstaclePrefabs) {
      obstacle.GetComponent<MoveLeft>().enabled = true;
    }
    playerController.StartRunning();
  }
}
