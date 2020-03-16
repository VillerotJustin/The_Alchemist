using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform campos;
    public Transform ppos;
    Vector3 playpos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playpos = ppos.position;
        playpos.z = -10f;
    
    }

    void FixedUpdate()
    {
        campos.position = playpos;
    }
}
