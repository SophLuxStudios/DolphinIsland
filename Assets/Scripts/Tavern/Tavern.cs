using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tavern : MonoBehaviour
{
    private Hazard hazard;

    [SerializeField] private Camera tableCamera;
    [SerializeField] private Slider hazardBetSlider;
    [SerializeField] private TextMeshProUGUI coinTavernText;
    [SerializeField] private TextMeshProUGUI woodTavernText;
    [SerializeField] private TextMeshProUGUI ironTavernText;
    [SerializeField] private Text coinHazardText;
    [SerializeField] private GameObject hazardPanel;
    [SerializeField] private GameObject wonAndLostPanel;
    [SerializeField] private Text wonOrLostText;
    [SerializeField] private Text betAmountText;

    int diceSum;
    int betAmount;
    bool isRoundOver;

    void Start()
    {
        hazard = GameObject.FindObjectOfType<Hazard>();

        isRoundOver = true;

        SetNewMax();

        DisplayCollectables();
        DisplayTotalCoin();
        DisplayHazardCoin();
    }

    public void IsWon(bool isWon, bool isDraw)
    {
        hazardPanel.gameObject.SetActive(false);

        wonAndLostPanel.gameObject.SetActive(true);
        betAmountText.text = betAmount.ToString();

        if (isWon)
        {
            PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + betAmount);

            wonOrLostText.text = "WON";

            Debug.Log("Round is won.");
        }
        else if(!isWon && isDraw)
        {
            betAmountText.text = null;

            wonOrLostText.text = "DRAW";

            Debug.Log("Round ends with draw.");
        }
        else
        {
            PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") - betAmount);

            wonOrLostText.text = "LOST";

            Debug.Log("Round is lost.");
        }

        isRoundOver = true;

        DisplayTotalCoin();
        SetNewMax();
    }

    public void LeftAfterBet()
    {
        PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") - betAmount);

        isRoundOver = true;

        DisplayTotalCoin();
        SetNewMax();
    }

    void SetNewMax()
    {
        hazardBetSlider.maxValue = PlayerPrefs.GetInt("Currency");
    }

    public void UpdateTavernScorePanel()
    {
       DisplayCollectables();
       DisplayTotalCoin();
    }

    void DisplayCollectables()
    {
        woodTavernText.text = PlayerPrefs.GetInt("CollectedWood").ToString();
        ironTavernText.text = PlayerPrefs.GetInt("CollectedIron").ToString();
    }

    void DisplayTotalCoin()
    {
        coinTavernText.text = PlayerPrefs.GetInt("Currency").ToString();
    }

    void DisplayHazardCoin()
    {
        coinHazardText.text = betAmount.ToString();
    }

    public void SetHazardBetSlider()
    {
        if(isRoundOver)
        {
            betAmount = (int)hazardBetSlider.value;

            DisplayHazardCoin();
        }
        else
        {
            ///if bet is set fix slider to bet amount
            hazardBetSlider.value = betAmount;
        }
    }

    public void RoundHasStarted()
    {
        isRoundOver = false;
    }

    public void ResetBetAmount()
    {
        betAmount = 0;
        hazardBetSlider.value = betAmount;
    }

    public void ActivateTableCam()
    {
        tableCamera.gameObject.SetActive(true);
        tableCamera.enabled = true;
    }

    public void DeactivateTableCam()
    {
        tableCamera.enabled = false;
    }
}