using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortSpriteOrderInScene : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> sprites;


    void Update(){
        sprites.Sort((o1,o2)=>(o2.transform.position.y.CompareTo(o1.transform.position.y)));

        for(int i = 0;i<sprites.Count;i++){
            sprites[i].sortingOrder = i+1;
        }
    }
}
