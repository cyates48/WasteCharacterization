using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour {

    public float speed = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.position += (transform.right * Time.deltaTime * 0.4f);
        rigidbody.MovePosition(rigidbody.position - (transform.right * Time.deltaTime *0.4f));
    }
}
