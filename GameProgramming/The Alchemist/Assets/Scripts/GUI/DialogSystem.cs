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

    private Coroutine coroutine;

    public bool skipDialog;

    public bool inDialog {get{return coroutine != null;}}

    void Start()
    {
        skipDialog = false;
        instance = this;    
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

    IEnumerator ProcessingDialog(string fileName){
        List<string> fileContent = FileManager.ReadTextAsset(Resources.Load<TextAsset>("Dialogs/"+fileName));

        GameManager.instance.playerCanMove = false;

        foreach(string line in fileContent){
            string[] splited = line.Replace("\t","").Split("(");
            skipDialog = false;

            if(line.Length >= 2){
                splited[1] = splited[1].Split(")")[0];
            }

            switch(splited[0]){
                case "DIALOG":
                    string[] infos = splited[1].Split(",");
                    textName.text = infos[0];
                    textDialog.text = infos[1];
                    textDialog.maxVisibleCharacters = 0;
                    root.SetActive(true);

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
            }

            yield return new WaitForEndOfFrame();
        }

        HideDialog();
        GameManager.instance.playerCanMove = true;
        coroutine = null;
    }

}
