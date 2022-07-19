using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public void QuitGame(){
        Application.Quit();
    }

    public void LaunchGame(){
        SceneManager.LoadScene("TEST_AREA");
    }
}
