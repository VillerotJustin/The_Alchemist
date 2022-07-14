using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timehandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    void Update()
    {
        GameManager.instance.UpdateTime();
        text.text = GameManager.instance.inGameHour.ToString()+"H";
    }
}
