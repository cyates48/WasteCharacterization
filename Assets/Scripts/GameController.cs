using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int totalPoints;
    public GameObject score;
    public float time_left;
    public GameObject spawner;
    public ConveyorBelt conveyorBelt;
    private Text text_score;
    public GameObject completeText;
    public GameObject timeObj;
    private Text text_time;

    enum GameState {
    	InProgress,
    	Ending,
    	Ended
    }
    GameState gameState;

    // Use this for initialization
    void Start () {
        totalPoints = 0;
        text_score = score.GetComponent<Text>();
        text_score.text = "0";
        text_time = timeObj.GetComponent<Text>();
        gameState = GameState.InProgress;
        UpdateTimerText();
    }

    void Update () {
        text_score.text = totalPoints.ToString();
        time_left -= Time.deltaTime;
        UpdateTimerText();
        if (gameState == GameState.InProgress && time_left <= 0) {
        	StopSpawning();
        }
        else if (gameState == GameState.Ending && !conveyorBelt.AreObjectsOnBelt()) {
        	EndLevel();
        }
    }

    void UpdateTimerText() {
        int seconds = (int) time_left;
        text_time.text = string.Format("{0:D2}:{1:D2}", seconds / 60, seconds % 60);
    }

    public void DistributePoints(bool gainPoints) {
        if (gainPoints)
            totalPoints += 5;
        else
            totalPoints -= 5;
    }

    void StopSpawning() {
    	gameState = GameState.Ending;
    	spawner.SetActive(false);
    }

    void EndLevel() {
    	gameState = GameState.Ended;
    	completeText.SetActive(true);
    }
}
