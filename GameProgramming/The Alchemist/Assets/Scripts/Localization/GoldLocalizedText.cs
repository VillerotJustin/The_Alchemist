using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldLocalizedText : LocalizedText
{
    public override void ReloadText()
    {
        text.text = GameManager.player.gold+" "+ Locals.GetLocal(localKey);
    }
}
