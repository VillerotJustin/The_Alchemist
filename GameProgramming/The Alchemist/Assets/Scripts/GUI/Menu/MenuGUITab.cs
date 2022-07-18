using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuGUITab : MonoBehaviour
{

    [SerializeField] protected GameObject tabRoot; 
    [SerializeField] protected Button tabButton;

    protected void Start(){
        OnClose();
    }

    public virtual void OnOpen(){
        tabRoot.SetActive(true);
    }

    public virtual void OnClose(){
        tabRoot.SetActive(false);
    }
}
