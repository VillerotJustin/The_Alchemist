using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessingManager : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] private VolumeProfile drunkProfile;

    private static PostProcessingManager ppm;

    private Coroutine routine;

    void Awake(){
        ppm = this;
        volume.profile = null;
        volume.weight = 0;
    }


    public static void ApplyDrunkFOV(int time){
        ppm.ChangeProfile(ppm.drunkProfile,time);
    } 


    void ChangeProfile(VolumeProfile newProfile,int time){
        if(routine != null){
            StopCoroutine(routine);
        }

        routine = StartCoroutine(ChangingProfile(newProfile,time));
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

        routine = null;
    }
}
