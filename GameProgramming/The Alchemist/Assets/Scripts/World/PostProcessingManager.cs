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
    [SerializeField] private VolumeProfile blindProfile;
    [SerializeField] private VolumeProfile drugProfile;

    private static PostProcessingManager ppm;

    private Coroutine routine;


    public static bool isApplyingEffect {get{return ppm.routine != null;}}

    void Awake(){
        if(ppm != null){
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        ppm = this;
        volume.profile = null;
        volume.weight = 0;
    }


    public static void ApplyDrugVision(){
        ppm.ChangeProfile(ppm.drugProfile);
    } 

    public static void ApplyDrunkFOV(){
        ppm.ChangeProfile(ppm.drunkProfile);
    } 

    public static void ApplyNightVision(){
        ppm.ChangeProfile(ppm.nightVisionProfile);
    } 

    public static void ApplyTiredVision(){
        ppm.ChangeProfile(ppm.tiredVisionProfile);
    } 

    public static void ApplyBlindVision(){
        ppm.ChangeProfile(ppm.blindProfile);
    } 

    public static void ApplyNoEffect(){
        ppm.ChangeProfile(null);
    }

    void ChangeProfile(VolumeProfile newProfile){
        if(newProfile == volume.profile) return;

        if(routine == null && volume.profile == null){
            routine = StartCoroutine(StartingProfile(newProfile));
        }else{
            if(routine != null){
                StopCoroutine(routine);
            }

            routine = StartCoroutine(StopProfile(newProfile));
        }
    }

    IEnumerator StopProfile(VolumeProfile newOne){
        while(volume.weight > 0f){
            volume.weight -= Time.deltaTime;
            if(volume.weight < 0f){
                volume.weight = 0f;
            }
            yield return new WaitForEndOfFrame();
        }
        routine = StartCoroutine(StartingProfile(newOne));
    }

    IEnumerator StartingProfile(VolumeProfile profile){
        volume.weight = 0;
        volume.profile = profile;

        while(volume.weight < 1f){
            volume.weight += Time.deltaTime;
            if(volume.weight > 1f){
                volume.weight = 1f;
            }
            yield return new WaitForEndOfFrame();
        }

        routine = null;
    }
}
