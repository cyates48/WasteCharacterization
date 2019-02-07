using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class DescriptionEnabler : MonoBehaviour {

    public DistanceGrabbable item;
    Transform m_centerEyeAnchor;

	// Use this for initialization
	void Start () { 
        m_centerEyeAnchor = GameObject.Find("CenterEyeAnchor").transform;
    }
	
	// Update is called once per frame
	void Update () {

        Canvas canvas = item.GetComponentInChildren<Canvas>();

        if (item.isGrabbed)
        {
            canvas.enabled = true;
        }
        else
            canvas.enabled = false;

        RectTransform rtransform = canvas.GetComponent<RectTransform>();
        rtransform.LookAt(m_centerEyeAnchor);
        rtransform.rotation = Quaternion.LookRotation(rtransform.position - m_centerEyeAnchor.position);
    }
}
