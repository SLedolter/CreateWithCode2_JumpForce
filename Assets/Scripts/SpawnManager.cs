using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
  public GameObject obstaclePrefab;
  private Vector3 spawnPosition = new Vector3(30, 0, 0);

  private float spawnDelay = 2;
  private float spawnInterval = 1;

  // Start is called before the first frame update
  void Start() {
    InvokeRepeating("SpawnObstacle", spawnDelay, spawnInterval);
  }

  // Update is called once per frame
  void Update() {

  }

  void SpawnObstacle() {
    Instantiate(obstaclePrefab, spawnPosition, obstaclePrefab.transform.rotation);
  }
}
