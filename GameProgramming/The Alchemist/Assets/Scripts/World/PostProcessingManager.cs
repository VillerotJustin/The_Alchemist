using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessingManager : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] private VolumeProfile drunkProfile;
    [SerializeField] private VolumeProfile nightVisionProfile;
    [SerializeField] private VolumeProfile tiredVisionProfile;

    private static PostProcessingManager ppm;

    private Coroutine routine;


    public static bool isApplyingEffect {get{return ppm.routine != null;}}

    void Awake(){
        ppm = this;
        volume.profile = null;
        volume.weight = 0;
    }


    public static void ApplyDrunkFOV(int time){
        ppm.ChangeProfile(ppm.drunkProfile,time);
    } 

    public static void ApplyNightVision(int time){
        ppm.ChangeProfile(ppm.nightVisionProfile,time);
    } 

    public static void ApplyTiredVision(int time){
        ppm.ChangeProfile(ppm.tiredVisionProfile,time);
    } 

    void ChangeProfile(VolumeProfile newProfile,int time){
        if(newProfile == volume.profile) return;

        if(routine != null){
            StopCoroutine(routine);
            routine = StartCoroutine(StopProfile(newProfile,time));
        }else{
            routine = StartCoroutine(ChangingProfile(newProfile,time));
        }
    }

    IEnumerator StopProfile(VolumeProfile newOne,int newTime){
        while(volume.weight > 0f){
            volume.weight -= Time.deltaTime;
            if(volume.weight < 0f){
                volume.weight = 0f;
            }
            yield return new WaitForEndOfFrame();
        }
        routine = StartCoroutine(ChangingProfile(newOne,newTime));
    }

    IEnumerator ChangingProfile(VolumeProfile profile,int time){
        volume.weight = 0;
        volume.profile = profile;

        while(volume.weight < 1f){
            volume.weight += Time.deltaTime;
            if(volume.weight > 1f){
                volume.weight = 1f;
            }
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(time);

        while(volume.weight > 0f){
            volume.weight -= Time.deltaTime;
            if(volume.weight < 0f){
                volume.weight = 0f;
            }
            yield return new WaitForEndOfFrame();
        }

        volume.profile = null;
        routine = null;
    }
}
