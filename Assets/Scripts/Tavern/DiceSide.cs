using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    bool diceLanded;
    public int sideValue;

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "DiceCrashSite")
        {
            diceLanded = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "DiceCrashSite")
        {
            diceLanded = false;
        }
    }

    public bool DiceLanded()
    {
        return diceLanded;
    }
}