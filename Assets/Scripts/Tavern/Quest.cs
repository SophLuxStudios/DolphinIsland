using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    //used class'
    [SerializeField] GameObject CanvasMap;
    private MapShip mapShip;
    private ShipStorage shipStorage;
    [SerializeField] private QuestList questList;

    //UI
    [SerializeField] GameObject questsNotCompletePanel;

    //private variables
    private int shipStorageCapacity;
    private int[] tavernLocations = new int[] {8, 11, 23, 25};
    private int fetchLocation;
    private int itemQuantity;
    private int fetchReward;
    private int collectQuantity;
    private int collectReward;

    private void Start()
    {
        mapShip = CanvasMap.GetComponent<MapShip>();
        shipStorage = GameObject.FindObjectOfType<ShipStorage>();

        shipStorageCapacity = shipStorage.shipStorageCapacity;

        CheckQuest();
    }

    public void GiveQuest()
    {
        if(PlayerPrefs.GetInt("FetchQuestActive", 0) == 1 ||
           PlayerPrefs.GetInt("CollectQuestActive", 0) == 1 ||
           PlayerPrefs.GetInt("HuntQuestActive", 0) == 1)
        {
            questsNotCompletePanel.SetActive(true);
        }
        else
        {
            GiveFetchQuest();

            GiveCollectQuest();

            if(PlayerPrefs.GetInt("HarpoonAmount") > 0)
            {
                GiveHuntQuest();
            }

            questList.DisplayQuestList();
        }
    }

    private void CheckQuest()
    {
        CheckFetchQuest();
    }

    private void GiveFetchQuest()
    {
        do
        {
            fetchLocation = tavernLocations[Random.Range(0,4)];
        } while(fetchLocation == PlayerPrefs.GetInt("ShipLocatedAt"));
            
        switch (PlayerPrefs.GetInt("ShipStorageUpgradeLevel"))
        {
            case 0:
                itemQuantity = Random.Range(50,101);
                fetchReward = itemQuantity/2; //award is between: 25-50
                break;
            case 1:
                itemQuantity = Random.Range(100,251);
                fetchReward = itemQuantity/2; //award is between: 50-125
                break;
            case 2:
                itemQuantity = Random.Range(200,501);
                fetchReward = itemQuantity/3; //award is between: 66-166
                break;
            case 3:
                itemQuantity = Random.Range(300,751);
                fetchReward = itemQuantity/3; //award is between: 100-250
                break;
            case 4:
                itemQuantity = Random.Range(500,1501);
                fetchReward = itemQuantity/4; //award is between: 125-375
                break;
        }

        /// MARK THE MAP
        PlayerPrefs.SetInt("QuestPickUpLocation", fetchLocation);
        PlayerPrefs.SetInt("QuestDeliveryLocation", PlayerPrefs.GetInt("ShipLocatedAt"));

        //Store quantity and reward
        PlayerPrefs.SetInt("FetchQuestQuantity", itemQuantity);
        PlayerPrefs.SetInt("FetchQuestReward", fetchReward);

        //Quest Text
        PlayerPrefs.SetString("FetchQuestText", "Pick up a shipment of " + itemQuantity + " item from the tavern which is marked on your map.");
        //Debug.Log("Location: " + fetchLocation + ".  Item Quantity: " + itemQuantity + ".  Reward: " + fetchReward);

        PlayerPrefs.SetInt("FetchQuestActive", 1);

        mapShip.MarkQuestLocation();
    }

    private void CheckFetchQuest()
    {
        if(PlayerPrefs.GetInt("PickedUp", 0) == 0)
        {
            if(PlayerPrefs.GetInt("ShipLocatedAt") == PlayerPrefs.GetInt("QuestPickUpLocation"))
            {
                //Check if there is room to store items
                if(PlayerPrefs.GetInt("CollectedWood") + PlayerPrefs.GetInt("CollectedIron") + 
                PlayerPrefs.GetInt("FetchQuestQuantity") <= shipStorageCapacity)
                {
                    PlayerPrefs.SetInt("FetchQuestQuantityCarried", PlayerPrefs.GetInt("FetchQuestQuantity"));
                    PlayerPrefs.SetInt("PickedUp", 1);

                    PlayerPrefs.SetString("FetchQuestText", "Deliver a shipment of " + itemQuantity + " items to the tavern which is marked on your map.");
                }
            }
            else
            {
                ///////////////////YOU ARE AT PICK UP LOCATION BUT THERE IS NO ROOM FOR QUEST SHIPMENT//////////////////////////
            }
        }
        else
        {
            if(PlayerPrefs.GetInt("ShipLocatedAt") == PlayerPrefs.GetInt("QuestDeliveryLocation"))
            {
                PlayerPrefs.SetInt("FetchQuestActive", 0);

                PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + PlayerPrefs.GetInt("FetchQuestReward"));

                PlayerPrefs.SetInt("FetchQuestQuantity", 0);
            }
        }
    }

    private void GiveCollectQuest()
    {
        int collectQuestTier = Random.Range(0,6);
        switch (collectQuestTier)
        {
            case 0:
                collectQuantity = 100;
                collectReward = 50;
                break;
            case 1:
            case 2:
                collectQuantity = 200;
                collectReward = 100;
                break;
            case 3:
            case 4:
                collectQuantity = 300;
                collectReward = 150;
                break;
            case 5:
                collectQuantity = 400;
                collectReward = 200;
                break;
        }

        int collectQuestType = Random.Range(0,2);
        if(collectQuestType == 1)//iron
        {
            PlayerPrefs.SetString("CollectQuestType", "CollectableIron");

            //iron modifier
            collectReward = (int)(collectReward * 1.5f);
        }
        else//wood
        {
            PlayerPrefs.SetString("CollectQuestType", "CollectableWood");
        }

        //Store quantity, type and reward
        PlayerPrefs.SetInt("CollectQuestQuantity", collectQuantity);
        PlayerPrefs.SetInt("CollectQuestReward", collectReward);
        //Quest Text
        PlayerPrefs.SetString("CollectQuestText", "Collect " + collectQuantity + " pieces of " + PlayerPrefs.GetString("CollectQuestType") + ".");

        PlayerPrefs.SetInt("CollectQuestActive", 1);
    }

    private void GiveHuntQuest()
    {
        int huntQuantity = Random.Range(3,7);
        int huntReward = huntQuantity * 100;

        //Store quantity and reward
        PlayerPrefs.SetInt("HuntQuestQuantity", huntQuantity);
        PlayerPrefs.SetInt("HuntQuestReward", huntReward);
        //Quest Text
        PlayerPrefs.SetString("HuntQuestText", "Hunt " + huntQuantity + " dolphins.");

        PlayerPrefs.SetInt("HuntQuestActive", 1);
        PlayerPrefs.SetInt("HuntQuestGiven", 1);
    }
}