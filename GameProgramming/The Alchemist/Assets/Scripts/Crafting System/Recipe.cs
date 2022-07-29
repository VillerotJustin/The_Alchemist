using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public string item1Name;
    public int item1Count;

    public string item2Name;
    public int item2Count;

    public string resultName;
    public int resultCount;

    public bool discovered = false;
    
    public Machines machine = Machines.CAULDRON;

    public enum Machines{
        ALL,
        CAULDRON,
        ALAMBIC
    }


    public Recipe(){
        item1Name = "";
        item1Count = 0;
        item2Count = 0;
        item2Name = "";
        resultCount = 0;
        resultName = "";
        discovered = false;
        machine = Machines.CAULDRON;
    }
}
