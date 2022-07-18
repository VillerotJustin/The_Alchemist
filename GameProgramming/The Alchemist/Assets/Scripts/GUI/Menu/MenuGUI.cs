using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuGUI : MonoBehaviour
{

    [SerializeField] private GameObject root;
    private bool menuOpened;

    [SerializeField] private MenuGUITab[] tabs;

    private int currentTab;

    void Start()
    {
        currentTab = 0;
        menuOpened = false;
        root.SetActive(false);

        foreach(MenuGUITab tab in tabs){
            tab.OnClose();
        }

    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            menuOpened = !menuOpened;
            if(menuOpened) OpenMenu();
            else CloseMenu();
        }
    }

    void OpenMenu(){
        root.SetActive(true);
        PlayerHotBarUI.instance.SetHotBarActive(false);
        tabs[currentTab].OnOpen();
        Time.timeScale = 0;

    }

    void CloseMenu(){      
        Time.timeScale = 1;
        root.SetActive(false);
        PlayerHotBarUI.instance.SetHotBarActive(true);
        tabs[currentTab].OnClose();
    }

    public void ChangeTab(int newTab){
        tabs[currentTab].OnClose();
        currentTab = newTab;
        tabs[currentTab].OnOpen();
    }
}
