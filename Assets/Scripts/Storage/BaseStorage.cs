using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStorage : MonoBehaviour
{
    //Used Scripts
    private TransferGoods transferGoods;

    [HideInInspector]
    public int baseStorageCapacity;

    //Base Storage Objects
    [SerializeField] private GameObject baseStorageUpgradeObject1;
    [SerializeField] private GameObject baseStorageUpgradeObject2;
    [SerializeField] private GameObject baseStorageUpgradeObject3;
    [SerializeField] private GameObject baseStorageUpgradeObject4;

    void Awake()
    {
        transferGoods = GameObject.FindObjectOfType<TransferGoods>();

        ActivateUpgradedObjects();

        SetBaseStorageCapacity();
    }

    public void SetBaseStorageCapacity()
    {
        switch (PlayerPrefs.GetInt("BaseStorageUpgradeLevel"))
        {
            case 0:
                baseStorageCapacity = 1000;
                break;
            case 1:
                baseStorageCapacity = 2500;
                break;
            case 2:
                baseStorageCapacity = 5000;
                break;
            case 3:
                baseStorageCapacity = 7500;
                break;
            case 4:
                baseStorageCapacity = 15000;
                break;
        }

        //Activate when upgraded
        ActivateUpgradedObjects();

        //Update capacity when  upgraded
        transferGoods.baseCapacity = baseStorageCapacity;        
    }

    public int BaseStorageCapacity()
    {
        return baseStorageCapacity;
    }

    public void ActivateUpgradedObjects()
    {
        switch (PlayerPrefs.GetInt("BaseStorageUpgradeLevel"))
        {
            case 1:
                baseStorageUpgradeObject1.SetActive(true);
                break;
            case 2:
                baseStorageUpgradeObject1.SetActive(true);
                baseStorageUpgradeObject2.SetActive(true);
                break;
            case 3:
                baseStorageUpgradeObject1.SetActive(true);
                baseStorageUpgradeObject2.SetActive(true);
                baseStorageUpgradeObject3.SetActive(true);
                break;
            case 4:
                baseStorageUpgradeObject1.SetActive(true);
                baseStorageUpgradeObject2.SetActive(true);
                baseStorageUpgradeObject3.SetActive(true);
                baseStorageUpgradeObject4.SetActive(true);
                break;
        }
    }
}