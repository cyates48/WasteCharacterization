using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceReaction : MonoBehaviour {

    public GameObject[] audience;
    public AudioSource audienceNoise;
    public AudioClip boo1, applause;

    void Start() {

    }

    public void audienceIdle() {
        for (int i = 0; i < audience.Length; i++){
            audience[i].GetComponent<Animation>().Play("idle");
        }
    }
	
	// Applaud when player puts item correctly
	public void audienceApplause() {
        for (int i = 0; i < audience.Length; i++)
        {
            audience[i].GetComponent<Animation>().Play("applause");
            audienceNoise.PlayOneShot(applause);
        }
    }

    // Boo when player puts item incorrectly
    public void audienceBoo() {
        for (int i = 0; i < audience.Length; i++)
        {
            audience[i].GetComponent<Animation>().Play("celebration");
            audienceNoise.PlayOneShot(boo1);

        }
    }
}
