using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEffectManager : MonoBehaviour
{
    [SerializeField] private Transform effectsRoot;

    [SerializeField] private GameObject effectPrefab;


    private Dictionary<string,UI_ItemEffect> effects;

    public static UIEffectManager instance;

    void Awake()
    {
        instance = this;
        effects = new Dictionary<string, UI_ItemEffect>();
    }

    public void Remove(string effect){
        if(effects.ContainsKey(effect)){
            Destroy(effects[effect].gameObject);
            effects.Remove(effect);
        }
    }

    public void RefreshAllEffects(){
        effects.Clear();

        foreach(Transform child in effectsRoot){
            Destroy(child.gameObject);
        }

        foreach(string effect in GameManager.instance.effectsName){
            effects[effect] = Instantiate(effectPrefab,effectsRoot).GetComponent<UI_ItemEffect>();
            effects[effect].Init(effect,this);
        }
    }
}
