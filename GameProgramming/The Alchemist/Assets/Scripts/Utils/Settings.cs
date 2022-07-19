using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    public static int GetQualityLevel(){
        return PlayerPrefs.GetInt("Quality",0);
    }

    public static void SaveQualityLevel(int newLevel){
        PlayerPrefs.SetInt("Quality",newLevel);
        PlayerPrefs.Save();
    }
}
