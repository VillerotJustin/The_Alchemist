using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI text;
    [SerializeField] protected string localKey;

    public void SetNewKey(string key){
        localKey = key;
        ReloadText();
    }

    public virtual void ReloadText(){
        text.text = Locals.GetLocal(localKey);
    }


    protected void Start(){
        ReloadText();
    }

    public TextMeshProUGUI GetText(){
        return text;
    }
}

