using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCCThem : MonoBehaviour
{
    private SCC scc;
    private SCCYou sccYou;

    private int diceCount;
    private int remainingDiceCount = 5;

    private int diceSumTemp;
    private int diceSum;

    [SerializeField] private DiceSCC[] theirDiceSCC;

    private bool isSixBanked;
    private bool isFiveBanked;
    private bool isFourBanked;

    private int bankSixCount = 0;
    private int bankFiveCount = 0;
    private int bankFourCount = 0;

    [SerializeField] private GameObject them4Mark;
    [SerializeField] private GameObject them5Mark;
    [SerializeField] private GameObject them6Mark;
    [SerializeField] private Text theirCargoText;

    void Start()
    {
        scc = GameObject.FindObjectOfType<SCC>();
        sccYou = GameObject.FindObjectOfType<SCCYou>();

        ResetDiceSum();
    }

    public void DiceLanded()
    {
        diceCount++;

        if(!isSixBanked)
        {
            CheckSix();
        }
        else
        {
            if(!isFiveBanked)
            {
                CheckFive();
            }
            else
            {
                if(!isFourBanked)
                {
                    CheckFour();
                }
                else
                {
                    CalculateSCCCargo();
                }
            }
        }

        if(diceCount == remainingDiceCount)
        {
            scc.SCCRules();

            Invoke("YourTurn", 2f);

            //Debug.Log("All dice has stopped.");
        }
    }

    void CheckSix()
    {
        foreach(DiceSCC die in theirDiceSCC)
        {
            if(die.diceResult == 6)
            {
                //Mark as Banked
                isSixBanked = true;

                remainingDiceCount = 4;

                //Bank it
                BankDice(die.diceResult);

                CheckFive();
            }
        }
    }

    void CheckFive()
    {
        foreach(DiceSCC die in theirDiceSCC)
        {
            if(die.diceResult == 5)
            {
                //Mark as Banked
                isFiveBanked = true;

                remainingDiceCount = 3;

                //Bank it
                BankDice(die.diceResult);

                CheckFour();
            }
        }
    }

    void CheckFour()
    {
        foreach(DiceSCC die in theirDiceSCC)
        {
            if(die.diceResult == 4)
            {
                //Mark as Banked
                isFourBanked = true;

                remainingDiceCount = 2;

                //Mark their cargo as full
                scc.isTheirCargoFull = true;

                //Bank it
                BankDice(die.diceResult);
            }
        }
    }

    void ResetPositionIfNotBanked()
    {
        //sccYou.RelocateDice();

        foreach(DiceSCC die in theirDiceSCC)
        {
            if(!die.isBanked)
            {
                die.ResetDice();
            }
        }
    }

    void BankDice(int bankValue)
    {
        foreach(DiceSCC die in theirDiceSCC)
        {
            if(die.diceResult == bankValue && !die.isBanked)
            {

                die.isBanked = true;

                die.transform.position = new Vector3( (31.65f - (bankValue * .09f)), 6.15f, (7559.85f - (bankValue * .09f)));

                switch (bankValue)
                {
                    case 6:
                        bankSixCount++;
                        if(bankSixCount == 1)
                        {
                            die.transform.rotation = Quaternion.Euler(0, 70, 90);
                            them6Mark.gameObject.SetActive(true);
                        }
                        else
                        {
                            die.SendBack();
                            die.isBanked = false;
                        }
                        break;
                    case 5:
                        bankFiveCount++;
                        if(bankFiveCount == 1)
                        {
                            die.transform.rotation = Quaternion.Euler(90, 70, 0);
                            them5Mark.gameObject.SetActive(true);
                        }
                        else
                        {
                            die.SendBack();
                            die.isBanked = false;
                        }
                        break;
                    case 4:
                        bankFourCount++;
                        if(bankFourCount == 1)
                        {
                            die.transform.rotation = Quaternion.Euler(0, 70, 180);
                            them4Mark.gameObject.SetActive(true);
                        }
                        else
                        {
                            die.SendBack();
                            die.isBanked = false;
                        }
                        break;
                }
            }
        }
    }

    public void ActivateSCCDice(bool activation)
    {
        foreach(DiceSCC die in theirDiceSCC)
        {

            die.gameObject.SetActive(activation);

            if(activation)
                die.transform.position += new Vector3( 600f, 600f, 600f);
            
        }
    }

    public void CalculateSCCCargo()
    {
        diceSumTemp = 0;
        diceSum = 0;

        foreach(DiceSCC die in theirDiceSCC)
        {
            if(die.hasLanded)
            {
                diceSumTemp += die.diceResult;
                //Debug.Log("Temp sum is " + diceSumTemp + ".");    
            }
        }

        diceSum = diceSumTemp - 15;
        //Debug.Log("Sum is " + diceSum + ".");

        theirCargoText.text = diceSum.ToString();

        scc.theirCargo = diceSum;
    }

    public void TheirTurn()
    {
        //ActivateTheirDice(true);

        ResetPositionIfNotBanked();

        Invoke("RollDice", 1f);
    }

    public void ActivateTheirDice(bool activation)
    {
        foreach(DiceSCC die in theirDiceSCC)
        {
            die.gameObject.SetActive(activation);
        }
    }

    public void YourTurn()
    {
        if(!scc.isRoundOver)
        {
            RelocateDice();

            sccYou.ResetPositionIfNotBanked();
            scc.whosTurn = "Yours";

            scc.SetRollDicePanel(true);
        }
    }

    public void RollDice()
    {
        scc.whosTurn = "Theirs";

        ResetDiceSum();

        foreach(DiceSCC die in theirDiceSCC)
        {
            if(!die.isBanked)
            {
                die.RollDice();
            }
        }
    }

    void RelocateDice()
    {
        foreach(DiceSCC die in theirDiceSCC)
        {
            if(!die.isBanked)
            {
                die.Gravity(false);
                die.transform.position += new Vector3( 600f, 600f, 600f);
            }
        }
    }

    void ResetDiceSum()
    {
        diceCount = 0;

        diceSumTemp = 0;

        diceSum = 0;
    }

    public void ResetSCC()
    {
        ResetPositionIfNotBanked();

        bankSixCount = 0;
        bankFiveCount = 0;
        bankFourCount = 0;

        isFourBanked = false;
        isFiveBanked = false;
        isSixBanked = false;

        foreach(DiceSCC die in theirDiceSCC)
        {
            die.isBanked = false;
        }

        Debug.Log("Check.");
        them4Mark.gameObject.SetActive(false);
        them5Mark.gameObject.SetActive(false);
        them6Mark.gameObject.SetActive(false);

        //theirCargoText.text = null;
    }

}