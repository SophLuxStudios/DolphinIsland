using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hazard : MonoBehaviour
{
    Tavern tavern;

    [SerializeField] private GameObject chanceDisplay;
    [SerializeField] private Slider hazardBetSlider;
    [SerializeField] private Slider hazardMainSlider;
    [SerializeField] private Text chanceHazardText;
    [SerializeField] private GameObject stopPlayingHazardPanel;
    [SerializeField] private GameObject betNotSetPanel;
    [SerializeField] private GameObject leaveAndLosePanel;
    [SerializeField] private GameObject pleaseMakeABetPanel;
    [SerializeField] private GameObject hazardPanel;
    [SerializeField] private GameObject tavernPanel;

    [SerializeField] private DiceHazard[] diceHazard;
    private int diceCount;
    private int diceSumTemp;


    int mainHazard;
    int diceSum;
    int chanceHazard;
    bool isChance;
    bool isRoundOver;
    bool isBetSet;

    void Start()
    {
        tavern = GameObject.FindObjectOfType<Tavern>();

        isChance = false;
        isRoundOver = true;
        isBetSet = false;
    }

    void HazardRules()
    {
        if(isChance)
        {
            if(diceSum == chanceHazard)
            {
                RoundOver(true);
            }
            else if(diceSum == mainHazard)
            {
                RoundOver(false);
            }
        }
        else
        {
            if(diceSum == mainHazard)
            {
                RoundOver(true);  
            }
            else if(diceSum == 2 || diceSum == 3)
            {
                RoundOver(false);
            }
            else if(diceSum == 11)
            {
                if(mainHazard == 7)
                {
                    RoundOver(true);
                }
                else
                {
                    RoundOver(false);
                }
            }
            else if(diceSum == 12)
            {
                if(mainHazard == 6 || mainHazard == 8)
                {
                    RoundOver(true);
                }
                else
                {
                    RoundOver(false);
                }
            }
            else
            {
                IsChanceHazard(true);

                Debug.Log("Chance is " + chanceHazard + ".");
            }
        }
    }

    public void DiceLanded()
    {    
        foreach(DiceHazard die in diceHazard)
        {
            if(die.diceResultUpdated && diceCount < 2)
            {
                diceSumTemp += die.diceResult;
                    
                diceCount++;
                die.diceResultUpdated = false;

                if (diceCount == 2)
                {
                    diceSum = diceSumTemp;
                    //Debug.Log("Dice sum is " + diceSum + ".");

                    ///whenever sum changes means dice rolled     
                    HazardRules();
                }
            }
        }    
    }

    public void StopPlaying()
    {
        if(!isBetSet)
        {
            QuitGame();
        }
        else
        {
            leaveAndLosePanel.gameObject.SetActive(true); 
        }
    }

    public void QuitGame()
    {
        if(isBetSet)
        {
            tavern.LeftAfterBet();
        }

        tavern.DeactivateTableCam();
        tavernPanel.gameObject.SetActive(true);
        hazardPanel.gameObject.SetActive(false);

        ResetDiceSum();

        isChance = false;
        isRoundOver = true;
        isBetSet = false;
        tavern.ResetBetAmount();

        mainHazard = 0;
        chanceHazard = 0;
    }

    void RoundOver(bool isWon)
    {
        IsChanceHazard(false);

        //Round is over
        isRoundOver = true;
        isBetSet = false;
        ////////////

        tavern.IsWon(isWon, false);
    }

    public void RollDice()
    {
        if(isBetSet)
        {
            ResetDiceSum();

            foreach(DiceHazard die in diceHazard)
            {
                die.RollDice();
            }
        }
        else
        {
            BetNotSet();
        }
    }

    void BetNotSet()
    {
        betNotSetPanel.gameObject.SetActive(true);
    }

    void ResetDiceSum()
    {
        diceSumTemp = 0;

        diceSum = 0;

        diceCount = 0;
    }


    void IsChanceHazard(bool chanceBool)
    {
        isChance = chanceBool;
        chanceDisplay.gameObject.SetActive(chanceBool);

        //set chance and display it
        if (chanceBool)
        {
            chanceHazard = diceSum;

            chanceHazardText.text = chanceHazard.ToString();
        }
    }

    public void SetBet()
    {
        if(hazardBetSlider.value == 0)
        {
            pleaseMakeABetPanel.gameObject.SetActive(true);
        }
        else
        {
            isRoundOver = false;
            isBetSet = true;

            mainHazard = (int)hazardMainSlider.value;

            Debug.Log("Bet is set. Main is " + mainHazard + ".");

            tavern.RoundHasStarted();
        }
    }

    public void SetHazardMain()
    {
        if(isRoundOver)
        {
            mainHazard = (int)hazardMainSlider.value;
        }
        else
        {
            hazardMainSlider.value = mainHazard;
        }
    }

    public void ActivateHazardDice(bool activation)
    {
        foreach(DiceHazard die in diceHazard)
        {
            die.gameObject.SetActive(activation);
        }
    }
}