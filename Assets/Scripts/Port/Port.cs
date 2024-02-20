using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Port : MonoBehaviour
{
    //port signs
    [SerializeField] private GameObject tavernSign;
    [SerializeField] private GameObject shopSign;
    [SerializeField] private GameObject shipwrightSign;

    //Cameras
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera tavernCamera;

    //Canvas
    [SerializeField] Canvas canvasTavern;
    [SerializeField] Canvas canvasMain;

    //Objects
    [SerializeField] private GameObject dock;
    [SerializeField] private GameObject port;
    [SerializeField] private GameObject tavernObjects;
    [SerializeField] private GameObject tavernWater;
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject terrainTileForTavernCam;
    [SerializeField] private GameObject flag;

    private int mapLocation;

    [HideInInspector]
    public string portContains;

    //used UI
    [SerializeField] private Text portText;
    [SerializeField] private GameObject shopPanel, shipwrightPanel;

    void Start()
    {
        flag = GameObject.Find("FlagPlane");
        mapLocation = PlayerPrefs.GetInt("ShipLocatedAt");
        
        if(mapLocation == 8 || mapLocation == 11 || mapLocation == 23 || mapLocation == 25)
        {
            AtTavern();
        }
        else if(mapLocation == 13 || mapLocation == 17 || mapLocation == 20 || mapLocation == 26)
        {
            AtShop();
        }
        else if(mapLocation == 10)
        {
            AtShipwright();
        }
        else
        {
            SetDockActive();
            portContains = "dock";
        }
    }

    void SetPortActive()
    {
        transform.GetChild(1).gameObject.SetActive(true);
    }

    void SetDockActive()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    void AtTavern()
    {
        SetPortActive();

        //Set proper sign active
        tavernSign.SetActive(true);
        portContains = "tavern";

        portText.text = "TAVERN";

        //Set required objects active
        tavernCamera.gameObject.SetActive(true);
        tavernObjects.gameObject.SetActive(true);

        //Disable tavernCamera
        tavernCamera.enabled = false;
    }
    void AtShop()
    {
        SetPortActive();

        //Set proper sign active
        shopSign.SetActive(true);
        portContains = "shop";

        portText.text = "STORE";
    }
    void AtShipwright()
    {
        SetPortActive();

        //Set proper sign active
        shipwrightSign.SetActive(true);
        portContains = "shipwright";

        portText.text = "SHIP WRIGHT";
    }

    public void PortButton()
    {
        if(portContains == "tavern")
        {
            //change water
            tavernWater.gameObject.SetActive(true);
            water.gameObject.SetActive(false);

            //re-position flag
            RepositionFlag(true);

            //Add terrain tile for tavern camera angle
            terrainTileForTavernCam.gameObject.SetActive(true);

            //change camera
            mainCamera.enabled = false;
            tavernCamera.enabled = true;

            //Set tavern canvas active 
            canvasTavern.gameObject.SetActive(true);
            canvasMain.gameObject.SetActive(false);
        }
        else if(portContains == "shop")
        {
            shopPanel.SetActive(true);
        }
        else
        {
            shipwrightPanel.SetActive(true);
        }
    }

    private void RepositionFlag(bool isTavern)
    {
        if(isTavern)
        {
            flag.transform.localPosition = new Vector3(-.4f, 3.7f, .4f);
            flag.transform.localRotation = Quaternion.Euler(90, 320, 270);
        }
        else
        {
            flag.transform.localPosition = new Vector3(0.51f, 3.66f, -0.3f);
            flag.transform.localRotation = Quaternion.Euler(90, 120, 270);
        }
    }

    public void ReturnShipButton()
    {
        //change water
        tavernWater.gameObject.SetActive(false);
        water.gameObject.SetActive(true);

        //re-position flag
        RepositionFlag(false);

        //deactivate tavern canvas
        canvasTavern.gameObject.SetActive(false);
        canvasMain.gameObject.SetActive(true);

        //change camera
        mainCamera.enabled = true;
        tavernCamera.enabled = false;
    }
}