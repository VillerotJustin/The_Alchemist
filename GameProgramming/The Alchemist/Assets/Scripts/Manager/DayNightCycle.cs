using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DayNightCycle
{
    [SerializeField] private int hourTimeInSeconds;
    [HideInInspector] public int currentHour;

    private Coroutine cycle;

    void Start()
    {
        currentHour = 6;
        RestartCycle();
    }

    IEnumerator CycleFunction(){
        while(currentHour <= 24){
            yield return new WaitForSeconds(hourTimeInSeconds);
            currentHour++;
        }
        cycle = null;
    }

    public void StopCycle(){
        if(cycle != null) GameManager.instance.StopCoroutine(cycle);
    }

    public void RestartCycle(){
        StopCycle();
        cycle = GameManager.instance.StartCoroutine(CycleFunction());
    }
}
