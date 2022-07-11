using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    [SerializeField] private ParticleSystem rainPS;
    [SerializeField] private ParticleSystem snowPS;

    void Start(){
        ClearPS(rainPS);
        ClearPS(snowPS);
    }

    void ClearPS(ParticleSystem ps){
        ps.Stop();
        ps.Clear();
    }

    void Update(){

        transform.position = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,transform.position.z);


        if(Input.GetKeyDown(KeyCode.Keypad1)){
            ClearPS(rainPS);
            snowPS.Play();
        }

        if(Input.GetKeyDown(KeyCode.Keypad2)){
            ClearPS(snowPS);
            rainPS.Play();
        }
        if(Input.GetKeyDown(KeyCode.Keypad3)){
            ClearPS(snowPS);
            ClearPS(rainPS);
        }
    }
}
