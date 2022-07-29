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


    public static void ApplyDrunkFOV(){
        ppm.ChangeProfile(ppm.drunkProfile);
    } 


    void ChangeProfile(VolumeProfile newProfile){
        if(routine != null){
            StopCoroutine(routine);
        }

        routine = StartCoroutine(ChangingProfile(newProfile));
    }


    IEnumerator ChangingProfile(VolumeProfile profile){
        volume.weight = 0;
        volume.profile = profile;

        while(volume.weight < 1f){
            volume.weight += Time.deltaTime;
            if(volume.weight > 1f){
                volume.weight = 1f;
            }
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(20);

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
