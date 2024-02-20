using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinEncounter : MonoBehaviour
{
    public bool encounter;
    public bool alreadyEncountered;

    [SerializeField] GameObject dolphin;

    //used Class
    private ShipMovement shipMovement;

    //Camera
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera dolphinEncounterCamera;
    
    //UI
    [SerializeField] private GameObject canvasDolphinEncounter;
    [SerializeField] private GameObject dolphinHuntPanel;
    [SerializeField] private GameObject noEncounterPanel;
    [SerializeField] private GameObject noHarpoonPanel;

    //Map
    [SerializeField] private GameObject mapParchmentRoll;


    void Start()
    {
        shipMovement = GameObject.FindObjectOfType<ShipMovement>();
    }

    public void LookForDolphin()
    {
        int encounterRoll = Random.Range(1, 11);
        if(PlayerPrefs.GetInt("HarpoonAmountUpgradeLevel") > 0)
        {
            //make if(encounterRoll < 11)// 100% chance FOR TEST
            //algorithm: 20% + (upgradeLevel * 20)% chance
            if(encounterRoll < 3 + (PlayerPrefs.GetInt("DolphinFinderUpgradeLevel") * 2))
            {
                encounter = true;
            
                shipMovement.UndockShip();
            }
            else
            {
                noEncounterPanel.SetActive(true);
                shipMovement.failedToFindDolphin = true;
            }
        }
        else
            noHarpoonPanel.SetActive(true);
    }

    public void ShipAtEncounterPosition()
    {
        dolphinEncounterCamera.gameObject.SetActive(true);
        dolphinEncounterCamera.enabled = false;

        dolphinHuntPanel.gameObject.SetActive(true);

        //run only once parameter
        alreadyEncountered = true;

        //activate dolphin
        dolphin.SetActive(true);
    }

    public void StartHunting()
    {
        SwitchCamToDolphinEncounter(true);

        //Instantiate harpoon
        gameObject.GetComponent<DolphinHunt>().SpawnHarpoon();

        //start chasing dolphin
        shipMovement.chasingDolphin = true;
    }

    private void SwitchCamToDolphinEncounter(bool isDolphinEncounter)
    {
        dolphinEncounterCamera.enabled = isDolphinEncounter;
        mainCamera.enabled = !isDolphinEncounter;

        //whenever dolphin cam is enabled activate its canvas
        canvasDolphinEncounter.SetActive(isDolphinEncounter);
    }

    public void EncounterEnded()
    {
        SwitchCamToDolphinEncounter(false);

        mapParchmentRoll.transform.position = new Vector3
        (
            mapParchmentRoll.transform.position.x,
            mapParchmentRoll.transform.position.y,
            (shipMovement.transform.position.z - 14f)
        );
    }
}