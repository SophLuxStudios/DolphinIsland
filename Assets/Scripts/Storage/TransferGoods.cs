using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TransferGoods : MonoBehaviour
{
    //Used Scripts
    private ShipStorage shipStorage;
    private BaseStorage baseStorage;

    //Used UI
    [SerializeField] private Slider woodTransferSlider;
    [SerializeField] private Slider ironTransferSlider;
    [SerializeField] private Text woodShipQuantityText;
    [SerializeField] private Text woodBaseQuantityText;
    [SerializeField] private Text ironShipQuantityText;
    [SerializeField] private Text ironBaseQuantityText;
    [SerializeField] private Text autoUnloadText;

    //private fields
    private int shipWood;
    private int baseWood;
    private int shipIron;
    private int baseIron;
    private int totalWood;
    private int totalIron;
    private string autoOn = "AUTO\nUNLOAD\nIS ON";
    private string autoOff= "AUTO\nUNLOAD\nIS OFF";

    //public properties
    public int shipCapacity;
    public int baseCapacity;

    void Start()
    {
        shipStorage = GameObject.FindObjectOfType<ShipStorage>();
        baseStorage = GameObject.FindObjectOfType<BaseStorage>();

        SetCapacity();

        //If autounload is on
        if(PlayerPrefs.GetInt("IsAutoUnloadOn", 1) == 1)
        {
            AutoUnload();
            autoUnloadText.text = autoOn;
        }
        else
        {
            autoUnloadText.text = autoOff;
        }

        SetSliderValues();
    }

    public void SetSliderValues()
    {
        //set total values
        totalWood = PlayerPrefs.GetInt("CollectedWood") + PlayerPrefs.GetInt("StoredWood");
        totalIron = PlayerPrefs.GetInt("CollectedIron") + PlayerPrefs.GetInt("StoredIron");

        //Set transfer slider's max values
        woodTransferSlider.maxValue = totalWood;
        ironTransferSlider.maxValue = totalIron;

        //Set transfer slider's initial values
        woodTransferSlider.value = PlayerPrefs.GetInt("CollectedWood");
        shipWood = PlayerPrefs.GetInt("CollectedWood");
        ironTransferSlider.value = PlayerPrefs.GetInt("CollectedIron");
        shipIron = PlayerPrefs.GetInt("CollectedIron");

        //Display values of goods
        DisplaySliderValues();
    }

    private void DisplaySliderValues()
    {
        woodShipQuantityText.text = PlayerPrefs.GetInt("CollectedWood").ToString();
        woodBaseQuantityText.text = PlayerPrefs.GetInt("StoredWood").ToString();
        ironShipQuantityText.text = PlayerPrefs.GetInt("CollectedIron").ToString();
        ironBaseQuantityText.text = PlayerPrefs.GetInt("StoredIron").ToString();
    }

    public void SetCapacity()
    {
        shipCapacity = shipStorage.ShipStorageCapacity();
        baseCapacity = baseStorage.BaseStorageCapacity();
    }

    private void AutoUnload()
    {
        int emptyRoom = baseCapacity - (baseIron + baseWood);
        int carryingAmount = shipWood + shipIron;
        if(emptyRoom >= carryingAmount)
        {
            //Transfer wood to base
            PlayerPrefs.SetInt("StoredWood", 
            PlayerPrefs.GetInt("CollectedWood") + PlayerPrefs.GetInt("StoredWood"));
            //Clear wood on ship
            PlayerPrefs.SetInt("CollectedWood", 0);
            //Transfer iron to base
            PlayerPrefs.SetInt("StoredIron", 
            PlayerPrefs.GetInt("CollectedIron") + PlayerPrefs.GetInt("StoredIron"));
            //Clear iron on ship
            PlayerPrefs.SetInt("CollectedIron", 0);
        }
    }

    public void AutoUnloadToggle()
    {
        if(PlayerPrefs.GetInt("IsAutoUnloadOn", 1) == 0)
        {
            autoUnloadText.text = autoOn;
            PlayerPrefs.SetInt("IsAutoUnloadOn", 1);
            Debug.Log("Auto unload is on.");
        }
        else
        {
            autoUnloadText.text = autoOff;
            PlayerPrefs.SetInt("IsAutoUnloadOn", 0);
            Debug.Log("Auto unload is off.");
        }
    }

    public void WoodSliderValueChange()
    {
        shipWood = (int)woodTransferSlider.value;
        baseWood = totalWood - (int)woodTransferSlider.value;

        //If wood in ship exceeds capacity set it to capacity
        if(shipWood > (shipCapacity - shipIron))
        {
            woodTransferSlider.value = (shipCapacity - shipIron);
        }
        //If wood in base exceeds capacity set it to capacity
        if(shipWood > baseCapacity - baseIron)
        {
            woodTransferSlider.value = totalWood - (baseCapacity - baseIron);
        }

        //Display wood portion
        woodShipQuantityText.text = shipWood.ToString();
        woodBaseQuantityText.text = baseWood.ToString();
    }

    public void IronSliderValueChange()
    {
        shipIron = (int)ironTransferSlider.value;
        baseIron = totalIron - (int)ironTransferSlider.value;

        //If iron in ship exceeds capacity set it to capacity
        if(shipIron > (shipCapacity - shipWood))
        {
            ironTransferSlider.value = (shipCapacity - shipWood);
        }
        //If iron in base exceeds capacity set it to capacity
        if(baseIron > baseCapacity - baseWood)
        {
            ironTransferSlider.value = totalIron - (baseCapacity - baseWood);
        }

        //Display iron portion
        ironShipQuantityText.text = shipIron.ToString();
        ironBaseQuantityText.text = baseIron.ToString();
    }

    public void CompleteTransfer()
    {
        PlayerPrefs.SetInt("CollectedWood", (int)woodTransferSlider.value);
        PlayerPrefs.SetInt("StoredWood", (int)(woodTransferSlider.maxValue - (int)woodTransferSlider.value));

        PlayerPrefs.SetInt("CollectedIron", (int)ironTransferSlider.value);
        PlayerPrefs.SetInt("StoredIron", (int)(ironTransferSlider.maxValue - (int)ironTransferSlider.value));
    }
}