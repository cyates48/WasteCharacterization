using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour {

    public float speed = 1f;
    public GameObject spawner;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnCollisionStay(Collision col) {
		Rigidbody trash_rb = col.gameObject.GetComponent<Rigidbody>();
		Vector3[] lane_locs = spawner.GetComponent<Spawner>().lane_locations;
		trash_rb.velocity = new Vector3(speed, 0f, 0f);

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
}