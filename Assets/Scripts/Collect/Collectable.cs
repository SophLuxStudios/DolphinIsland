using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectable : MonoBehaviour
{
    //Serialized properties
    [SerializeField] private float turnSpeed = 90f;

    //Used classes
    private ShipStorage shipStorage;
    private ScoreManager scoreManager;

    private bool isEndlessMode;

    void Start()
    {
        shipStorage = GameObject.FindObjectOfType<ShipStorage>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("EndlessRun"))
        {    
            isEndlessMode = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if collided object is the ship
        if(other.gameObject.name == "Ship")
        {
            //in endless mode
            if(isEndlessMode)
            {
                //Add score
                scoreManager.IncrementScore();
            }
            else//in story mode
            {
                //check if ship capacity is full
                if(PlayerPrefs.GetInt("CollectedWood") + PlayerPrefs.GetInt("CollectedIron") +
                scoreManager.collectedWood + scoreManager.collectedIron <
                shipStorage.shipStorageCapacity)
                {
                    CheckCollectQuest(gameObject.tag);

                    switch (gameObject.tag)
                    {
                        case "CollectableWood":
                            scoreManager.collectedWood++;
                            scoreManager.DisplayCollectableWood();
                            break;
                        case "CollectableIron":
                            scoreManager.collectedIron++;
                            scoreManager.DisplayCollectableIron();
                            break;
                    }
                }
            }

            FindObjectOfType<AudioManager>().Play("Collect");

            //Destroy this collectable //Destroy(gameObject);
            //Deactive because it is child of another so will be destroyed later
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(isEndlessMode)
        {
            //Rotate coin collectables' on the y axis
            transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
        }
        else if(!isEndlessMode)
        {
            //Rotate wood and iron collectables' on the z axis
            transform.Rotate(0, 0, turnSpeed * Time.deltaTime);

            //Deactivate if full capacity or at dock position in story mode
            if(PlayerPrefs.GetInt("ShipCapacityReached") == 1 || transform.position.z > 7500)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void CheckCollectQuest(string questType)
    {
        if(PlayerPrefs.GetInt("CollectQuestActive", 0) == 1)
        {
            if(PlayerPrefs.GetString("CollectQuestType") == questType)
            {                    
                PlayerPrefs.SetInt("CollectQuestQuantity", (PlayerPrefs.GetInt("CollectQuestQuantity") - 1));                    

                if(PlayerPrefs.GetInt("CollectQuestQuantity") == 0)                    
                {
                    PlayerPrefs.SetInt("CollectQuestActive", 0);

                    PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + PlayerPrefs.GetInt("CollectQuestReward"));
                }                    
            }                    
        }                  
    }
}