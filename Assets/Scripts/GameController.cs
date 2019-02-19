using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int totalPoints;
    public GameObject score;
    public float total_time;
    private float time_left;
    public Spawner spawner;
    public ConveyorBelt conveyorBelt;
    private Text text_score;
    public GameObject completeText;
    public GameObject timeObj;
    private Text text_time;
    public GameObject startBoard, gameBoard;

    // streak
    private int streak;
    private int streakBonus;
    public Text text_streak;
    public Text text_streak_bonus;

    // Sounds
    public AudioSource audiosrc;
    public AudioClip timer_sfx, right_sfx, wrong_sfx, done_sfx;
    
    // Levels
    public struct Level {
        public float spawn_interval;
        public float conveyor_speed;
        public float conv_tex_speed;

        public Level(float x1, float x2, float x3) {
            spawn_interval = x1;
            conveyor_speed = x2;
            conv_tex_speed = x3;
        }
    }

    public GameObject pointer;
    public GameObject replayButton;

    enum GameState {
        Waiting,
    	InProgress,
    	Ending,
    	Ended
    }
    GameState gameState;

    // Use this for initialization
    void Start () {
        gameState = GameState.Waiting;
        text_streak = streakNumObj.GetComponent<Text>();
        text_time = timeObj.GetComponent<Text>();
        text_score = score.GetComponent<Text>();
        text_streak_bonus = streakBonusObj.GetComponent<Text>();
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
        time_left = total_time;
        totalPoints = 0;     
        text_score.text = "0";
        UpdateTimerText();
        streak = 0;
        streakBonus = 0;
        text_streak.text = "0";
        text_streak_bonus.text = "";
        gameBoard.SetActive(true);
        startBoard.SetActive(false);
        gameState = GameState.InProgress;
        spawner.StartSpawning();
        conveyorBelt.StartBelt();
        pointer.SetActive(false);
        completeText.SetActive(false);
        replayButton.SetActive(false);
    }

    public void DistributePoints(bool gainPoints) {
        if (gainPoints) {
            totalPoints += 5;
            streak += 1;
            addStreakBonus();
            audiosrc.PlayOneShot(right_sfx);
        }
        else {
            totalPoints -= 5;
            streak = 0;
            streakBonus = 0;
            text_streak_bonus.text = " ";
            audiosrc.PlayOneShot(wrong_sfx);
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
        audiosrc.PlayOneShot(timer_sfx);
    }

    void EndLevel() {
    	gameState = GameState.Ended;
    	completeText.SetActive(true);
        replayButton.SetActive(true);
        conveyorBelt.StopBelt();
        pointer.SetActive(true);

        if (current_level + 1 < levels.Length) {
            nextButton.SetActive(true);
        }

        audiosrc.PlayOneShot(done_sfx);
    }

    public void NextLevel() {
        current_level += 1;
        text_level.text = string.Format("Level {0}", current_level + 1);
        StartGame(current_level);
    }

    public void ReplayLevel() {
        StartGame(current_level);
    }
}
