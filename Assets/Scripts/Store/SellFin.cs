using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellFin : MonoBehaviour
{
    [SerializeField] private Slider finSellSlider;
    [SerializeField] private Text finSellQuantityText;
    private int finSellQuantity;

    void Start()
    {
        SetMaxSliderValue();
    }

    private void SetMaxSliderValue()
    {
        finSellSlider.maxValue = PlayerPrefs.GetInt("OwnedFin");
    }

    public void SetQuantity()
    {
        finSellQuantity = (int)finSellSlider.value;
    }

    public void Sell()
    {
        PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + (finSellQuantity * 250));
        PlayerPrefs.SetInt("OwnedFin", PlayerPrefs.GetInt("OwnedFin") - finSellQuantity);

        //when fins are sold re-set the max values
        SetMaxSliderValue();
    }
}