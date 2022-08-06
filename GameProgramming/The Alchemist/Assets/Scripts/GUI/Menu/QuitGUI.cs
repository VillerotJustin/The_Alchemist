using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGUI : MenuGUITab
{
    public void QuitToDesktop(){
        Application.Quit();
    }

    public void QuitToTitleScreen(){
        GameManager.instance.ToMainMenu();
    }
}
