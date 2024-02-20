using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCC : MonoBehaviour
{
    private Tavern tavern;
    private SCCYou sccYou;
    private SCCThem sccThem;

    [SerializeField] private GameObject stopPlayingSCCPanel;
    [SerializeField] private GameObject SCCBetNotSetPanel;
    [SerializeField] private GameObject SCCRollDicePanel;

    public bool isRoundOver;
    public bool isBetSet;
    public string whosTurn;

    private int diceSumTemp;
    private int diceSum;


    public bool isYourCargoFull;
    public bool isTheirCargoFull;

    public int yourCargo;
    public int theirCargo;

    void Start()
    {
        tavern = GameObject.FindObjectOfType<Tavern>();
        sccYou = GameObject.FindObjectOfType<SCCYou>();
        sccThem = GameObject.FindObjectOfType<SCCThem>();

        whosTurn = "Yours";
    }

    public void SCCRules()
    {
        if(isYourCargoFull && isTheirCargoFull)
        {
            if(yourCargo > theirCargo)
            {
                RoundOver(true, false);
            }
            else if(theirCargo > yourCargo)
            {
                RoundOver(false, false);
            }
            else if(yourCargo == theirCargo)
            {
                RoundOver(false, true);
            }
        }
        else if(isYourCargoFull && !isTheirCargoFull)
        {
            RoundOver(true, false); 
        }
        else if(!isYourCargoFull && isTheirCargoFull)
        {
            RoundOver(false, false);
        }
    }

    void RoundOver(bool isWon, bool isDraw)
    {
        //Round is over
        isRoundOver = true;
        isBetSet = false;
        AllowStopPlaying(true);
        isYourCargoFull = false;
        isTheirCargoFull = false;
        ////////////

        yourCargo = 0;
        theirCargo = 0;

        sccYou.ResetSCC();
        sccThem.ResetSCC();

        SCCRollDicePanel.gameObject.SetActive(true);

        tavern.IsWon(isWon, isDraw);
    }

    public void SetRollDicePanel(bool isActive)
    {
        SCCRollDicePanel.gameObject.SetActive(isActive);
    }

    void AllowStopPlaying(bool isAllowed)
    {
        stopPlayingSCCPanel.gameObject.SetActive(isAllowed);
    }

    public void ActivateSCCDice(bool activation)
    {
        sccThem.ActivateSCCDice(activation);
        sccYou.ActivateSCCDice(activation);
    }

    public void BetNotSet()
    {
        SCCBetNotSetPanel.gameObject.SetActive(true);
    }

    public void SetBet()
    {
        isRoundOver = false;
        isBetSet = true;
        
        AllowStopPlaying(false);
    }

    public void StopPlaying()
    {
        //tavern.currentlyPlaying = null;

        sccYou.ResetDiceSum();
        //////////////////////////////////////////

        isRoundOver = true;
    }
}