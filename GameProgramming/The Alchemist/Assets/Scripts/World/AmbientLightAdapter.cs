using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AmbientLightAdapter : MonoBehaviour
{
    [SerializeField] private Color colorDay;
    [SerializeField] private Color colorNight;
    [SerializeField] private Light2D ambientLight;

    [SerializeField] private int lightFadeAtHour;

    void Update()
    {
        if(GameManager.instance.inGameHour < lightFadeAtHour){
            ambientLight.color = colorDay;
        }else{
            ambientLight.color = Color.Lerp(colorDay,colorNight,(float)(GameManager.instance.inGameHour - lightFadeAtHour)/(24-lightFadeAtHour));
        }
        
    }
}
