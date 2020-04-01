using System.Collections;
using System.Collections.Generic;

using System.Timers;

using UnityEngine;

public class attack : MonoBehaviour
{
    public Transform potion_transform;
    public Transform player_transform;

    public Rigidbody2D rb;

    public int spawndistance;
    public float speed;
    public float firstphasetime;
    

    private float waittime;
    private Vector3 target;
  
    
    // Start is called before the first frame update
    private float rd_in(float min, float max, float minnp, float maxnp)
    {
        float a = Random.Range(min, max);
        while (a < maxnp && a > minnp)
        {

            a = Random.Range(min, max);
        }
        return a;
      }
     
    void Start()
    {
        waittime = firstphasetime;
        
        potion_transform.position = new Vector3(rd_in(-spawndistance, spawndistance, -5,5), rd_in(-spawndistance, spawndistance, -6, 6 ), 1);
        target = potion_transform.position;
        rb.angularVelocity =  2000;
    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(System.Windows.Forms.InputLanguage.CurrentInputLanguage.LayoutName);
        Application.Quit();
       
       
        
    }
    // Update is called once per frame
    void Update()
    {
        potion_transform.position = Vector2.MoveTowards(potion_transform.position, target, speed * Time.deltaTime);
        if (Vector2.Distance(potion_transform.position,target) < 0.4f)
        {
            if (waittime <= 0)
            {
                speed += 0.2f;
                firstphasetime -= 0.1f;
                rb.angularVelocity = 0; 
                float m = Mathf.Atan((player_transform.position.y - potion_transform.position.y) / (player_transform.position.x - potion_transform.position.x)) * (180 / Mathf.PI) - 90;
                rb.rotation = m;
                target = player_transform.position;
                waittime = firstphasetime;
            }
            else
            {
                rb.angularVelocity = 2000;
                waittime -= Time.deltaTime;
            }
        }

    }
}
