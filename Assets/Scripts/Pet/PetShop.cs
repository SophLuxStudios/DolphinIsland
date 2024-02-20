using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetShop : MonoBehaviour
{
    [SerializeField] private GameObject buyParrotPanel;
    [SerializeField] private GameObject alreadyBoughtPanel;
    [SerializeField] private GameObject insufficientFundsPanel;

    private const int parrotPrice = 400;

    public void BuyParrot()
    {
        if(PlayerPrefs.GetInt("ParrotOwned", 0) == 0)
        {
            buyParrotPanel.SetActive(true);
        }
        else
        {
            alreadyBoughtPanel.SetActive(true);
        }
    }

    public void ParrotBought()
    {
        if(PlayerPrefs.GetInt("Currency") >= parrotPrice)
        {
            PlayerPrefs.SetInt("ParrotOwned", 1);

            PlayerPrefs.SetInt("Currency", (PlayerPrefs.GetInt("Currency") - parrotPrice));
        }
        else
        {
            insufficientFundsPanel.SetActive(true);
        }
    }
}