using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timehandler : MonoBehaviour
{


    void Update()
    {
        GameManager.instance.UpdateTime();
        PlayerInfo.instance.RefreshTime();
    }
}
