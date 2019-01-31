using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameController : MonoBehaviour {

    public int totalPoints;

    // Use this for initialization
    void Start () {
        totalPoints = 0;
    }

    public void DistributePoints(bool gainPoints) {
        Debug.Log(totalPoints);
        if (gainPoints)
            totalPoints += 5;
        else
            totalPoints -= 5;
    }
}
