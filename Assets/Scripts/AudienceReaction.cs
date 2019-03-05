using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceReaction : MonoBehaviour {

    public GameObject[] audience;
    public Animation tempPerson;

    enum GameState
    {
        Waiting,
        InProgress,
        Ending,
        Ended
    }
    GameState gameState;

    enum AudienceState
    {
        Idle,
        Applaud,
        Boo
    }
    AudienceState audienceState;

    // Use this for initialization
    void Start () {
        audienceState = AudienceState.Idle;
	}

    public void getAudienceAction(GameState state) {
        if ((state == GameState.Waiting ||
            state == GameState.Ended)
            && audienceState == AudienceState.Idle) {
            for (int i = 0; i < audience.Length; i++)
            {
                audience[i].GetComponent<Animation>().Play("idle");
            }
        }
    }
	
	// Applaud when player puts item correctly
	public void audienceApplause() {
        for (int i = 0; i < audience.Length; i++)
        {
            audience[i].GetComponent<Animation>().Play("applause");
        }
    }

    // Boo when player puts item incorrectly
    public void audienceBoo() {
        for (int i = 0; i < audience.Length; i++)
        {
            audience[i].GetComponent<Animation>().Play("celebration");
        }
    }
}
