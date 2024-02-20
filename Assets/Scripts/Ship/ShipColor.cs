using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipColor : MonoBehaviour
{
    [SerializeField] private Material[] boatMaterials = new Material[3];

    public void SetColor(int colorIndex)
    {
        switch(colorIndex)
        {
            case 0:
                boatMaterials[PlayerPrefs.GetInt("ShipVariationIndex", 0)].color = Color.yellow;
                break;
            case 1:
                boatMaterials[PlayerPrefs.GetInt("ShipVariationIndex", 0)].color = Color.red;
                break;
            case 2:
                boatMaterials[PlayerPrefs.GetInt("ShipVariationIndex", 0)].color = new Color(0.15f, 0.15f, 0.15f, 1);//black 40,40,40
                break;
            case 3:
                boatMaterials[PlayerPrefs.GetInt("ShipVariationIndex", 0)].color = new Color(0, 0.45f, 1f, 1);//blue 0,120,255
                break;
            case 4:
                boatMaterials[PlayerPrefs.GetInt("ShipVariationIndex", 0)].color = new Color(1, 0.41f, 0.71f, 1);//pink 255,105,180
                break;
            case 5:
                boatMaterials[PlayerPrefs.GetInt("ShipVariationIndex", 0)].color = new Color(0, 0.62f, 0, 1);//green 0,160,0
                break;
            case 6:
                switch(PlayerPrefs.GetInt("ShipVariationIndex", 0))
                {
                    case 0:
                        boatMaterials[0].color = new Color(0.59f, 0.59f, 0.59f, 1);//150,150,150
                        break;
                    case 1:
                        boatMaterials[1].color = new Color(0.785f, 0.666f, 0.51f, 1);//200,170,130
                        break;
                    case 2:
                        boatMaterials[2].color = new Color(1, 1, 1, 1);
                        break;
                }
            break;
        }
    }
}