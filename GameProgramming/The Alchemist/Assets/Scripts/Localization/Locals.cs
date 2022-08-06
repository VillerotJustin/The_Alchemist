using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locals
{
    private static Locals self;
    private string currentLanguage;

    private Dictionary<string,string> locals;

    public static void Init(){
        new Locals();
    }

    public Locals(){
        self = this;
        locals = new Dictionary<string, string>();
        LoadLanguage("fra");
    }

    public static void ChangeLanguage(string newOne){
        self.LoadLanguage(newOne);
    }

    public static string GetLocal(string key){
        return self.GetValue(key);
    }

    string GetValue(string key){
        if(locals.ContainsKey(key)) return locals[key];
        return "LOCAL_ERROR";
    }

    void LoadLanguage(string newLanguage){
        if(newLanguage.Equals(currentLanguage)) return;
        
        currentLanguage = newLanguage;
        locals.Clear();
        LoadContent(newLanguage+"_items");
        LoadContent(newLanguage+"_dialogs");
        LoadContent(newLanguage+"_system");
    }


    void LoadContent(string fileName){
        List<string> fileContent = FileManager.ReadTextAsset(Resources.Load<TextAsset>("Locals/"+fileName));
        string line;
        string[] split;

        for(int i = 0;i < fileContent.Count;i++){
            line = fileContent[i];
            if(line.StartsWith("#") || string.IsNullOrWhiteSpace(line)) continue;

            split = line.Split(" = ");

            if(split.Length != 2){
                Debug.Log("Error on line "+line+". There should be only one = .");
                continue;
            }

            if(split[0].EndsWith(" ")){
                split[0] = split[0].Substring(0,split[0].Length-1);
            }
            if(split[1].EndsWith(" ")){
                split[1] = split[1].Substring(0,split[1].Length-1);
            }
            locals.Add(split[0],split[1]);
        }
    }
}
