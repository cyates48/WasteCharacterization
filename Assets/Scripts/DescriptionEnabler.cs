using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class DescriptionEnabler : MonoBehaviour {

    public DistanceGrabbable item;

	// Use this for initialization
	void Start () { 
        
    }
	
	// Update is called once per frame
	void Update () {

        GameObject canvas = item.transform.GetChild(0).gameObject;

        if (item.isGrabbed)
        {
            canvas.GetComponent<Canvas>().enabled = true;
        }
        else
            canvas.GetComponent<Canvas>().enabled = false;
    }
}
