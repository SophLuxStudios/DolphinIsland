using UnityEngine;
using UnityEngine.UI;
using System;

public class Upgrades : MonoBehaviour
{
    //Used Scripts
    private ShipStorage shipStorage;
    private BaseStorage baseStorage;
    private BaseGoodsDisplayer baseGoodsDisplayer;

    //UI
    [SerializeField] private GameObject insufficientFundsPanel;
    [SerializeField] private GameObject alreadyAtMaxPanel;
    [SerializeField] private GameObject[] cost = new GameObject[displayedUpgradeCount];
    [SerializeField] private Text[] upgradeNameText = new Text[displayedUpgradeCount];
    [SerializeField] private Text[] upgradeCostText = new Text[displayedUpgradeCount];
    [SerializeField] private GameObject[] upgradedSigns = new GameObject[displayedUpgradeCount * maxUpgrade];
    
    //upgrade variables
    [SerializeField] private UpgradableProperties[] Upgradable = new UpgradableProperties[displayedUpgradeCount];
    private const int displayedUpgradeCount = 2;
    private const int maxUpgrade = 4;
    private int currency;
    private int upgradeCost;

    private void Start()
    {
        shipStorage = GameObject.FindObjectOfType<ShipStorage>();
        baseStorage = GameObject.FindObjectOfType<BaseStorage>();
        baseGoodsDisplayer = GameObject.FindObjectOfType<BaseGoodsDisplayer>();

        UpgradeCategory("Storage");

        currency = PlayerPrefs.GetInt("Currency");
    }

    private void SetUpgradePanel()
    {
        for(int i = 0; i < Upgradable.Length; i++)
        {
            //Display Upgrade Name
            upgradeNameText[i].text = "UPGRADE " + Upgradable[i].displayableName;
            //Set Upgrade Cost
            upgradeCost = SetUpgradeCost(Upgradable[i].upgradeLevel);
            //Display Upgrade Cost if it is not at maximum
            if(Upgradable[i].upgradeLevel < maxUpgrade)
            {
                cost[i].SetActive(true);
                upgradeCostText[i].text = upgradeCost.ToString();
            }
            else
            {
                cost[i].SetActive(false);
            }
            //Activate Upgraded Signs
            for (int j = i * maxUpgrade; j < (i * maxUpgrade) + Upgradable[i].upgradeLevel; j++)
            {
                upgradedSigns[j].SetActive(true);
            }
            //deactivate Upgraded Signs
            for (int j = (Upgradable[i].upgradeLevel + (i * maxUpgrade)); j < ((i + 1) * maxUpgrade); j++)
            {
                //Debug.Log("i is : " + i + "    J is : " + j);
                upgradedSigns[j].SetActive(false);
            }
        }
    }

    [Serializable]
    private struct UpgradableProperties
    {
        public string category;

        public string name;

        public string displayableName;

        public int upgradeLevel;

        public int upgradeCost;
    }
    
    public void UpgradeCategory(string name)
    {
        switch(name)
        {
            case "Storage":
                UpgradableConstructor(name, 0, "ShipStorage", "SHIP\nSTORAGE");
                UpgradableConstructor(name, 1, "BaseStorage", "BASE\nSTORAGE");
                SetUpgradePanel();
                break;
            case "Resource":
                UpgradableConstructor(name, 0, "LumberCamp", "LUMBER\nCAMP");
                UpgradableConstructor(name, 1, "MiningCamp", "MINING\nCAMP");
                SetUpgradePanel();
                break;
            case "Hunt":
                UpgradableConstructor(name, 0, "HarpoonAmount", "HARPOON\nAMOUNT");
                UpgradableConstructor(name, 1, "DolphinFinder", "DOLPHIN\nFINDER");
                SetUpgradePanel();
                break;
        }
    }

    private void UpgradableConstructor(string category, int upgradableIndex, string name, string displayableName)
    {
        Upgradable[upgradableIndex].category = category;
        Upgradable[upgradableIndex].name = name;
        Upgradable[upgradableIndex].displayableName = displayableName;
        Upgradable[upgradableIndex].upgradeLevel = PlayerPrefs.GetInt(name + "UpgradeLevel");
        Upgradable[upgradableIndex].upgradeCost = SetUpgradeCost(Upgradable[upgradableIndex].upgradeLevel);   
    }

    public void Upgrade(int upgradableIndex)
    {
        if(Upgradable[upgradableIndex].upgradeLevel == maxUpgrade)
        {
            alreadyAtMaxPanel.SetActive(true);
        }
        else if(currency >= Upgradable[upgradableIndex].upgradeCost)
        {
            FindObjectOfType<AudioManager>().Play("Hammering");

            int upgradedLevel = PlayerPrefs.GetInt(Upgradable[upgradableIndex].name + "UpgradeLevel") + 1;
            PlayerPrefs.SetInt(Upgradable[upgradableIndex].name + "UpgradeLevel", upgradedLevel);
            //SetUpgradePanel();

            PlayerPrefs.SetInt("Currency", currency - Upgradable[upgradableIndex].upgradeCost);
            currency = PlayerPrefs.GetInt("Currency");

            //Display new currency
            baseGoodsDisplayer.DisplayGoods();
            
            //Reconstruct
            UpgradeCategory(Upgradable[upgradableIndex].category);

            //When storage is upgraded reset capacity
            if(Upgradable[upgradableIndex].name == "ShipStorage")
            {
                shipStorage.SetShipStorageCapacity();
            }
            else if(Upgradable[upgradableIndex].name == "BaseStorage")
            {
                baseStorage.SetBaseStorageCapacity();
            }
        }
        else if(currency < Upgradable[upgradableIndex].upgradeCost)
        {
            insufficientFundsPanel.SetActive(true);
        }
    }

    private int SetUpgradeCost(int upgradeLevel)
    {
        switch(upgradeLevel)
        {
            case 0:
                return 500;
            case 1:
                return 1000;
            case 2:
                return 2500;
            case 3:
                return 5000;
            default:
                return 0;
        }
    }
}