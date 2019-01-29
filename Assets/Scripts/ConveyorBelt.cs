﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour {

    public float speed = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    void onCollisionStay(Collision collision) {
        float beltVelocity = speed * Time.deltaTime;
        collision.gameObject.GetComponent<Rigidbody>().velocity = beltVelocity * transform.forward;
        Debug.Log("weee");
    }
}
