using UnityEngine;

public class Shipwright : MonoBehaviour
{
    private const int boatTwoPrice = 3000;
    private const int boatThreePrice = 5000;
    [SerializeField] private GameObject buyBoatTwo;
    [SerializeField] private GameObject buyBoatThree;
    [SerializeField] private GameObject boughtTwo;
    [SerializeField] private GameObject boughtThree;
    [SerializeField] private GameObject insufficientFundsPanel;

    void Start()
    {
        if(PlayerPrefs.GetInt("BoatTwoAvaible", 0) == 1)
        {
            BoatTwoIsBought();
        }

        if(PlayerPrefs.GetInt("BoatThreeAvaible", 0) == 1)
        {
            BoatThreeIsBought();
        }
    }

    public void BuyBoatTwo()
    {
        if(PlayerPrefs.GetInt("Currency") >= boatTwoPrice)
        {
            PlayerPrefs.SetInt("BoatTwoAvaible", 1);
            BoatTwoIsBought();
            PlayerPrefs.SetInt("Currency", (PlayerPrefs.GetInt("Currency") - boatTwoPrice));
        }
        else
        {
            insufficientFundsPanel.SetActive(true);
        }
    }

    public void BuyBoatThree()
    {
        if(PlayerPrefs.GetInt("Currency") >= boatThreePrice)
        {
            PlayerPrefs.SetInt("BoatThreeAvaible", 1);
            BoatThreeIsBought();
            PlayerPrefs.SetInt("Currency", (PlayerPrefs.GetInt("Currency") - boatThreePrice));
        }
        else
        {
            insufficientFundsPanel.SetActive(true);
        }
    }

    private void BoatTwoIsBought()
    {
        buyBoatTwo.SetActive(false);
        boughtTwo.SetActive(true);
    }

    private void BoatThreeIsBought()
    {
        buyBoatThree.SetActive(false);
        boughtThree.SetActive(true);
    }
}