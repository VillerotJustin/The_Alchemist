using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string localKey;

    public void SetNewKey(string key){
        localKey = key;
        ReloadText();
    }

    public void ReloadText(){
        text.text = Locals.GetLocal(localKey);
    }


    void Start(){
        ReloadText();
    }

    public TextMeshProUGUI GetText(){
        return text;
    }
}

