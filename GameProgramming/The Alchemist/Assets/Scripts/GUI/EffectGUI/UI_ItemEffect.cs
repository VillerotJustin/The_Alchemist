using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemEffect : MonoBehaviour
{
    [SerializeField] private Image bgImage;
    [SerializeField] private Image frontImage;
    [SerializeField] private string effectName;

    private UIEffectManager manager;

    private float totalTime;

    private float currnetTime;

    public void Init(string effect, UIEffectManager ui){
        effectName = effect;
        manager = ui;

        totalTime = GameManager.instance.GetTotalTimeOfEffect(effect);
        bgImage.sprite = Resources.Load<Sprite>("Effects/"+effect);
        if(bgImage.sprite == null){
            bgImage.sprite = Resources.Load<Sprite>("Items/Sprites/PLACEHOLDER");
        }
        frontImage.sprite = bgImage.sprite;
    }


    void Update(){
        if(!GameManager.instance.EffectExists(effectName)){
            manager.Remove(effectName);
            return;
        }

        frontImage.fillAmount = GameManager.instance.GetCurrentTimeOfEffect(effectName)/totalTime;
    }
}
