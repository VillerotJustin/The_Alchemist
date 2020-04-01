using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer sr;
    void Start()
    {
        Physics.queriesHitTriggers = true;

    }
    private void OnMouseDown()
    {
        sr.color = Color.gray;

    }
    private void OnMouseExit()
    {
        sr.color = Color.white;
    }
    void OnMouseUpAsButton()
    {


        Application.Quit();
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
