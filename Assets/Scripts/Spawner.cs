using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private int fallingDepth;

    // Use this for initialization
    void Start () {

        transform.position = spawnPoint;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (transform.position.y <= fallingDepth) { Application.LoadLevel(Application.loadedLevel); }
	}
}