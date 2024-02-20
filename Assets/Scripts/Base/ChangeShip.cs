using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShip : MonoBehaviour
{
    [SerializeField] GameObject[] shipVariations = new GameObject[3];
    [SerializeField] GameObject noOtherShipAvailablePanel;

    void Awake()
    {
        shipVariations[PlayerPrefs.GetInt("ShipVariationIndex", 0)].SetActive(true);
    }

    public void ChangeBoat()
    {
        if(PlayerPrefs.GetInt("BoatTwoAvaible", 0) == 1 || PlayerPrefs.GetInt("BoatThreeAvaible", 0) == 1)
        {
            DeactivatePreviousShip();

            if(PlayerPrefs.GetInt("BoatTwoAvaible", 0) == 0)
            {
                //toggle between boat and boat3
                PlayerPrefs.SetInt("ShipVariationIndex", PlayerPrefs.GetInt("ShipVariationIndex") == 0 ? 2 : 0);
            }
            else if(PlayerPrefs.GetInt("BoatThreeAvaible", 0) == 0)
            {
                //toggle between boat and boat2
                PlayerPrefs.SetInt("ShipVariationIndex", PlayerPrefs.GetInt("ShipVariationIndex") == 0 ? 1 : 0);
            }
            else
            {
                //all avaible
                PlayerPrefs.SetInt("ShipVariationIndex", PlayerPrefs.GetInt("ShipVariationIndex") + 1);

                if(PlayerPrefs.GetInt("ShipVariationIndex") > 2)
                {
                    PlayerPrefs.SetInt("ShipVariationIndex", 0);
                }
            }

            ActivateSelectedShip();
        }
        else
        {
            noOtherShipAvailablePanel.SetActive(true);
        }
    }

    private void ActivateSelectedShip()
    {
        shipVariations[PlayerPrefs.GetInt("ShipVariationIndex")].SetActive(true);
    }

    private void DeactivatePreviousShip()
    {
        shipVariations[PlayerPrefs.GetInt("ShipVariationIndex")].SetActive(false);
    }
}