using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    //make class public
    public static ScoreManager scoreManager;

    //used classes
    private GameObject ship;
    private ShipMovement shipMovement;
    private ShipStorage shipStorage;
    private TerrainTile terrainTile;

    //private properties
    private int score;
    public int collectedWood = 0;
    public int collectedIron = 0;

    //serialized UIs
    [SerializeField] private TextMeshProUGUI showScoreMainText;
    [SerializeField] private TextMeshProUGUI showHighScorePauseText;
    [SerializeField] private TextMeshProUGUI showScoreDeadText;
    [SerializeField] private TextMeshProUGUI showHighScoreDeadText;
    [SerializeField] private TextMeshProUGUI xWoodText;
    [SerializeField] private TextMeshProUGUI xIronText;

    //Mode booleans
    bool inEndlessMode;
    //bool inStoryMode;

    private void Awake()
    {
        scoreManager = this;

        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("EndlessRun"))
        {
            inEndlessMode = true;
        }
        else if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("StoryMode"))
        {
            inEndlessMode = false;
        }
    }

    private void Start()
    {
        ship = GameObject.FindGameObjectWithTag("Ship");
        shipMovement = ship.GetComponent<ShipMovement>();
        shipStorage = GameObject.FindObjectOfType<ShipStorage>();
        terrainTile = GameObject.FindObjectOfType<TerrainTile>();

        if(inEndlessMode)
        {
            DisplayHighScore();
        }
        else
        {
            DisplayCollectableWood();
            DisplayCollectableIron();
        }
    }

    private void DisplayHighScore()
    {
        //Display HS on pause panel
        showHighScorePauseText.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();

        //Display HS on dead panel
        showHighScoreDeadText.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
    }

    private void IsHighScore()
    {
        if(PlayerPrefs.GetInt("HighScore") < score)
        {
            SetHighScore();
        }   
    }

    public void IncrementScore()
    {
        //Set score
        score++;
        
        //call display before setting new HS
        DisplayScore();

        //HS check
        IsHighScore();
    }

    private void DisplayScore()
    {      
        //Display score on main panel
        if(PlayerPrefs.GetInt("HighScore") < score)
        {
            showScoreMainText.text = " " + score.ToString() + " New Best";
        }
        else
        {
            showScoreMainText.text = " " + score.ToString();
        }

        //Display score on dead panel
        showScoreDeadText.text = "Score: " + score.ToString();

        //speed increase depending on score
        shipMovement.shipForwardSpeed += shipMovement.speedIncreasePerPoint;
    }

    public void DisplayCollectableWood()
    {
        xWoodText.text = "X " + (PlayerPrefs.GetInt("CollectedWood") + collectedWood).ToString();

        InGameCapacityCheck();
    }

    public void DisplayCollectableIron()
    {
        xIronText.text = "X " + (PlayerPrefs.GetInt("CollectedIron") + collectedIron).ToString();

        InGameCapacityCheck();
    }

    void InGameCapacityCheck()
    {
        if(PlayerPrefs.GetInt("CollectedWood") + PlayerPrefs.GetInt("CollectedIron") +
        collectedWood + collectedIron == shipStorage.shipStorageCapacity)
        {
            PlayerPrefs.SetInt("ShipCapacityReached", 1);
            shipStorage.CapacityReachedSignActivation(true);
            terrainTile.isShipCapacityReached = true;
        }
    }

    public void RegisterCollected()
    {
        PlayerPrefs.SetInt("CollectedWood", (PlayerPrefs.GetInt("CollectedWood") + collectedWood));
        PlayerPrefs.SetInt("CollectedIron", (PlayerPrefs.GetInt("CollectedIron") + collectedIron));

        collectedWood = 0;
        collectedIron = 0;

        Debug.Log("Collected goods are registered.");
    }

    private void SetHighScore()
    {
        //Set high score and display
        PlayerPrefs.SetInt("HighScore", score);

        DisplayHighScore();
    }
}