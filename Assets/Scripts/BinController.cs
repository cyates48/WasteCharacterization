using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class BinController : MonoBehaviour {

	public GameObject gameController;
    int item_max = 8;

	// Use this for initialization
	void Start () {
	}

    // Call the check function when item enters a bin
	void OnTriggerEnter(Collider item) {
        bool gainPoints;
        if (item.name=="DetectGrabRange" || item.name=="GrabVolumeCone" || item.name=="GrabVolumeSmall" || item.name=="GrabVolumeBig" || item.name=="OVRPlayerController")
            return;
        else
            gainPoints = gameObject.tag == item.tag;

        gameController.GetComponent<GameController>().DistributePoints(gainPoints, item.gameObject);
	}
}
