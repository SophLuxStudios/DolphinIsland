using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    //used class'
    private ShipColor shipColor;

    //private fields
    private readonly string[] colors = {"Yellow", "Red", "Black", "Blue", "Pink", "Green"};
    private List<string> ownedColors = new List<string>();
    [SerializeField] private Material[] paintMaterials = new Material[6];
    private List<Material> ownedPaintMaterials = new List<Material>();
    [SerializeField] GameObject[] paintCans = new GameObject[6];
    [SerializeField] GameObject[] colorButtons = new GameObject[6];


    void Awake()
    {
        shipColor = GetComponent<ShipColor>();

        for(int i = 0; i < colors.Length; i++)
        {
            string playerPrefsName = colors[i] + "PaintBought";
            if(PlayerPrefs.GetInt(playerPrefsName, 0) == 1)
            {
                colorButtons[i].SetActive(true);
                ownedColors.Add(colors[i]);
                ownedPaintMaterials.Add(paintMaterials[i]);
            }
        }

        for(int i = 0; i < ownedColors.Count; i++)
        {
            paintCans[i].SetActive(true);
            paintCans[i].gameObject.GetComponent<Renderer>().material = ownedPaintMaterials[i];
        }
    }
}