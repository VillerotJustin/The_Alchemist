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
    
    public Machines machine = Machines.TEST;

    public enum Machines{
        ALL,
        TEST
    }
}
