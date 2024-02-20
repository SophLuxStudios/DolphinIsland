using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrot : MonoBehaviour
{
    [SerializeField] GameObject parrot;
    void Awake()
    {
        if(PlayerPrefs.GetInt("ParrotOwned", 0) == 1)
        {
            parrot.SetActive(true);
        }
    }
}