using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_script : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform cam;
    public Transform Player;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        cam.position = new Vector3(Player.position.x, Player.position.y, cam.position.z);
    }
}
