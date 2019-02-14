using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int totalPoints;
    public GameObject score;
    public float time_left;
    public Spawner spawner;
    public ConveyorBelt conveyorBelt;
    private Text text_score;
    public GameObject completeText;
    public GameObject timeObj;
    private Text text_time;
    public GameObject startBoard, gameBoard;

    // streak
    public int streak;
    public int streakBonus;
    public GameObject streakNumObj;
    public GameObject streakBonusObj;
    private Text text_streak;
    private Text text_streak_bonus;


    enum GameState {
        Waiting,
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
        gameState = GameState.Waiting;
        UpdateTimerText();

        streak = 0;
        streakBonus = 0;
        text_streak = streakNumObj.GetComponent<Text>();
        text_streak.text = "0";
        text_streak_bonus = streakBonusObj.GetComponent<Text>();
        text_streak_bonus.text = "";

    }

    void Update () {
        text_score.text = totalPoints.ToString();
        text_streak.text = streak.ToString();
        
        if (gameState == GameState.InProgress) {
            time_left -= Time.deltaTime;
            UpdateTimerText();
        }
        if (gameState == GameState.InProgress && time_left <= 0) {
        	StopSpawning();
        }
        else if (gameState == GameState.Ending && !conveyorBelt.AreObjectsOnBelt()) {
        	EndLevel();
        }
    }

    void UpdateTimerText() {
        int seconds = (int) time_left;
        if (seconds < 0) seconds = 0;
        text_time.text = string.Format("{0:D2}:{1:D2}", seconds / 60, seconds % 60);
    }

    public void StartGame() {
        gameBoard.SetActive(true);
        startBoard.SetActive(false);
        gameState = GameState.InProgress;
        spawner.StartSpawning();
        conveyorBelt.StartBelt();
    }

    public void DistributePoints(bool gainPoints) {
        if (gainPoints) {
            totalPoints += 5;
            streak += 1;
            addStreakBonus();
        }
        else {
            totalPoints -= 5;
            streak = 0;
            streakBonus = 0;
            text_streak_bonus.text = " ";
        }
    }

    void addStreakBonus() {
        if (streak >= 30)
            streakBonus = 15;
        else if (streak >= 20)
            streakBonus = 10;
        else if (streak >= 10)
            streakBonus = 5;

        if (streakBonus > 4)
            text_streak_bonus.text = "+" + streakBonus.ToString();
        totalPoints += streakBonus;
    }

    void StopSpawning() {
    	gameState = GameState.Ending;
    	spawner.StopSpawning();
    }

    void EndLevel() {
    	gameState = GameState.Ended;
    	completeText.SetActive(true);
        conveyorBelt.StopBelt();
    }
}
