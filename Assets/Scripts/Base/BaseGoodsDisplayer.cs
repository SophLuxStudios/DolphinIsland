using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseGoodsDisplayer : MonoBehaviour
{
    //used scripts
    [SerializeField] private TransferGoods transferGoods;

    //private fields
    private int totalWood;
    private int totalIron;

    //UI
    [SerializeField] private TextMeshProUGUI xWoodText;
    [SerializeField] private TextMeshProUGUI xIronText;
    [SerializeField] private TextMeshProUGUI xCoinText;
    [SerializeField] private TextMeshProUGUI xFinText;

    void Start()
    {
        DisplayGoods();
    }

    public void DisplayGoods()
    {
        totalWood = PlayerPrefs.GetInt("CollectedWood") + PlayerPrefs.GetInt("StoredWood");
        totalIron = PlayerPrefs.GetInt("CollectedIron") + PlayerPrefs.GetInt("StoredIron");

        transferGoods.SetSliderValues();

        xWoodText.text = "X " + totalWood.ToString();
        xIronText.text = "X " + totalIron.ToString();
        xCoinText.text = "X " + PlayerPrefs.GetInt("Currency", 0).ToString();
        xFinText.text = "X " + PlayerPrefs.GetInt("OwnedFin", 0).ToString();
    }
}