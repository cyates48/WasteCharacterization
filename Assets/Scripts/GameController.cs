using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int totalPoints;
    public GameObject score;
    private Text text_score;

    // Use this for initialization
    void Start () {
        totalPoints = 0;
        text_score = score.GetComponent<Text>();
    }

    void Update () {
        text_score.text = totalPoints.ToString();
    }

    public void DistributePoints(bool gainPoints) {
        if (gainPoints)
            totalPoints += 5;
        else
            totalPoints -= 5;
    }
}
