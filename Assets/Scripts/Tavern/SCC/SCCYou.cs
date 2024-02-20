using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCCYou : MonoBehaviour
{
    private SCC scc;
    private SCCThem sccThem;

    private int diceCount;
    private int remainingDiceCount = 5;

    private int diceSumTemp;
    private int diceSum;

    [SerializeField] private DiceSCC[] yourDiceSCC;

    private bool isSixBanked;
    private bool isFiveBanked;
    private bool isFourBanked;

    private int bankSixCount = 0;
    private int bankFiveCount = 0;
    private int bankFourCount = 0;

    [SerializeField] private GameObject you4Mark;
    [SerializeField] private GameObject you5Mark;
    [SerializeField] private GameObject you6Mark;
    [SerializeField] private Text yourCargoText;    

    void Start()
    {
        scc = GameObject.FindObjectOfType<SCC>();
        sccThem = GameObject.FindObjectOfType<SCCThem>();

        ResetDiceSum();
    }

    public void DiceLanded()
    {
        scc.SetRollDicePanel(false);
        Invoke("EndYourTurn", 3f);

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

        //CheckAllLanded();
    }

    void CheckSix()
    {
        foreach(DiceSCC die in yourDiceSCC)
        {
            if(die.diceResult == 6)
            {
                //Mark as Banked
                isSixBanked = true;

                remainingDiceCount = 4;

                //Bank it
                BankYourDice(die.diceResult);

                CheckFive();
            }
        }

        CheckAllLanded();
    }

    void CheckFive()
    {
        foreach(DiceSCC die in yourDiceSCC)
        {
            if(die.diceResult == 5)
            {
                //Mark as Banked
                isFiveBanked = true;

                remainingDiceCount = 3;

                //Bank it
                BankYourDice(die.diceResult);

                CheckFour();
            }
        }

        //CheckAllLanded();
    }

    void CheckFour()
    {
        foreach(DiceSCC die in yourDiceSCC)
        {
            if(die.diceResult == 4)
            {
                //Mark as Banked
                isFourBanked = true;

                remainingDiceCount = 2;

                //Mark your cargo as full
                scc.isYourCargoFull = true;

                //Bank it
                BankYourDice(die.diceResult);
            }
        }

        //CheckAllLanded();
    }

    void CheckAllLanded()
    {
        if(diceCount == remainingDiceCount)
        {
            Debug.Log("All dice has stopped.");

            
        }
    }

    void EndYourTurn()
    {
        RelocateDice();
        sccThem.TheirTurn();
    }

    void RelocateDice()
    {
        foreach(DiceSCC die in yourDiceSCC)
        {
            if(!die.isBanked)
            {
                die.Gravity(false); 
                die.transform.position += new Vector3( 500f, 500f, 500f);
            }
        }
    }

    void BankYourDice(int bankValue)
    {
        foreach(DiceSCC die in yourDiceSCC)
        {
            if(die.diceResult == bankValue && !die.isBanked)
            {

                die.isBanked = true;

                die.transform.position = new Vector3( (31.94f + (bankValue * .09f)), 6.15f, (7557.94f + (bankValue * .09f)));

                switch (bankValue)
                {
                    case 6:
                        bankSixCount++;
                        if(bankSixCount == 1)
                        {
                            die.transform.rotation = Quaternion.Euler(0, 70, 90);
                            you6Mark.gameObject.SetActive(true);
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
                            you5Mark.gameObject.SetActive(true);
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
                            you4Mark.gameObject.SetActive(true);
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
        foreach(DiceSCC die in yourDiceSCC)
        {
            die.gameObject.SetActive(activation);
        }
    }

    public void CalculateSCCCargo()
    {
        diceSumTemp = 0;
        diceSum = 0;

        foreach(DiceSCC die in yourDiceSCC)
        {
            if(die.hasLanded)
            {
                diceSumTemp += die.diceResult;
                //Debug.Log("Temp sum is " + diceSumTemp + ".");    
            }
        }

        diceSum = diceSumTemp - 15;
        //Debug.Log("Sum is " + diceSum + ".");

        yourCargoText.text = diceSum.ToString();

        scc.yourCargo = diceSum;
    }

    public void RollDice()
    {
        if(scc.isBetSet)
        {
            ResetDiceSum();

            foreach(DiceSCC die in yourDiceSCC)
            {
                if(!die.isBanked)
                {
                    die.RollDice();
                }
            }        
        }
        else
        {
            scc.BetNotSet();
        }
    }

    public void ResetPositionIfNotBanked()
    {
        foreach(DiceSCC die in yourDiceSCC)
        {
            if(!die.isBanked)
            {
                die.ResetDice();
            }
        }
    }

    public void ResetDiceSum()
    {
        diceCount = 0;

        diceSumTemp = 0;

        diceSum = 0;
    }

    public void ResetSCC()
    {
        bankSixCount = 0;
        bankFiveCount = 0;
        bankFourCount = 0;

        isFourBanked = false;
        isFiveBanked = false;
        isSixBanked = false;

        foreach(DiceSCC die in yourDiceSCC)
        {
            die.isBanked = false;
        }

        you4Mark.gameObject.SetActive(false);
        you5Mark.gameObject.SetActive(false);
        you6Mark.gameObject.SetActive(false);

        //yourCargoText.text = null;
    }
}