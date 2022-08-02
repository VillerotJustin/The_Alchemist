using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttributeWorker
{
    private Coroutine routine;
    private float totalTime;
    private float currentTime;
    private string attributeName;

    private bool skip;

    public float GetCurrentTime(){return currentTime;}
    public float GetTotalTime(){return totalTime;}


    public void End(){
        currentTime = 0;
        skip = true;
    }

    public ItemAttributeWorker(string attribute, int time){
        totalTime = time;
        currentTime = time;
        attributeName = attribute;
        skip = false;
    }

    public void Init(){
        switch(attributeName){
            case "HEALING":
                Debug.Log("Vous allez mieux. Enfin, je crois...");
                GameManager.instance.RemoveEffect(attributeName);
                break;
            case "ENERGY":
                Debug.Log("Vous êtes SPEED maintenant !");
                GameManager.instance.RemoveEffect(attributeName);
                break;

            case "DRUNK":
                GameManager.instance.SetEffectToEnd("NICTALOPY");
                GameManager.instance.SetEffectToEnd("TIREDNESS");
                routine = GameManager.instance.StartCoroutine(DrunkEffect());
                break;
            case "SPEED":
                routine = GameManager.instance.StartCoroutine(SpeedEffect());
                break;
            case "NICTALOPY":
                GameManager.instance.SetEffectToEnd("DRUNK");
                GameManager.instance.SetEffectToEnd("TIREDNESS");
                routine = GameManager.instance.StartCoroutine(NightVisionEffect());
                break;
            case "TIREDNESS":
                GameManager.instance.SetEffectToEnd("NICTALOPY");
                GameManager.instance.SetEffectToEnd("DRUNK");
                routine = GameManager.instance.StartCoroutine(TiredEffect());
                break;
        }
    }


    public void StopCoroutine(){
        if(routine == null) return;
        GameManager.instance.StopCoroutine(routine);
    }


    IEnumerator DrunkEffect(){
       PostProcessingManager.ApplyDrunkFOV();


        while(currentTime > 0){
            currentTime-=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if(!skip)
            PostProcessingManager.ApplyNoEffect();

        GameManager.instance.RemoveEffect(attributeName);
    }


    IEnumerator TiredEffect(){
        PostProcessingManager.ApplyTiredVision();

        float correctSpeed = GameManager.player.speed; 
        GameManager.player.speed = correctSpeed/2;
        

        while(currentTime > 0){
            currentTime-=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if(!skip)
            PostProcessingManager.ApplyNoEffect();

        GameManager.player.speed = correctSpeed;
        GameManager.instance.RemoveEffect(attributeName);
    }

    IEnumerator SpeedEffect(){

        float correctSpeed = GameManager.player.speed; 
        GameManager.player.speed = correctSpeed*2;
        

        while(currentTime > 0){
            currentTime-=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if(!skip)
            PostProcessingManager.ApplyNoEffect();

        GameManager.player.speed = correctSpeed;
        GameManager.instance.RemoveEffect(attributeName);
    }


    IEnumerator NightVisionEffect(){
       PostProcessingManager.ApplyNightVision();


        while(currentTime > 0){
            currentTime-=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if(!skip)
            PostProcessingManager.ApplyNoEffect();

        GameManager.instance.RemoveEffect(attributeName);
    }
}
