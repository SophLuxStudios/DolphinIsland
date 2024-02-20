using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapShip : MonoBehaviour
{
    //Map components
    private Animator roll;
    private float activationTime = 2.4f;
    private float deactivationTime = 0.3f;
    [SerializeField] private GameObject mapParchment;
    [SerializeField] private GameObject locationsPanel;

    //used scripts
    private ShipMovement shipMovement;
    private DolphinEncounter dolphinEncounter;

    //Map markings
    [SerializeField] private Image shipIcon;
    private RectTransform shipIconRT;
    private RectTransform currentLocationRT;
    [SerializeField] private Image questLocationMarkImage;
    private RectTransform questLocationMarkRT;
    [SerializeField] private Button[] Location = new Button[27];

    void Start()
    {
        roll = mapParchment.GetComponent<Animator>();
        shipMovement = GameObject.FindObjectOfType<ShipMovement>();
        dolphinEncounter = GameObject.FindObjectOfType<DolphinEncounter>();

        //Set anchor position of QuestLocationMarkImage
        MarkQuestLocation();

        //Set anchor positions of shipIcon as current location's
        currentLocationRT = Location[PlayerPrefs.GetInt("ShipLocatedAt")].GetComponent<RectTransform>();
        shipIconRT = shipIcon.GetComponent<RectTransform>();
        shipIconRT.anchoredPosition = currentLocationRT.anchoredPosition;

        switch(PlayerPrefs.GetInt("ShipLocatedAt"))
        {
            case 1:
                Location[0].gameObject.SetActive(true);
                Location[2].gameObject.SetActive(true);
                Location[6].gameObject.SetActive(true);
                Location[7].gameObject.SetActive(true);
                break;
            
            case 2:
                Location[0].gameObject.SetActive(true);
                Location[1].gameObject.SetActive(true);
                Location[3].gameObject.SetActive(true);
                Location[9].gameObject.SetActive(true);
                Location[10].gameObject.SetActive(true);
                break;

            case 3:
                Location[0].gameObject.SetActive(true);
                Location[2].gameObject.SetActive(true);
                Location[4].gameObject.SetActive(true);
                Location[11].gameObject.SetActive(true);
                Location[20].gameObject.SetActive(true);
                break;
            
            case 4:
                Location[0].gameObject.SetActive(true);
                Location[3].gameObject.SetActive(true);
                Location[5].gameObject.SetActive(true);
                Location[13].gameObject.SetActive(true);
                break;

            case 5:
                Location[0].gameObject.SetActive(true);
                Location[4].gameObject.SetActive(true);
                Location[6].gameObject.SetActive(true);
                Location[14].gameObject.SetActive(true);
                break;

            case 6:
                Location[0].gameObject.SetActive(true);
                Location[1].gameObject.SetActive(true);
                Location[5].gameObject.SetActive(true);
                Location[15].gameObject.SetActive(true);
                break;

            case 7:
                Location[1].gameObject.SetActive(true);
                Location[8].gameObject.SetActive(true);
                Location[26].gameObject.SetActive(true);
                break;

            case 8:
                Location[7].gameObject.SetActive(true);
                Location[9].gameObject.SetActive(true);
                Location[16].gameObject.SetActive(true);
                break;

            case 9:
                Location[2].gameObject.SetActive(true);
                Location[8].gameObject.SetActive(true);
                Location[17].gameObject.SetActive(true);
                break;

            case 10:
                Location[2].gameObject.SetActive(true);
                Location[18].gameObject.SetActive(true);
                Location[19].gameObject.SetActive(true);
                break;

            case 11:
                Location[3].gameObject.SetActive(true);
                Location[12].gameObject.SetActive(true);
                Location[21].gameObject.SetActive(true);
                break;

            case 12:
                Location[11].gameObject.SetActive(true);
                Location[13].gameObject.SetActive(true);
                Location[22].gameObject.SetActive(true);
                break;

            case 13:
                Location[4].gameObject.SetActive(true);
                Location[12].gameObject.SetActive(true);
                Location[14].gameObject.SetActive(true);
                Location[23].gameObject.SetActive(true);
                Location[24].gameObject.SetActive(true);
                break;

            case 14:
                Location[5].gameObject.SetActive(true);
                Location[13].gameObject.SetActive(true);
                Location[15].gameObject.SetActive(true);
                Location[25].gameObject.SetActive(true);
                break;

            case 15:
                Location[6].gameObject.SetActive(true);
                Location[14].gameObject.SetActive(true);
                Location[26].gameObject.SetActive(true);
                break;

            case 16:
                Location[8].gameObject.SetActive(true);
                Location[26].gameObject.SetActive(true);
                break;

            case 17:
                Location[9].gameObject.SetActive(true);
                Location[18].gameObject.SetActive(true);
                break;

            case 18:
                Location[10].gameObject.SetActive(true);
                Location[17].gameObject.SetActive(true);
                Location[19].gameObject.SetActive(true);
                break;

            case 19:
                Location[10].gameObject.SetActive(true);
                Location[18].gameObject.SetActive(true);
                Location[20].gameObject.SetActive(true);
                break;

            case 20:
                Location[3].gameObject.SetActive(true);
                Location[19].gameObject.SetActive(true);
                Location[21].gameObject.SetActive(true);
                break;

            case 21:
                Location[11].gameObject.SetActive(true);
                Location[20].gameObject.SetActive(true);
                Location[22].gameObject.SetActive(true);
                break;

            case 22:
                Location[12].gameObject.SetActive(true);
                Location[21].gameObject.SetActive(true);
                Location[23].gameObject.SetActive(true);
                break;

            case 23:
                Location[13].gameObject.SetActive(true);
                Location[22].gameObject.SetActive(true);
                Location[24].gameObject.SetActive(true);
                break;

            case 24:
                Location[13].gameObject.SetActive(true);
                Location[23].gameObject.SetActive(true);
                Location[25].gameObject.SetActive(true);
                break;

            case 25:
                Location[14].gameObject.SetActive(true);
                Location[24].gameObject.SetActive(true);
                Location[26].gameObject.SetActive(true);
                break;

            case 26:
                Location[7].gameObject.SetActive(true);
                Location[15].gameObject.SetActive(true);
                Location[16].gameObject.SetActive(true);
                Location[25].gameObject.SetActive(true);
                break;
        }
    }

    public void SetDestination(int location)
    {
        //storyCam.LocationDeactivate();

        LocationDeactivate();

        PlayerPrefs.SetInt("ShipLocatedAt", location);

        if(location == 0)
        {
            PlayerPrefs.SetString("SceneName", "StoryBase");
        }
        else
        {
            PlayerPrefs.SetString("SceneName", "StoryMode");
        }

        if(dolphinEncounter.encounter)
        {
            shipMovement.MoveOnMap();
        }
        else
        {
            shipMovement.UndockShip();
        }
    }

    public void MapAnimation()
    {
        if(roll.GetBool("Open"))
        {
            //mapParchmentCollider.center = new Vector3(-0.008f, 0, 0);
            //mapParchmentCollider.size = new Vector3(0.005f, 0.02f, 0.004f);
            roll.SetBool("Open", false);
            Invoke("LocationDeactivate", deactivationTime);
        }
        else
        {
            //mapParchmentCollider.center = new Vector3(0, 0, 0);
            //mapParchmentCollider.size = new Vector3(0.02f, 0.02f, 0.004f);
            roll.SetBool("Open", true);
            Invoke("LocationActivate", activationTime);
        }
    }

    void LocationActivate()
    {
        locationsPanel.SetActive(true);
    }

    public void LocationDeactivate()
    {
        locationsPanel.SetActive(false);
    }

    public void MarkQuestLocation()
    {
        Debug.Log("Quest Location is marked.");

        if(PlayerPrefs.GetInt("FetchQuestActive", 0) == 0)
        {
            questLocationMarkImage.gameObject.SetActive(false);
        }
        else questLocationMarkImage.gameObject.SetActive(true);

        if(PlayerPrefs.GetInt("PickedUp", 0) == 0)
        {
            questLocationMarkRT = questLocationMarkImage.GetComponent<RectTransform>();
            questLocationMarkRT.anchoredPosition = Location[PlayerPrefs.GetInt("QuestPickUpLocation")].GetComponent<RectTransform>().anchoredPosition;
        }
        else
        {
            questLocationMarkRT = questLocationMarkImage.GetComponent<RectTransform>();
            questLocationMarkRT.anchoredPosition = Location[PlayerPrefs.GetInt("QuestDeliveryLocation")].GetComponent<RectTransform>().anchoredPosition;
        }
    }
}