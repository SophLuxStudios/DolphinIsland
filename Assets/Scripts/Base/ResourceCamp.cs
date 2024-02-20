using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ResourceCamp : MonoBehaviour
{
    //Used Scripts
    private BaseStorage baseStorage;
    private BaseGoodsDisplayer baseGoodsDisplayer;

    //DateTime
    DateTime currentDate;
    DateTime lastCollectedDate;

    //private fields
    private float collectCoolDown = 86400;
    private string resourceToDisplay;

    //UI
    [SerializeField] private GameObject resourceTimerPanel;
    [SerializeField] private GameObject notEnoughRoomAtStoragePanel;
    [SerializeField] private Text resourceTimerText;

    void Start()
    {

        baseStorage = GameObject.FindObjectOfType<BaseStorage>();
        baseGoodsDisplayer = GameObject.FindObjectOfType<BaseGoodsDisplayer>();

        SetThisToAdController();
    }

    void Update()
    {
        DisplayCoolDown(resourceToDisplay);
    }

    public void SetThisToAdController()
    {
        FindObjectOfType<AdController>().SetResourceCampScript(this);
        Debug.Log("Script is sent");
    }

    public void CollectResource(string resourceType)
    {
        resourceToDisplay = resourceType;

        resourceTimerPanel.SetActive(true);
        
        CancelInvoke("DeactivateCoolDownDisplay");
        Invoke("DeactivateCoolDownDisplay", 5.0f);

        if(CheckCoolDown(resourceType))
        {
            SaveDate(resourceType);

            ResourceCollected(resourceType);
            Debug.Log("Resource Collected.");
        }
        else
        {
            Debug.Log("Wait for cooldown.");  
        }
    }

    private void DeactivateCoolDownDisplay()
    {
        resourceTimerPanel.SetActive(false);
    }

    private void DisplayCoolDown(string resourceType)
    {
        //float dateAfterReduction = CheckDate(resourceType) - PlayerPrefs.GetInt("TotalTimeReduction", 0);

        int textfieldHours = (int)(collectCoolDown - CheckDate(resourceType)) / 3600;
        int textfieldMinutes = ((int)(collectCoolDown - CheckDate(resourceType)) - (textfieldHours * 3600)) / 60;
        int textfieldSeconds = ((int)(collectCoolDown - CheckDate(resourceType)) - (textfieldHours * 3600) - (textfieldMinutes * 60));

        if(collectCoolDown <= CheckDate(resourceType))
        {
            resourceTimerText.text = "00:00:00";
        }
        else
        {
            resourceTimerText.text = 
            resourceType.Substring(0, 4) + "\n" + textfieldHours.ToString("D2") + ":" + textfieldMinutes.ToString("D2") + ":" + textfieldSeconds.ToString("D2");
        }
    }

    private bool CheckCoolDown(string resourceType)
    {
        bool resourceReadyToCollect = false;
        
        if(collectCoolDown <= CheckDate(resourceType))
        {
            resourceReadyToCollect = true;
        }

        return resourceReadyToCollect;
    }

    //Check the difference between current and saved time
    public float CheckDate(string resourceType)
    {
        currentDate = System.DateTime.Now;

        string saveLocation = resourceType + "Timer";
        string tempString = PlayerPrefs.GetString(saveLocation, "1");

        //Grab the last collected date from PlayerPrefs as a long
        long tempLong = Convert.ToInt64(tempString);

        //Convert the last collected date from binary to DateTime
        DateTime lastCollectedDate = DateTime.FromBinary(tempLong);

        //Use the subtract method and store the result as a timespan
        TimeSpan difference = currentDate - lastCollectedDate;

        //set time reduction PlayerPrefs name for resource
        string timeReductionPlayerPrefsName = "TotalTimeReduction" + resourceType;

        return (float)difference.TotalSeconds + PlayerPrefs.GetInt(timeReductionPlayerPrefsName, 0);
    }

    public void TimeReductionReward()
    {
        PlayerPrefs.SetInt("TotalTimeReductionWoodResource", PlayerPrefs.GetInt("TotalTimeReductionWoodResource", 0) + 7200);
        PlayerPrefs.SetInt("TotalTimeReductionIronResource", PlayerPrefs.GetInt("TotalTimeReductionIronResource", 0) + 7200);
    }

    public void SaveDate(string resourceType)
    {
        string saveLocation = resourceType + "Timer";
        PlayerPrefs.SetString(saveLocation, System.DateTime.Now.ToBinary().ToString());
    }

    private void ResourceCollected(string resourceType)
    {
        string upgradeName = null;
        int modifier = 1;
        int reward = 0;

        if(resourceType == "WoodResource")
        {
            upgradeName = "LumberCampUpgradeLevel";
        }
        else if(resourceType == "IronResource")
        {
            upgradeName = "MiningCampUpgradeLevel";
            modifier = 2;
        }

        switch(PlayerPrefs.GetInt(upgradeName, 0))
        {
            case 0:
                reward = 25 / modifier;
                break;
            case 1:
                reward = 100 / modifier;
                break;
            case 2:
                reward = 200 / modifier;
                break;
            case 3:
                reward = 500 / modifier;
                break;
            case 4:
                reward = 1000 / modifier;
                break;
        }

        if(resourceType == "WoodResource")
        {
            if(baseStorage.baseStorageCapacity >= (PlayerPrefs.GetInt("StoredWood") + reward))
            {
                PlayerPrefs.SetInt("StoredWood", (PlayerPrefs.GetInt("StoredWood") + reward));

                baseGoodsDisplayer.DisplayGoods();

                PlayerPrefs.SetInt("TotalTimeReductionWoodResource", 0);
            }
            else
            {
                notEnoughRoomAtStoragePanel.SetActive(true);
            }
        }
        else if(resourceType == "IronResource")
        {
            if(baseStorage.baseStorageCapacity >= (PlayerPrefs.GetInt("StoredIron") + reward))
            {
                PlayerPrefs.SetInt("StoredIron", (PlayerPrefs.GetInt("StoredIron") + reward));

                baseGoodsDisplayer.DisplayGoods();

                PlayerPrefs.SetInt("TotalTimeReductionIronResource", 0);
            }
            else
            {
                notEnoughRoomAtStoragePanel.SetActive(true);
            }
        }
    }
}