using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public void QuitGame(){
        Application.Quit();
    }

    public void LaunchGame(bool debug){
        GameManager.instance.NewGame(debug);
    }


    public void ChangeLocalization(string newLoc){
        GameManager.ChangeLocalization(newLoc);
    }
}
