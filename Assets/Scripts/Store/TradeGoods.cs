using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TradeGoods : MonoBehaviour
{
    //used scripts
    private BaseGoodsDisplayer baseGoodsDisplayer;
    private ScoreManager scoreManager;
    private ShipStorage shipStorage;

    //private fields
    private bool atBase;

    //used UI
    [SerializeField] private Slider woodBuySlider;
    [SerializeField] private Slider woodSellSlider;
    [SerializeField] private Slider ironBuySlider;
    [SerializeField] private Slider ironSellSlider;
    [SerializeField] private Text woodBuyQuantityText;
    [SerializeField] private Text woodSellQuantityText;
    [SerializeField] private Text ironBuyQuantityText;
    [SerializeField] private Text ironSellQuantityText;
    [SerializeField] private Text coinBuyQuantityText;
    [SerializeField] private Text coinSellQuantityText;

    //shop variables
    private int woodBuyQuantity;
    private int woodSellQuantity;
    private int ironBuyQuantity;
    private int ironSellQuantity;
    private int coinBuyQuantity;
    private int coinSellQuantity;

    //ship variables
    private int shipStorageCapacity;

    void Start()
    {
        baseGoodsDisplayer = GameObject.FindObjectOfType<BaseGoodsDisplayer>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        shipStorage = GameObject.FindObjectOfType<ShipStorage>();

        shipStorageCapacity = shipStorage.shipStorageCapacity;

        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName("StoryBase"))
        {
            atBase = true;
        }
    }

    public void SetMaxSliderValues()
    {
        if(atBase)
        {
            woodSellSlider.maxValue = PlayerPrefs.GetInt("StoredWood");
            ironSellSlider.maxValue = PlayerPrefs.GetInt("StoredIron");
        }
        else
        {
            woodSellSlider.maxValue = PlayerPrefs.GetInt("CollectedWood");
            ironSellSlider.maxValue = PlayerPrefs.GetInt("CollectedIron");

            woodBuySlider.maxValue = shipStorageCapacity - (PlayerPrefs.GetInt("CollectedWood") + PlayerPrefs.GetInt("CollectedIron"));
            ironBuySlider.maxValue = shipStorageCapacity - (PlayerPrefs.GetInt("CollectedWood") + PlayerPrefs.GetInt("CollectedIron"));
        }
    }

    public void Buy()
    {
        PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") - coinBuyQuantity);

        if(atBase)
        {
            PlayerPrefs.SetInt("StoredWood", PlayerPrefs.GetInt("StoredWood") + woodBuyQuantity);
            PlayerPrefs.SetInt("StoredIron", PlayerPrefs.GetInt("StoredIron") + ironBuyQuantity);
        }
        else
        {
            PlayerPrefs.SetInt("CollectedWood", PlayerPrefs.GetInt("CollectedWood") + woodBuyQuantity);
            PlayerPrefs.SetInt("CollectedIron", PlayerPrefs.GetInt("CollectedIron") + ironBuyQuantity);
        }

        //when collectables are bought re-set the max values
        SetMaxSliderValues();

        DisplayScorePanelValues();
    }

    public void Sell()
    {
        PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + coinSellQuantity);

        if(atBase)
        {
            PlayerPrefs.SetInt("StoredWood", (PlayerPrefs.GetInt("StoredWood") - woodSellQuantity));
            PlayerPrefs.SetInt("StoredIron", (PlayerPrefs.GetInt("StoredIron") - ironSellQuantity));
        }
        else
        {
            PlayerPrefs.SetInt("CollectedWood", (PlayerPrefs.GetInt("CollectedWood") - woodSellQuantity));
            PlayerPrefs.SetInt("CollectedIron", (PlayerPrefs.GetInt("CollectedIron") - ironSellQuantity));
        }

        //when collectables are sold re-set the max values
        SetMaxSliderValues();

        DisplayScorePanelValues();
    }

    public void SetBuyQuantity()
    {
        woodBuyQuantity = (int)woodBuySlider.value;
        ironBuyQuantity = (int)ironBuySlider.value;

        int woodCost = woodBuyQuantity;
        int ironCost = ironBuyQuantity * 2;

        coinBuyQuantity = woodCost + ironCost;

        if((PlayerPrefs.GetInt("Currency") - ironCost) + ironBuyQuantity + PlayerPrefs.GetInt("CollectedWood") + PlayerPrefs.GetInt("CollectedIron") <= shipStorageCapacity || atBase)
        {
            woodBuySlider.maxValue = PlayerPrefs.GetInt("Currency") - ironCost;
        }
        else
        {
            woodBuySlider.maxValue = shipStorageCapacity - PlayerPrefs.GetInt("CollectedWood") + PlayerPrefs.GetInt("CollectedIron") - ironBuyQuantity;
        }

        if(((PlayerPrefs.GetInt("Currency") - woodCost) / 2) + woodBuyQuantity + PlayerPrefs.GetInt("CollectedWood") + PlayerPrefs.GetInt("CollectedIron") <= shipStorageCapacity || atBase)
        {
            ironBuySlider.maxValue = (PlayerPrefs.GetInt("Currency") - woodCost) / 2;
        }
        else
        {
            ironBuySlider.maxValue = shipStorageCapacity - PlayerPrefs.GetInt("CollectedWood") + PlayerPrefs.GetInt("CollectedIron") - woodBuyQuantity;
        }

        DisplayShopQuantity();
    }

    public void SetSellQuantity()
    {
        woodSellQuantity = (int)woodSellSlider.value;
        ironSellQuantity = (int)ironSellSlider.value;
        coinSellQuantity = woodSellQuantity + (ironSellQuantity * 2);

        DisplayShopQuantity();
    }

    void DisplayShopQuantity()
    {
        woodBuyQuantityText.text = woodBuyQuantity.ToString();
        woodSellQuantityText.text = woodSellQuantity.ToString();

        ironBuyQuantityText.text = ironBuyQuantity.ToString();
        ironSellQuantityText.text = ironSellQuantity.ToString();

        coinBuyQuantityText.text = coinBuyQuantity.ToString();
        coinSellQuantityText.text = coinSellQuantity.ToString();
    }

    void DisplayScorePanelValues()//Display new values on score panel
    {
        if(atBase)
        {
            baseGoodsDisplayer.DisplayGoods();
        }
        else
        {
            scoreManager.DisplayCollectableWood();
            scoreManager.DisplayCollectableIron();
        }
    }
}