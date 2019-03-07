using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    // MonoBehaviours
    public ConveyorBelt conveyorBelt;
    public Spawner spawner;
    public AudienceReaction audienceReaction;

    // time
    public float total_time;
    private float time_left;
    
    // Board stuff
    private int totalPoints;
    public GameObject startBoard, gameBoard;
    public GameObject completeText;
    public GameObject replayButton;
    public GameObject nextButton;
    public GameObject pointer;
    public Text text_score;
    public Text text_level;    
    public Text text_time;

    // streak
    private int streak;
    private int streakBonus;
    public Text text_streak;
    public Text text_streak_bonus;

    // Sounds
    public AudioSource audiosrc;
    public AudioClip timer_sfx, right_sfx, wrong_sfx, done_sfx;

    // Extra bins and walls and tuts
    public GameObject EWasteBin, HHWBin;
    public GameObject CloseWall, FarWall;
    public GameObject[] tuts;
    int tut_index;
    
    // Levels
    public struct Level {
        public float spawn_interval;
        public float conveyor_speed;
        public float conv_tex_speed;
        public bool use_extra_bins;
        public bool has_tut;

        public Level(float x1, float x2, float x3, bool x4 = false, bool x5 = false) {
            spawn_interval = x1;
            conveyor_speed = x2;
            conv_tex_speed = x3;
            use_extra_bins = x4;
            has_tut = x5;
        }
    }
    Level[] levels;
    private int current_level;

    // Game state
    public enum GameState {
        Waiting,
        InProgress,
        Ending,
        Ended
    }
    public GameState gameState;

    // Object trackers
    int obj_counter;
    Queue<GameObject> objs_to_delete;
    List<GameObject> scored_objs;

    // Use this for initialization
    void Start () {
        gameState = GameState.Waiting;
        levels = new Level[4];
        levels[0] = new Level(3.0f, 1.0f, 0.0005f);
        levels[1] = new Level(2.5f, 1.5f, 0.0010f);
        levels[2] = new Level(2.5f, 1.5f, 0.0010f, true, true);
        levels[3] = new Level(1.75f, 2.0f, 0.0015f, true);
        current_level = 0;
        text_level.text = "Level 1";
        tut_index = 0;
        obj_counter = 0;
        objs_to_delete = new Queue<GameObject>();
        scored_objs = new List<GameObject>();
    }

    // Called every frame
    void Update () {
        // Set score and streak numbers on backboard
        text_score.text = totalPoints.ToString();
        text_streak.text = streak.ToString();
        
        if (gameState == GameState.InProgress) {
            // Update time
            time_left -= Time.deltaTime;
            UpdateTimerText();
        }
        if (gameState == GameState.InProgress && time_left <= 0) {
            // Time is out so stop spawning
            StopSpawning();
        }
        else if (gameState == GameState.Ending && obj_counter <= 0) {
            // Items are off belt so stop spawning
            EndLevel();
        }

        //getAudienceActions(gameState);
    }

    // Format and update the timer text
    void UpdateTimerText() {
        int seconds = (int) time_left;
        if (seconds < 0) seconds = 0;
        text_time.text = string.Format("{0:D2}:{1:D2}", seconds / 60, seconds % 60);
    }

    // Initialise level variables and start it
    public void StartGame(int level_index) {
        // Reset level parameters
        Level level = levels[level_index];
        time_left = total_time;
        totalPoints = 0;     
        text_score.text = "0";
        UpdateTimerText();
        streak = 0;
        streakBonus = 0;
        text_streak.text = "0";
        text_streak_bonus.text = "";
        gameState = GameState.InProgress;

        // Activate game backboard and deactivate other UI
        gameBoard.SetActive(true);
        startBoard.SetActive(false);
        pointer.SetActive(false);
        completeText.SetActive(false);
        replayButton.SetActive(false);
        nextButton.SetActive(false);

        // Start the conveyor belt and spawner
        spawner.StartSpawning(level.spawn_interval, level.use_extra_bins);
        conveyorBelt.StartBelt(level.conveyor_speed, level.conv_tex_speed);
    }

    // Add/remove points
    public void DistributePoints(bool gainPoints, GameObject obj) {
        if (scored_objs.Contains(obj))
            return;

        if (!(obj.tag == "landfill" || obj.tag == "recycle" || obj.tag == "compost" || obj.tag == "ewaste" || obj.tag == "hazardous"))
            return;

        if (gainPoints) {
            totalPoints += 5;
            streak += 1;
            addStreakBonus();
            audiosrc.PlayOneShot(right_sfx);
            audienceReaction.audienceApplause();
        }
        else {
            totalPoints -= 5;
            streak = 0;
            streakBonus = 0;
            text_streak_bonus.text = " ";
            audiosrc.PlayOneShot(wrong_sfx);
            audienceReaction.audienceBoo();
        }
        obj_counter--;
        objs_to_delete.Enqueue(obj);
        scored_objs.Add(obj);
        Invoke("DeleteObject", 2.0f);
    }

    // Add extra points based on the streak
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

    public void NextLevel(bool ignore_tut = false) {
        current_level += 1;
        text_level.text = string.Format("Level {0}", current_level + 1);

        Level level = levels[current_level]; 

        if (!level.has_tut || ignore_tut) {
            StartGame(current_level);
        } else {
            current_level -= 1;
            gameBoard.SetActive(false);
            tuts[tut_index].SetActive(true);
            tut_index += 1;

            if (level.use_extra_bins) {
                // Enable/disable based on level types
                HHWBin.SetActive(level.use_extra_bins);
                EWasteBin.SetActive(level.use_extra_bins);
                CloseWall.SetActive(!level.use_extra_bins);
                FarWall.SetActive(level.use_extra_bins);
            }
        }
    }

    public void ReplayLevel() {
        StartGame(current_level);
    }

    public void OnObjectSpawned() {
        obj_counter++;
    }

    void DeleteObject() {
        Destroy(objs_to_delete.Dequeue());
    }
}
