using UnityEngine;
using UnityEngine.SceneManagement;

public class TerrainTile : MonoBehaviour
{
    //used classes
    private TerrainSpawner terrainSpawner;
    private ShipHealth shipHealth;
    //private ObjectPooler objectPooler;

    //Serialized prefabs
    [SerializeField] private GameObject collectableWoodPrefab;
    [SerializeField] private GameObject collectableIronPrefab;
    [SerializeField] private GameObject collectableCoinPrefab;
    [SerializeField] private GameObject[] rockObstaclePrefabs;
    [SerializeField] private GameObject[] decorationPrefabs;

    //used variables
    public bool isShipCapacityReached;
    private bool isEndlessMode;
    private int obstacleSpawnIndex;
    private int[] spawnIndexes = new int[5];
    private int zeroCount = 0; 
    private int oneCount = 0; 
    private int twoCount = 0;
    private bool ignoreTrigger;

    private void Awake()
    {
        if(PlayerPrefs.GetInt("ShipCapacityReached") == 1)
        {
            isShipCapacityReached = true;
        }
        /*else
        {
            isShipCapacityReached = false;
        }*/

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("EndlessRun"))
        {    
            isEndlessMode = true;
        }
    }

    private void Start()
    {
        SpawnItem();

        //objectPooler = ObjectPooler.Instance;

        terrainSpawner = GameObject.FindObjectOfType<TerrainSpawner>();
        shipHealth = ShipHealth.shipHealth;
    }

    private void OnTriggerExit(Collider ship)
    {
        if(!isEndlessMode && transform.position.z < 6600)
        {
            //because ship and boat both have triggers ignore once then execute once
            if(!ignoreTrigger)
            {
                terrainSpawner.SpawnTile();
            }
            ignoreTrigger = !ignoreTrigger;

            if(transform.position.z < 7500)
            {
                if(shipHealth.alive)
                {
                    if(!shipHealth.shipDiedOnce)
                    {
                        Destroy(gameObject, 2);
                    }
                    else
                    {
                        Destroy(gameObject, 10);
                    }
                }
            }
        }
        else if(isEndlessMode)
        {
            //because ship and boat both have triggers ignore once then execute once
            if(!ignoreTrigger)
            {
                terrainSpawner.SpawnTile();
            }
            ignoreTrigger = !ignoreTrigger;
            
            if(shipHealth.alive)
            {
                if(!shipHealth.shipDiedOnce)
                {
                    Destroy(gameObject, 2);
                }
                else
                {
                    Destroy(gameObject, 10);
                }
            }
        }
    }

    private void SpawnItem()
    {

        //Child indexes of ItemSpawners are 2 through 6
        for(int childIndex = 2; childIndex < 7; childIndex++)
        {
            if(transform.position.z == 0 && childIndex == 2)
            {
                childIndex++;
            }

            bool doAgain;
            do//do not let more than two same point for each terrain
            {
                obstacleSpawnIndex = Random.Range(0, 3);

                doAgain = false;
                switch(obstacleSpawnIndex)
                {
                    case 0:
                        if(zeroCount < 2)
                        {
                            zeroCount++;
                            spawnIndexes[childIndex-2] = obstacleSpawnIndex;
                        }
                        else
                        {
                            doAgain = true;
                        }
                        break;
                    case 1:
                        if(oneCount < 2)
                        {
                            oneCount++;
                            spawnIndexes[childIndex-2] = obstacleSpawnIndex;
                        }
                        else
                        {
                            doAgain = true;
                        }
                        break;
                    case 2:
                        if(twoCount < 2)
                        {
                            twoCount++;
                            spawnIndexes[childIndex-2] = obstacleSpawnIndex;
                        }
                        else
                        {
                            doAgain = true;
                        }
                        break;
                }
            }while(doAgain);
            
            if(isEndlessMode)
            {
                Transform obstacleSpawnPoint = transform.GetChild(childIndex).GetChild(spawnIndexes[childIndex-2]).transform;
                SpawnObstacle(obstacleSpawnPoint);
            }
            else if(transform.position.z < 7450)
            {
                Transform obstacleSpawnPoint = transform.GetChild(childIndex).GetChild(spawnIndexes[childIndex-2]).transform;
                SpawnObstacle(obstacleSpawnPoint);
            }

            if(!isShipCapacityReached || isEndlessMode)
            {
                // randomly choose whether or not to spawn a collectable
                int doWeGiveReward = Random.Range(0, 2);
                if(doWeGiveReward == 0)
                {
                    int collectableSpawnIndex;
                    //choose a random point to spawn the collectable.
                    do
                    {
                        collectableSpawnIndex = Random.Range(0, 3);

                    //if it is the same point as obstacleSpawn do again.
                    } while (collectableSpawnIndex == obstacleSpawnIndex);

                    //spawn the collectable at chosen position
                    Transform collectableSpawnPoint = transform.GetChild(childIndex).GetChild(collectableSpawnIndex);
                    SpawnCollectables(collectableSpawnPoint);
                }
            }
        }
    }

    public void SpawnObstacle(Transform spawnAt)
    {
        int obstacleIndex = Random.Range(0, rockObstaclePrefabs.Length);
        Instantiate(rockObstaclePrefabs[obstacleIndex], spawnAt.position, Quaternion.identity, transform);
    }

    public void SpawnCollectables(Transform spawnAt)
    {
        Vector3 spawnCollectableAt = spawnAt.position + new Vector3(0, 1, 0);
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("EndlessRun"))
        {
            Instantiate(collectableCoinPrefab, spawnCollectableAt, Quaternion.identity, transform);
        }
        else
        {
            //choose a random collectable to be spawned
            int collectableDecider = Random.Range(0, 4);
            if(collectableDecider == 0)
            {
                //objectPooler.SpawnFromPool("CollectableIron", spawnAt.position, Quaternion.identity);
                Instantiate(collectableIronPrefab, spawnCollectableAt, Quaternion.identity, transform);
            }
            else if(transform.position.z < 7400)
            {
                //objectPooler.SpawnFromPool("CollectableWood", spawnAt.position, Quaternion.identity);
                Instantiate(collectableWoodPrefab, spawnCollectableAt, Quaternion.Euler(0, 0, 25), transform);
            }
        }
    }

    public void SpawnDecoration(Transform spawnAt, int isRight)
    {
        //choose a random object to be spawned
        int decorationIndex = Random.Range(0, decorationPrefabs.Length);

        //Vector3 lookAtPosition = transform.GetChild(2).transform.position;

        Quaternion decorationRotation = Quaternion.Euler(0,0,0);

        switch (decorationIndex)
        {
            case 0: //skull
                decorationRotation = Quaternion.Euler(-115, -110 * isRight, 0);
                break;
            case 1: //shell
                decorationRotation = Quaternion.Euler(0, (90 + (90 * -isRight)), 0);
                break;
            case 2: //shell1
                decorationRotation = Quaternion.Euler(0, 0, 0);
                break;
            case 3: //shell2
                decorationRotation = Quaternion.Euler(0, -100 * isRight, 0);
                break;
            case 4: //FortBed
                spawnAt.position +=  new Vector3(0f, 3.5f, 0f);
                decorationRotation = Quaternion.Euler(-90, (100 + (110 * -isRight)), 175);
                break;
            case 5: //SpearedSkull
                spawnAt.position +=  new Vector3(0f, 6.5f, 0f);
                decorationRotation = Quaternion.Euler(5, 50 * isRight, 5);
                break;
            case 6: //Elephant
                decorationRotation = Quaternion.Euler(0, 150 * isRight, 0);
                break;
        }

        //VALUES
        //skull 1,2,3: (-115, -110, 0)  skull 4,5: (-115, 110, 0)
        //shell 1,2,3: (0, 0, 0)  shell 4,5: (0, 180, 0)
        //shell1 1,2,3: (0, 0, 0)  shell1 4,5: (0, 0, 0)
        //shell2 1,2,3: (0, -100, 0)  shell2 4,5: (0, 100, 0)
        //FortBed 1,2,3: (-90, -10, 175)  FortBed 4,5: (-90, 210, 175)
        //SpearedSkull 1,2,3: (5, 50, 5)  SpearedSkull 4,5: (5, -50,5)
        //Elephant 1,2,3: (0, 150, 0)  Elephant 4,5: (0, -150, 0)

        Instantiate(decorationPrefabs[decorationIndex], spawnAt.position, decorationRotation, transform);
    }
}