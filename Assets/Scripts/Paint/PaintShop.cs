using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintShop : MonoBehaviour
{
    [SerializeField] private GameObject buyPaintPanel;
    [SerializeField] private GameObject alreadyBoughtPanel;
    [SerializeField] private GameObject insufficientFundsPanel;
    private const int paintPrice = 200;

    private string playerPrefsName;

    public void SelectPaint(string paintName)
    {
        playerPrefsName = paintName + "PaintBought";
        
        if(PlayerPrefs.GetInt(playerPrefsName, 0) == 0)
        {
            buyPaintPanel.SetActive(true);
        }
        else
        {
            alreadyBoughtPanel.SetActive(true);
        }
    }

    public void AddToBoughtPaints()
    {
        if(PlayerPrefs.GetInt("Currency") >= paintPrice)
        {
            PlayerPrefs.SetInt(playerPrefsName, 1);

            PlayerPrefs.SetInt("Currency", (PlayerPrefs.GetInt("Currency") - paintPrice));
        }
        else
        {
            insufficientFundsPanel.SetActive(true);
        }
    }
}