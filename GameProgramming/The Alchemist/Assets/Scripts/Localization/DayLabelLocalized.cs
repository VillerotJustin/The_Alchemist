using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLabelLocalized : LocalizedText
{
    public override void ReloadText()
    {
        text.text = Locals.GetLocal("Day_"+GameManager.currentDayInWeek)+" "+(GameManager.currentDay+1);
    }
}
