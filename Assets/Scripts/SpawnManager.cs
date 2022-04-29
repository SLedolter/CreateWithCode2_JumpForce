using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
  public GameObject[] obstaclePrefabs;
  private Vector3 spawnPosition = new Vector3(30, 0, 0);
  private GameManager gameManagerScript;

  private float spawnDelay = 2;
  private float spawnInterval = 1.5f;

  // Start is called before the first frame update
  void Start() {
    InvokeRepeating("SpawnObstacle", spawnDelay, spawnInterval);
    gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
  }

  // Update is called once per frame
  void Update() {

  }

  void SpawnObstacle() {
    if(!gameManagerScript.gameOver && gameManagerScript.startSpawning) {
      int randomObstacleIndex = Random.Range(0, obstaclePrefabs.Length);
      Instantiate(obstaclePrefabs[randomObstacleIndex], spawnPosition, obstaclePrefabs[randomObstacleIndex].transform.rotation);
    }
  }
}
