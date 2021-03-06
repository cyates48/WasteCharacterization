﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour {

	private float speed;
    private float texture_speed;
    public GameObject spawner;
    public Material conveyor_mat;

    bool isMoving;

	// Use this for initialization
	void Start () {
		isMoving = false;
	}

	void Update() {
		if (isMoving) {
			Vector2 new_offset = conveyor_mat.mainTextureOffset;
			new_offset.x -= texture_speed;
			conveyor_mat.mainTextureOffset = new_offset;
		}
	}
	
	void OnCollisionStay(Collision col) {
		Rigidbody trash_rb = col.gameObject.GetComponent<Rigidbody>();
		Vector3[] lane_locs = spawner.GetComponent<Spawner>().lane_locations;
		trash_rb.velocity = speed * new Vector3(.5f, 0f, 0f);

		int closestlane = -1;
		float closestdist = 999999;
		for (int i = 0; i < lane_locs.Length; i++) {
			float dist = Mathf.Pow(trash_rb.position.z - lane_locs[i].z, 2);
			if (dist < closestdist) {
				closestdist = dist;
				closestlane = i;
			}
		}

		trash_rb.position = new Vector3(trash_rb.position.x, trash_rb.position.y, lane_locs[closestlane].z);
	}

	void OnCollisionExit(Collision col) {
		Rigidbody rb = col.rigidbody;
		rb.AddTorque(1.0f * Vector3.forward);
	}

	void OnApplicationQuit() {
		conveyor_mat.mainTextureOffset = new Vector2(0.0f, -0.09f);
	}

	public void StartBelt(float spd, float tex) {
		speed = spd;
		texture_speed = tex;
		isMoving = true;
	}

	public void StopBelt() {
		isMoving = false;
	}
}