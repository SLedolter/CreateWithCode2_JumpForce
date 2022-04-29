using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
  private int playerScore = 0;
  public int scoreIncrease = 5;
  
  public bool gameOver = false;
  public float obstacleSpeed = 20;

  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {
    if(!gameOver) {
      playerScore += scoreIncrease;
      Debug.Log(playerScore);
    } else {
      Debug.Log($"Game over! Score: {playerScore}");
    }
  }
}
