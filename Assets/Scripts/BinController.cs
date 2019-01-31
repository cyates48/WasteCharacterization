using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinController : MonoBehaviour {

	public GameObject gameController;
    public bool gainPoints;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Call the check function when item enters a bin
	void OnTriggerEnter(Collider item) {
        if (gameObject.tag == "landfill" && item.tag == "landfill")
            gainPoints = true;
        else if (gameObject.tag == "recycle" && item.tag == "recycle")
            gainPoints = true;
        else if (gameObject.tag == "compost" && item.tag == "compost")
            gainPoints = true;
        else
            gainPoints = false;

        gameController.GetComponent<GameController>().DistributePoints(gainPoints);
	}
}
