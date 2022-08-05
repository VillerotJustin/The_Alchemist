using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsGUI : MenuGUITab
{
    [SerializeField] private TMP_Dropdown qualityDropdown;
    protected new void Start()
    {
        base.Start();

        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new List<string>(QualitySettings.names));

        qualityDropdown.SetValueWithoutNotify(Settings.GetQualityLevel());
    }

    public void ChangeQualitySettings(){
        QualitySettings.SetQualityLevel(qualityDropdown.value);
        Settings.SaveQualityLevel(qualityDropdown.value);
    }

    public void ChangeLanguage(string newOne){
        GameManager.ChangeLocalization(newOne);
    }

}
