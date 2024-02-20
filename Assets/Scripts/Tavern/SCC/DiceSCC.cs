using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSCC : MonoBehaviour
{
    private Rigidbody rb;
    public int diceResult;
    public bool hasLanded;
    bool rolled;
    public bool isBanked;
    [SerializeField] private Vector3 initialPosition;
    private Vector3 landingPosition;
    public DiceSide[] diceSides; 

    private SCCYou sccYou;
    private SCCThem sccThem;
    private SCC scc;

    //[SerializeField] private int forceModifier = 70;

    void Start()
    {
        sccYou = GameObject.FindObjectOfType<SCCYou>();
        sccThem = GameObject.FindObjectOfType<SCCThem>();
        scc = GameObject.FindObjectOfType<SCC>();

        rb = GetComponent<Rigidbody>();
        //initialPosition = transform.position;
        this.transform.Rotate(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), Space.Self);

        rb.useGravity = false;

        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if(rb.IsSleeping() && !hasLanded && rolled)
        {
            hasLanded = true;
            rb.useGravity = false;

            rb.isKinematic = true;

            SideValueCheck();
        }
        else if(rb.IsSleeping() && hasLanded && diceResult == 0)
        {
            RollAgain();
        }
    }

    public void RollDice()
    {
        if(!rolled && !hasLanded)
        {
            rolled = true;
            rb.useGravity = true;

            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));

            //rb.AddForce(transform.up * forceModifier);
        }
        else if(rolled && hasLanded)
        {
            ResetDice();
        }
    }

    public void ResetDice()
    {
        transform.position = initialPosition;
        this.transform.Rotate(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), Space.Self);

        rolled = false;

        hasLanded = false;

        rb.useGravity = false;

        rb.isKinematic = false;
    }

    void RollAgain()
    {
        ResetDice();

        rolled = true;
        rb.useGravity = true;

        rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
    }

    void SideValueCheck()
    {
        diceResult = 0;

        landingPosition = transform.position;

        foreach(DiceSide side in diceSides)
        {
            if(side.DiceLanded())
            {
                diceResult = side.sideValue;

                //Debug.Log(diceResult + " has been rolled.");

                if(scc.whosTurn == "Yours")
                {
                    sccYou.DiceLanded();
                }
                else if(scc.whosTurn == "Theirs")
                {
                    sccThem.DiceLanded();
                }
            }
        }
    }

    public void Gravity(bool use)
    {
        rb.useGravity = use;
    }

    public void SendBack()
    {
        transform.position = landingPosition;
    }
}