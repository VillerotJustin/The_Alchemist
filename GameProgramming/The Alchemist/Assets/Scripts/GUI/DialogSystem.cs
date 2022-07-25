using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem instance;

    [SerializeField] private GameObject root;
    [SerializeField] private Image characterSprite;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textDialog;

    [SerializeField] private GameObject[] buttons;

    private Coroutine coroutine;

    public bool skipDialog;

    public bool inDialog {get{return coroutine != null;}}

    private int choice;

    private string[] choiceNext;


    void Start()
    {
        choiceNext = new string[3];
        choice = -1;
        skipDialog = false;
        instance = this;    
        HideDialog();
    }

    public void StartDialog(string fileName){
        if(coroutine != null){
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(ProcessingDialog(fileName));
    }

    public void HideDialog(){
        root.SetActive(false);
    }


    public void SetChoice(int choiceMade){
        choice = choiceMade;
    }

    public void SkipCurrentDialog(){
        skipDialog = true;
    }


    IEnumerator ProcessingDialog(string fileName){
        List<string> fileContent = FileManager.ReadTextAsset(Resources.Load<TextAsset>("Dialogs/"+fileName));

        GameManager.instance.playerCanMove = false;

        for(int i = 0;i < fileContent.Count;i++){
            string[] splited = fileContent[i].Replace("\t","").Split("(");
            skipDialog = false;

            if(fileContent[i].Length >= 2){
                splited[1] = splited[1].Split(")")[0];
            }

            switch(splited[0]){
                case "DIALOG":
                    string[] infos = splited[1].Split(",");
                    textName.text = infos[0];
                    textDialog.text = infos[1];
                    textDialog.maxVisibleCharacters = 0;
                    root.SetActive(true);
                    textDialog.gameObject.SetActive(true);
                    foreach(GameObject button in buttons){
                        button.SetActive(false);
                    }

                    while(textDialog.maxVisibleCharacters < infos[1].Length){
                        textDialog.maxVisibleCharacters++;

                        if(skipDialog){
                            textDialog.maxVisibleCharacters = infos[1].Length;
                        }
                        yield return new WaitForEndOfFrame();
                    }


                    while(!skipDialog){
                        yield return new WaitForEndOfFrame();
                    }

                    break;
                case "MUGSHOT":
                    characterSprite.sprite = Resources.Load<Sprite>("Characters/Mugshots/"+splited[1]);
                    break;
                case "WAIT":
                    yield return new WaitForSeconds(float.Parse(splited[1],System.Globalization.CultureInfo.InvariantCulture));
                    break;
                case "HIDE":
                    HideDialog();
                    break;
                case "SHOW":
                    root.SetActive(true);
                    break;
                case "{":
                    textDialog.gameObject.SetActive(false);
                    foreach(GameObject button in buttons){
                        button.SetActive(false);
                    }

                    int currentBox = 0;
                    i++;
                    while(currentBox <= 3 && i < fileContent.Count && fileContent[i] != "}"){
                        string[] choice = fileContent[i].Replace("\t","").Split("(");
                        if(choice[0] == "CHOICE" && choice.Length > 1){
                            choice[1] = choice[1].Split(")")[0];
                            string[] splitedLine = choice[1].Split(",");
                            choiceNext[currentBox] = splitedLine[1];
                            buttons[currentBox].SetActive(true);
                            buttons[currentBox].GetComponentInChildren<TextMeshProUGUI>().text = splitedLine[0];
                            currentBox++;
                        }
                        i++;
                    }

                    choice = -1;
                    while(choice == -1){
                        yield return new WaitForEndOfFrame();
                    }

                    StartDialog(choiceNext[choice]);
                    yield break;
            }

            yield return new WaitForEndOfFrame();
        }

        HideDialog();
        GameManager.instance.playerCanMove = true;
        coroutine = null;
    }

}
