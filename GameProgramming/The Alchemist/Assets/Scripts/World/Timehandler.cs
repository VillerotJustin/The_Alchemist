using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timehandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private RectTransform compass;

    void Update()
    {
        GameManager.instance.UpdateTime();
        text.text = GameManager.instance.inGameHour.ToString()+"H";
        compass.eulerAngles = new Vector3(0,0,(360f/24f) * GameManager.instance.inGameHour);
    }
}
