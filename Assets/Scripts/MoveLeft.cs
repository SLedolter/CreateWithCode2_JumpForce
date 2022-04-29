using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour {
  private GameManager gameManagerScript;
  private float leftBound = -15;

  // Start is called before the first frame update
  void Start() {
    gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
  }

  // Update is called once per frame
  void Update() {
    if(gameManagerScript.gameOver != true) {
      transform.Translate(Vector3.left * Time.deltaTime * gameManagerScript.obstacleSpeed);
    }

    if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle")) {
      Destroy(gameObject);
    }
  }
}
