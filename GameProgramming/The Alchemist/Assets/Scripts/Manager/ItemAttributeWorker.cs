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
            case "ANTIDOTE":
                Debug.Log("Vous allez mieux !");
                GameManager.instance.RemoveEffect(attributeName);
                break;
            case "VOMITING":
                Debug.Log("ARGH !");
                GameManager.instance.RemoveEffect(attributeName);
                break;

            case "SPEED":
                routine = GameManager.instance.StartCoroutine(SpeedEffect());
                break;
            case "RESISTANCE":
                routine = GameManager.instance.StartCoroutine(ResistanceEffect());
                break;
            case "REPULSIVE":
                routine = GameManager.instance.StartCoroutine(RepulsiveEffect());
                break;
            case "CONFUSION":
                routine = GameManager.instance.StartCoroutine(ConfusionEffect());
                break;
            case "INVISIBILITY":
                EndAllPostProcessingEffects(attributeName);
                routine = GameManager.instance.StartCoroutine(InvisibilityEffect());
                break;

            case "DRUNK":
                EndAllPostProcessingEffects(attributeName);
                routine = GameManager.instance.StartCoroutine(DrunkEffect());
                break;
            case "NICTALOPY":
                EndAllPostProcessingEffects(attributeName);
                routine = GameManager.instance.StartCoroutine(NightVisionEffect());
                break;
            case "TIREDNESS":
                EndAllPostProcessingEffects(attributeName);
                routine = GameManager.instance.StartCoroutine(TiredEffect());
                break;
            case "BLINDNESS":
                EndAllPostProcessingEffects(attributeName);
                routine = GameManager.instance.StartCoroutine(BlindEffect());
                break;
            case "DRUG":
                EndAllPostProcessingEffects(attributeName);
                routine = GameManager.instance.StartCoroutine(DrugEffect());
                break;
        }
    }

    void EndAllPostProcessingEffects(string expect){
        string[] postProcess = {"DRUNK","NICTALOPY","TIREDNESS","BLINDNESS","DRUG"};
        foreach(string post in postProcess){
            if(post.Equals(expect)) continue;
            GameManager.instance.SetEffectToEnd(post);
        }
    }

    void EndRoutine(bool hasPostProcess){
        if(hasPostProcess && !skip)
            PostProcessingManager.ApplyNoEffect();

        GameManager.instance.RemoveEffect(attributeName);
    }


    public void StopCoroutine(){
        if(routine == null) return;
        GameManager.instance.StopCoroutine(routine);
    }


    IEnumerator InvisibilityEffect(){
        GameManager.playerColor = new Color(1,1,1,0.5f);

        while(currentTime > 0){
            currentTime-=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        GameManager.playerColor = new Color(1,1,1,1);
        EndRoutine(false);
    }

    IEnumerator RepulsiveEffect(){

        while(currentTime > 0){
            currentTime-=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        EndRoutine(false);
    }

    IEnumerator ConfusionEffect(){

        GameManager.invertedControls = -1;

        while(currentTime > 0){
            currentTime-=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        GameManager.invertedControls = 1;
        EndRoutine(false);
    }

    IEnumerator ResistanceEffect(){

        while(currentTime > 0){
            currentTime-=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        EndRoutine(false);
    }


    IEnumerator DrugEffect(){
       PostProcessingManager.ApplyDrugVision();


        while(currentTime > 0){
            currentTime-=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        EndRoutine(true);
    }

    IEnumerator BlindEffect(){
       PostProcessingManager.ApplyBlindVision();


        while(currentTime > 0){
            currentTime-=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        EndRoutine(true);
    }

    IEnumerator DrunkEffect(){
       PostProcessingManager.ApplyDrunkFOV();


        while(currentTime > 0){
            currentTime-=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        EndRoutine(true);
    }


    IEnumerator TiredEffect(){
        PostProcessingManager.ApplyTiredVision();

        float correctSpeed = GameManager.player.speed; 
        GameManager.player.speed = correctSpeed/2;
        

        while(currentTime > 0){
            currentTime-=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        GameManager.player.speed = correctSpeed;
        EndRoutine(true);
    }

    IEnumerator SpeedEffect(){

        float correctSpeed = GameManager.player.speed; 
        GameManager.player.speed = correctSpeed*2;
        

        while(currentTime > 0){
            currentTime-=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        GameManager.player.speed = correctSpeed;
        EndRoutine(false);
    }


    IEnumerator NightVisionEffect(){
       PostProcessingManager.ApplyNightVision();


        while(currentTime > 0){
            currentTime-=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        EndRoutine(true);
    }
}
