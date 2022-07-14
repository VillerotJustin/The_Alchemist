using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NightLight : MonoBehaviour
{
    [SerializeField] private Light2D objLight;
    [SerializeField] private int activateAfterHour;

    private Coroutine activateLight;

    [SerializeField] private float activationSpeed;

    void Start(){
        activateLight = null;
        if(GameManager.instance.inGameHour >= activateAfterHour){
            objLight.color = new Color(objLight.color.r,objLight.color.g,objLight.color.r,1);
            enabled = false;
        }else{
            objLight.color = new Color(objLight.color.r,objLight.color.g,objLight.color.r,0);
            enabled = true;
        }

    }

    void Update()
    {
        // Probably a bad idea but hey :)
        if(GameManager.instance.inGameHour >= activateAfterHour){
            StartActivation();
            this.enabled = false;
        }
    }

    void StartActivation(){
        if(activateLight != null){
            StopCoroutine(activateLight);
        }
        activateLight = StartCoroutine(LightActivation());
    }
    
    IEnumerator LightActivation(){
        while(objLight.color.a < 1){
            objLight.color = new Color(objLight.color.r,objLight.color.g,objLight.color.r,Mathf.MoveTowards(objLight.color.a,1,Time.deltaTime*activationSpeed));
            yield return new WaitForEndOfFrame();
        }
        activateLight = null;
    }
}
