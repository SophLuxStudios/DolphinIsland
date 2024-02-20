using UnityEngine;
using UnityEngine.SceneManagement;

public class MapBase : MonoBehaviour
{
    //used classes
    private CameraStoryBase cameraStoryBase;

    //private fields
    private int fastTravelLocation;

    [SerializeField] private GameObject insufficientFundsPanel;
    [SerializeField] private GameObject fastTravelPanel;

    [SerializeField] private GameObject Location7, Location8, Location9, Location10,
    Location11, Location12, Location13, Location14,Location15, Location16,
    Location17, Location18, Location19, Location20, Location21, Location22,
    Location23, Location24, Location25, Location26;

    void Start()
    {
        cameraStoryBase = GameObject.FindObjectOfType<CameraStoryBase>();

        PlayerPrefs.SetInt("ShipLocatedAt", 0);

        DiscoveryCheck();
    }

    public void MapLocation(int location)
    {
        if(cameraStoryBase.isAtMapPosition)
        {
            PlayerPrefs.SetInt("ShipLocatedAt", location);

            Sail();
        }
    }

    private void Sail()
    {
        cameraStoryBase.MoveToShip();
        cameraStoryBase.setSail = true;
    }

    public void FastTravelSetLocation(int location)
    {
        fastTravelLocation = location;

        fastTravelPanel.SetActive(true);
    }

    public void FastTravel()
    {
        int currency = PlayerPrefs.GetInt("Currency");

        if(currency >= 50)
        {
            PlayerPrefs.SetInt("Currency", (currency - 50));
            MapLocation(fastTravelLocation);
        }
        else
        {
            insufficientFundsPanel.SetActive(true);
        }
    }

    private void DiscoveryCheck()
    {
        if(PlayerPrefs.GetInt("DiscoveredLocation7", 0) == 1)
        {
            Location7.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("DiscoveredLocation8", 0) == 1)
        {
            Location8.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("DiscoveredLocation9", 0) == 1)
        {
            Location9.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("DiscoveredLocation10", 0) == 1)
        {
            Location10.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("DiscoveredLocation11", 0) == 1)
        {
            Location11.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("DiscoveredLocation12", 0) == 1)
        {
            Location12.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("DiscoveredLocation13", 0) == 1)
        {
            Location13.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("DiscoveredLocation14", 0) == 1)
        {
            Location14.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("DiscoveredLocation15", 0) == 1)
        {
            Location15.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("DiscoveredLocation16", 0) == 1)
        {
            Location16.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("DiscoveredLocation17", 0) == 1)
        {
            Location17.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("DiscoveredLocation18", 0) == 1)
        {
            Location18.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("DiscoveredLocation19", 0) == 1)
        {
            Location19.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("DiscoveredLocation20", 0) == 1)
        {
            Location20.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("DiscoveredLocation21", 0) == 1)
        {
            Location21.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("DiscoveredLocation22", 0) == 1)
        {
            Location22.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("DiscoveredLocation23", 0) == 1)
        {
            Location23.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("DiscoveredLocation24", 0) == 1)
        {
            Location24.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("DiscoveredLocation25", 0) == 1)
        {
            Location25.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("DiscoveredLocation26", 0) == 1)
        {
            Location26.gameObject.SetActive(true);
        }
    }
}