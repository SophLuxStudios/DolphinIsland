using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapacityDisplayer : MonoBehaviour
{
    //used scripts
    [SerializeField] private ShipStorage shipStorage;
    [SerializeField] private BaseStorage baseStorage;

    //UI
    [SerializeField] private Text shipCapacityText;
    [SerializeField] private Text baseCapacityText;

    void OnEnable()
    {
        DisplayCapacity();
    }

    private void DisplayCapacity()
    {
        shipCapacityText.text = shipStorage.ShipStorageCapacity().ToString();
        baseCapacityText.text = baseStorage.BaseStorageCapacity().ToString();
    }
}