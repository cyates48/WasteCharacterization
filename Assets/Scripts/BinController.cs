using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class BinController : MonoBehaviour {

	public GameObject gameController;
    Queue<GameObject> containedItems;
    int item_max = 8;

	// Use this for initialization
	void Start () {
		containedItems = new Queue<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if (containedItems.Count > item_max) {
			GameObject item = containedItems.Dequeue();
			Destroy(item);
		}
	}

    // Call the check function when item enters a bin
	void OnTriggerEnter(Collider item) {
        bool gainPoints;
        if (item.name=="DetectGrabRange" || item.name=="GrabVolumeCone" || item.name=="GrabVolumeSmall" || item.name=="GrabVolumeBig" || item.name=="OVRPlayerController")
            return;
        else
            gainPoints = gameObject.tag == item.tag;

        containedItems.Enqueue(item.gameObject);
        gameController.GetComponent<GameController>().DistributePoints(gainPoints);
        item.gameObject.GetComponent<DistanceGrabbable>().enabled = false;
	}
}
