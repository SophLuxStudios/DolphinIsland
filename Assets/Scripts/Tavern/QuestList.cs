using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestList : MonoBehaviour
{
    //used class'
    private InGameMenu inGameMenu;

    //serialized UIs
    [SerializeField] private Text FetchQuestText;
    [SerializeField] private Text CollectQuestText;
    [SerializeField] private Text HuntQuestText;

    void Start()
    {
        DisplayQuestList();
    }

    public void DisplayQuestList()
    {
        //if no quest
        if(PlayerPrefs.GetInt("FetchQuestActive", 0) == 0 &&
           PlayerPrefs.GetInt("CollectQuestActive", 0) == 0 &&
           PlayerPrefs.GetInt("HuntQuestActive", 0) == 0)
        {
            FetchQuestText.text = "You do not have any quests. \n(Visit a tavern to acquire)";
        }
        else
        {
            //Fetch Quest Info
            if(PlayerPrefs.GetInt("FetchQuestActive", 0) == 1)
            {
                FetchQuestText.text = PlayerPrefs.GetString("FetchQuestText");
            }
            else
            {
                FetchQuestText.text = "Quest completed.";
            }
            
            //Collect Quest Info
            if(PlayerPrefs.GetInt("CollectQuestActive", 0) == 1)
            {
                CollectQuestText.text = PlayerPrefs.GetString("CollectQuestText");
            }
            else
            {
                CollectQuestText.text = "Quest completed.";
            }
            
            //Hunt Quest Info
            if(PlayerPrefs.GetInt("HuntQuestActive", 0) == 1)
            {
                HuntQuestText.text = PlayerPrefs.GetString("HuntQuestText");
            }
            else if(PlayerPrefs.GetInt("HuntQuestGiven", 0) == 1)
            {
                HuntQuestText.text = "Quest completed.";
            }
        }
    }
}