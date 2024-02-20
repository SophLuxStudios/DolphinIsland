using UnityEngine;
using System;
using System.Collections.Generic;

public class FlagData : MonoBehaviour
{
    //private fields
    private const int numberOfFlags = 9;
    private const int numberOfOwnedByDefault = 5;

    //public properties
    public Flag[] Flags = new Flag[numberOfFlags];

    //static properties
    public static List<Flag> ownedFlags = new List<Flag>();
    public static List<Flag> notOwnedFlags = new List<Flag>();

    private void Awake()
    {
        SortOwnedFlags();
        OwnedFlagCount();
    }

    [Serializable]
    public struct Flag
    {
        public Material material;

        public bool isOwned;

        public int price;
    }

    public void FlagBought(int index)
    {
        PlayerPrefs.SetInt("IsFlag[" + index + "]Owned", 1);

        //every time a flag is bought re-sort and re-count
        SortOwnedFlags();
        OwnedFlagCount();
    }

    private void OwnedFlagCount()
    {
        //clear lists before re-adding to prevent piling up
        ownedFlags.Clear();
        notOwnedFlags.Clear();

        foreach (Flag flag in Flags)
        {
            if(flag.isOwned)
            {
                ownedFlags.Add(flag);
            }
            else
            {
                notOwnedFlags.Add(flag);
            }
        }
    }

    private void SortOwnedFlags()
    {
        //add defaults to owned indexes
        for(int i = 0; i < numberOfOwnedByDefault; i++)
        {
            Flags[i].isOwned = true;
        }

        for(int i = numberOfOwnedByDefault; i < numberOfFlags; i++)
        {
            if(PlayerPrefs.GetInt("IsFlag[" + i + "]Owned", 0) == 1)
            {
                Flags[i].isOwned = true;
            }
            else if(PlayerPrefs.GetInt("IsFlag[" + i + "]Owned", 0) == 0)
            {
                Flags[i].isOwned = false;
            }
        }
    }
}