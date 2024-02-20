using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipVariation : MonoBehaviour
{
    [SerializeField] GameObject[] shipVariations = new GameObject[3];

    void Awake()
    {
        GameObject boat = Instantiate(shipVariations[PlayerPrefs.GetInt("ShipVariationIndex", 0)], transform.position, transform.rotation, transform);
        boat.transform.SetAsFirstSibling();

        if(SceneManager.GetActiveScene().name == "Menu")
        {
            if(PlayerPrefs.GetInt("ShipVariationIndex", 0) == 0)
            {
                boat.GetComponent<BoxCollider>().enabled = false;
            }
            else if(PlayerPrefs.GetInt("ShipVariationIndex", 0) == 1 || PlayerPrefs.GetInt("ShipVariationIndex", 0) == 2)
            {
                boat.GetComponent<CapsuleCollider>().enabled = false;
            }
        }

        if(PlayerPrefs.GetInt("ShipVariationIndex", 0) == 2)
        {
            boat.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            boat.transform.Find("Paddle").rotation = Quaternion.Euler(-90, 0, 180);
        }
    }
}