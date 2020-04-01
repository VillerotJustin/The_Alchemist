using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class easteregg : MonoBehaviour
{
    // Start is called before the first frame update
    public int easterclick = 10;
    public Rigidbody2D rb;
    private int click = 0;
    private void OnMouseDown()
    {
        if (click < easterclick)
        {
            click += 1;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
