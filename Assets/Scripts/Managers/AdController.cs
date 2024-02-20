using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdController : MonoBehaviour, IUnityAdsListener
{
    //Singleton
    private static AdController instance;

    //used class
    [SerializeField] private ResourceCamp resourceCamp;

    //UI
    [SerializeField] private GameObject pausePanel;

    //private fields
    private const string gameId = "5267908";
    private bool testMode = false;
    private const string rewardedAd = "Rewarded_Android";
    private const string rebornAd = "rebornAd";
    private const string interstitialAd ="Interstitial_Android";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Advertisement.AddListener(this);

        InitializeAds();
    }

    public void SetResourceCampScript(ResourceCamp script)
    {
        resourceCamp = script;
        Debug.Log("Script is found");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        //Make pausePanel null
        pausePanel = null;

        // Define conditional logic for each ad completion status:
        if(showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.

            Debug.Log("Reward type is: " + placementId);

            switch(placementId)
            {
                case "rebornAd":
                    ShipHealth.shipHealth.Reborn();
                    break;
                case "goldReward":
                    PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + 100);
                    FindObjectOfType<BaseGoodsDisplayer>().DisplayGoods();
                    break;
                case "timeReduction":
                    resourceCamp.TimeReductionReward();
                    break;               
            }

        }
        else if(showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
            Debug.Log("Do not reward player.");
        }
        else if(showResult == ShowResult.Failed)
        {
            Debug.LogWarning ("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady (string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == rewardedAd) {
            //Advertisement.Show (rewardedAd);
        }
    }
    
    public void OnUnityAdsDidError (string message)
    {
        //Make pausePanel null
        pausePanel = null;
    }

    public void OnUnityAdsDidStart (string placementId)
    {
        //Activate pausePanel
        GameObject canvas = GameObject.Find("Canvas");
        pausePanel = canvas.transform.GetChild(canvas.transform.childCount - 1).gameObject;
        pausePanel.SetActive(true);

        //Stop time
        Time.timeScale = 0.0f;
    }

    public void InitializeAds()
    {
        if(!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, testMode);
        }
    }

    public void PlayRewardAd(string rewardType)
    {
        Debug.Log(rewardType);
        
        //is the reward ad ready to play
        if(Advertisement.IsReady(rewardType))
        {
            Advertisement.Show(rewardType);
        }
        else
        {
            Debug.Log("Reward ad is not ready.");
        }
    }
}