using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamestart : MonoBehaviour
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


        SceneManager.LoadScene("Samplescene", LoadSceneMode.Single);
    }
 
  
    // Update is called once per frame
    void Update()
    {
        
    }

}
