using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinController : MonoBehaviour {

	public GameObject gameController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		gameController.GetComponent<GameController>().OnBinEntered();
	}
}
