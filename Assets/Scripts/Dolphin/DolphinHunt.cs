using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinHunt : MonoBehaviour
{
    private ShipMovement shipMovement;
    private DolphinMovement dolphinMovement;
    private Harpoon harpoon;

    [SerializeField] private int harpoonsLeft;

    [SerializeField] private GameObject harpoonPrefab;

    [SerializeField] private Transform ship;
    [SerializeField] private Transform harpoonSpawnPosition;

    [SerializeField] private GameObject throwHarpoonPanel;

    void Start()
    {
        shipMovement = GameObject.FindObjectOfType<ShipMovement>();

        harpoonsLeft = PlayerPrefs.GetInt("HarpoonAmountUpgradeLevel");
    }

    public void SpawnHarpoon()
    {
        dolphinMovement = GameObject.FindObjectOfType<DolphinMovement>();

        GameObject temp = Instantiate(harpoonPrefab, harpoonSpawnPosition.position, Quaternion.Euler (93f, 0f, 0f), ship);
        harpoon = temp.GetComponent<Harpoon>();

        throwHarpoonPanel.SetActive(true);
    }

    public void ThrowHarpoon()
    {
        throwHarpoonPanel.SetActive(false);

        harpoon.Throw();
    }

    public void HarpoonMissed()
    {
        harpoonsLeft--;

        if(harpoonsLeft > 0)
        {
            SpawnHarpoon();
        }
        else
        {
            dolphinMovement.DolphinGotAway();
            shipMovement.isHuntEnded = true;
        }
    }

    public void HarpoonHitDolphin()
    {
        dolphinMovement.DolphinIsHit();
        shipMovement.isHuntEnded = true;

        PlayerPrefs.SetInt("OwnedFin", PlayerPrefs.GetInt("OwnedFin", 0) + 1);
    }

    private void CheckHuntQuest()
    {
        if(PlayerPrefs.GetInt("HuntQuestActive", 0) == 1)
        {
            PlayerPrefs.SetInt("HuntQuestQuantity", (PlayerPrefs.GetInt("HuntQuestQuantity") - 1));

            if(PlayerPrefs.GetInt("HuntQuestQuantity") == 0)
            {
                PlayerPrefs.SetInt("HuntQuestActive", 0);

                PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + PlayerPrefs.GetInt("HuntQuestReward"));
            }
        }
    }
}