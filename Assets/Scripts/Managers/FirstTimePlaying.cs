using UnityEngine;

public class FirstTimePlaying : MonoBehaviour
{
    void Awake()
    {
        if(!PlayerPrefs.HasKey("FirstTimePlaying"))
        {
            //Debug.Log("It is the first time playing.");

            //upgrades
            PlayerPrefs.SetInt("ShipStorageUpgradeLevel", 0);
            PlayerPrefs.SetInt("BaseStorageUpgradeLevel", 0);
            PlayerPrefs.SetInt("HarpoonAmountUpgradeLevel", 0);
            PlayerPrefs.SetInt("DolphinFinder", 0);

            //collectables and currency
            PlayerPrefs.SetInt("CollectedWood", 0);
            PlayerPrefs.SetInt("CollectedIron", 0);
            PlayerPrefs.SetInt("Currency", 0);
            PlayerPrefs.SetInt("ShipCapacityReached", 0);

            //Set Auto-unload as ON
            //PlayerPrefs.SetInt("IsAutoUnloadOn", 1);

            //Set Volume
            //PlayerPrefs.SetFloat("musicSliderValue", .6f);
            //PlayerPrefs.SetFloat("effectSliderValue", .6f);

            //set PlayerPrefs to is not first time
            PlayerPrefs.SetInt("FirstTimePlaying", 0);
        }
        /*else
        {
            //Debug.Log("It is NOT the first time playing.");
        }*/
    }
}