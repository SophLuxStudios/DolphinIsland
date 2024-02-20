using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagStore : MonoBehaviour
{
    //used class
    [SerializeField] private FlagData flagData;

    //private fields
    private const int displayedFlagsLength = 3;
    private GameObject[] displayedFlags = new GameObject[displayedFlagsLength];
    private int notOwnedFlagNumber;
    private int ownedFlagNumber;
    private int scrollIndex;///scroll index is the index of first item on store
    private int storeIndex;///store index is the index of selected item
    private int itemIndex;///item index is the index of Flag[]

    //serialized private fields
    [SerializeField] private GameObject[] flagButtons = new GameObject[displayedFlagsLength];

    //UI
    [SerializeField] private GameObject insufficientFundsPanel;
    [SerializeField] private GameObject pricePanel;
    [SerializeField] private Text[] priceText = new Text[displayedFlagsLength];

    private void Start()
    {
        flagData = transform.Find("FlagManager").gameObject.GetComponent<FlagData>();

        notOwnedFlagNumber = FlagData.notOwnedFlags.Count;
        ownedFlagNumber = FlagData.ownedFlags.Count;
        
        for(int i = 0; i < displayedFlagsLength; i++)
        {
            displayedFlags[i] = this.gameObject.transform.GetChild(i).gameObject;
        }

        scrollIndex = 0;
        SetStoreFlags();
    }

    private void SetStoreFlags()
    {
        notOwnedFlagNumber = FlagData.notOwnedFlags.Count;
        ownedFlagNumber = FlagData.ownedFlags.Count;

        int counter = 0;

        if(notOwnedFlagNumber < displayedFlagsLength)
        {
            for (int i = displayedFlagsLength - 1; i >= notOwnedFlagNumber; i--)
            {
                flagButtons[i].GetComponent<Button>().interactable = false;

                displayedFlags[i].SetActive(false);
            }

            for(int i = 0; i < notOwnedFlagNumber; i++)
            {
                displayedFlags[i].GetComponent<Renderer>().material = FlagData.notOwnedFlags[i].material;
            }
        }
        else
        {
            for(int i = 0; i < displayedFlagsLength; i++)
            {
                if(scrollIndex + i < notOwnedFlagNumber)
                {
                    displayedFlags[i].GetComponent<Renderer>().material = FlagData.notOwnedFlags[scrollIndex + i].material;
                }
                else
                {
                    displayedFlags[i].GetComponent<Renderer>().material = FlagData.notOwnedFlags[counter].material;
                    counter++;
                }
            }
        }
    }

    private void ResetFlagStore()
    {
        notOwnedFlagNumber = FlagData.notOwnedFlags.Count;
        ownedFlagNumber = FlagData.ownedFlags.Count;

        scrollIndex = 0;

        SetStoreFlags();
    }

    public void BuyFlag()
    {
        int currency = PlayerPrefs.GetInt("Currency");
        int price = FlagData.notOwnedFlags[itemIndex - ownedFlagNumber].price;

        if(currency >= price)
        {
            flagData.FlagBought(itemIndex);

            PlayerPrefs.SetInt("Currency", (currency - price));

            ResetFlagStore();
        }
        else
        {
            insufficientFundsPanel.SetActive(true);
        }
    }

    public void ScrollRight()
    {
        scrollIndex++;
        if(scrollIndex == notOwnedFlagNumber)
        {
            scrollIndex = 0;
        }

        SetStoreFlags();
    }

    public void ScrollLeft()
    {
        if(scrollIndex > 0)
        {
            scrollIndex--;
        }
        else
        {
            scrollIndex = notOwnedFlagNumber - 1;
        }

        SetStoreFlags();
    }

    public void SetStoreFlagIndex(int buttonIndex)
    {
        storeIndex = buttonIndex;

        itemIndex = scrollIndex + storeIndex + ownedFlagNumber;

        if(itemIndex >= ownedFlagNumber + notOwnedFlagNumber)
        {
            itemIndex -= notOwnedFlagNumber;
        }

        int price = FlagData.notOwnedFlags[itemIndex - ownedFlagNumber].price;
        priceText[buttonIndex].text = price.ToString();

        Debug.Log("Flag[" + itemIndex + "] is selected.");
    }
}