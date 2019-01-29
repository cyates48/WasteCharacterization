﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnItem : MonoBehaviour {

    public Transform fork;
    Random lane_rnd = new Random();
    Random pref_rnd = new Random();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        spawnItems();
	}

    void spawnItems() {
        int lane = lane_rnd.Next(1, 3);
        int prefab = pref_rnd.Next(1, 20);

        if (lane == 1)
            Instantiate(fork, new Vector3(1.97f, 2.348f, -1.338f), Quaternion.identity);
        else if (lane == 2)
            Instantiate(fork, new Vector3(1.97f, 2.348f, -1.047f), Quaternion.identity);
        else
            Instantiate(fork, new Vector3(1.97f, 2.348f, -0.78f), Quaternion.identity);
    }
}
