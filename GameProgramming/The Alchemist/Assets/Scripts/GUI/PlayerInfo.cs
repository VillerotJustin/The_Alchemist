using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private RectTransform compass;
    [SerializeField] private TextMeshProUGUI textDay;
    [SerializeField] private TextMeshProUGUI textGold;

    public static PlayerInfo instance;

    void Awake(){
        instance = this;
    }

    public void RefreshTime(){
        textTime.text = GameManager.instance.inGameHour.ToString()+"H";
        compass.eulerAngles = new Vector3(0,0,(360f/24f) * GameManager.instance.inGameHour);
    }
}
