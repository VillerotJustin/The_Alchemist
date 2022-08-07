using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DayNightCycle
{
    [SerializeField] private float hourTimeInSeconds;
    [SerializeField] public int currentHour;

    private float currentHourDepletion;

    [SerializeField] public bool canUpdateCycle;

    void Start()
    {
        canUpdateCycle = true;
        currentHour = 6;
        currentHourDepletion = hourTimeInSeconds;
    }

    public void ResetTime(){
        Start();
    }

    public void UpdateTime(){
        if(!canUpdateCycle) return;

        currentHourDepletion-=Time.deltaTime;
        if(currentHourDepletion <= 0){
            currentHourDepletion = hourTimeInSeconds;
            currentHour++;
            if(currentHour == 24){
                canUpdateCycle = false;
            }
        }
    }

}
