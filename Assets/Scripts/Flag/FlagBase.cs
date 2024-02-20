using UnityEngine;

public class FlagBase : MonoBehaviour
{
    //private fields
    private const int displayedFlagsLength = 3;
    private GameObject[] displayedFlags = new GameObject[displayedFlagsLength];
    private int ownedFlagNumber;
    private int scrollIndex;///scroll index is the index of first item on store

    //serialized private fields
    [SerializeField] private GameObject shipFlag;
    [SerializeField] private GameObject flagSelectionPanel;
    
    private void Start()
    {
        ownedFlagNumber = FlagData.ownedFlags.Count;

        SetShipFlagMaterial(PlayerPrefs.GetInt("EquippedFlag", 0));

        for(int i = 0; i < displayedFlagsLength; i++)
        {
            displayedFlags[i] = this.gameObject.transform.GetChild(i).gameObject;
        }

        scrollIndex = 0;
        SetOptionFlagMaterial();
    }

    private void SetOptionFlagMaterial()
    {
        int counter = 0;

        for(int i = 0; i < displayedFlagsLength; i++)
        {
            if(scrollIndex + i < ownedFlagNumber)
            {
                displayedFlags[i].GetComponent<Renderer>().material = FlagData.ownedFlags[scrollIndex + i].material;
            }
            else
            {
                displayedFlags[i].GetComponent<Renderer>().material = FlagData.ownedFlags[counter].material;
                counter++;
            }
        }
    }

    public void ScrollUp()
    {
        scrollIndex++;

        if(scrollIndex == ownedFlagNumber)
        {
            scrollIndex = 0;
        }

        SetOptionFlagMaterial();
    }

    public void ScrollDown()
    {
        if(scrollIndex > 0)
        {
            scrollIndex--;
        }
        else
        {
            scrollIndex = ownedFlagNumber - 1;
        }

        SetOptionFlagMaterial();
    }

    public void SetShipFlagIndex(int buttonIndex)
    {
        int index;

        if(scrollIndex + buttonIndex < ownedFlagNumber)
        {
            index = scrollIndex + buttonIndex;
        }
        else
        {
            index = scrollIndex + buttonIndex - ownedFlagNumber;
        }

        SetShipFlagMaterial(index);
    }

    private void SetShipFlagMaterial(int index)
    {
        shipFlag.GetComponent<Renderer>().material = FlagData.ownedFlags[index].material;

        PlayerPrefs.SetInt("EquippedFlag", index);

        Debug.Log("Equipped flag[" + index + "]");
    }

    public void AtShipPosition(bool activation)
    {
        flagSelectionPanel.SetActive(activation);

        for(int i = 0; i < displayedFlagsLength; i++)
        {
            displayedFlags[i].SetActive(activation);
        }
    }
}