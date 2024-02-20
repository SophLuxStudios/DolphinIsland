using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipStorage : MonoBehaviour
{
    //Used Scripts
    private TransferGoods transferGoods;

    [HideInInspector]
    public int shipStorageCapacity;

    [SerializeField] private GameObject capacityReachedSign;

    void Awake()
    {
        transferGoods = GameObject.FindObjectOfType<TransferGoods>();

        SetShipStorageCapacity();

        CapacityCheck();
    }

    public void SetShipStorageCapacity()
    {
        switch (PlayerPrefs.GetInt("ShipStorageUpgradeLevel"))
        {
            case 0:
                shipStorageCapacity = 100;
                break;
            case 1:
                shipStorageCapacity = 250;
                break;
            case 2:
                shipStorageCapacity = 500;
                break;
            case 3:
                shipStorageCapacity = 750;
                break;
            case 4:
                shipStorageCapacity = 1500;
                break;
        }

        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName("StoryBase"))
        {
            transferGoods.shipCapacity = shipStorageCapacity;
        }
    }

    public int ShipStorageCapacity()
    {
        return shipStorageCapacity;
    }

    private void CapacityCheck()
    {
        if(PlayerPrefs.GetInt("CollectedWood") + PlayerPrefs.GetInt("CollectedIron") +
           PlayerPrefs.GetInt("FetchQuestQuantityCarried") < shipStorageCapacity)
        {
            PlayerPrefs.SetInt("ShipCapacityReached", 0);
            CapacityReachedSignActivation(false);
        }
        else
        {
            PlayerPrefs.SetInt("ShipCapacityReached", 1);
            CapacityReachedSignActivation(true);
        }
    }

    public void CapacityReachedSignActivation(bool activation)
    {
        if(capacityReachedSign != null)
        {
            capacityReachedSign.SetActive(activation);
        }
    }
}