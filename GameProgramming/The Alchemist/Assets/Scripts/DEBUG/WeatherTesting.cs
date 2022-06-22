using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherTesting : MonoBehaviour
{
    [SerializeField] private ParticleSystem rainPS;
    [SerializeField] private ParticleSystem snowPS;

    void Start(){
        rainPS.Play();
        ClearPS(snowPS);
    }

    void ClearPS(ParticleSystem ps){
        ps.Stop();
        ps.Clear();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.A)){
            ClearPS(rainPS);
            snowPS.Play();
        }

        if(Input.GetKeyDown(KeyCode.Z)){
            ClearPS(snowPS);
            rainPS.Play();
        }
        if(Input.GetKeyDown(KeyCode.E)){
            ClearPS(snowPS);
            ClearPS(rainPS);
        }
    }
}
