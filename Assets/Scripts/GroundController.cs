using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour {

	public GameObject gameController;

    // Call the check function when item hits the ground
	void OnCollisionEnter(Collision item) {
        if (item.gameObject.tag == "landfill" || item.gameObject.tag == "recycle" || item.gameObject.tag == "compost") {
        	gameController.GetComponent<GameController>().DistributePoints(false);
        	Destroy(item.gameObject);
        }    
	}
}
