using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortSpriteOrderInScene : MonoBehaviour
{
    private List<SpriteRenderer> sprites;
    private SpriteRenderer[] allSprites;



    void Update(){
        allSprites = FindObjectsOfType<SpriteRenderer>();

        if(allSprites.Length <= 1) return;

        sprites = new List<SpriteRenderer>(allSprites);

        sprites.Sort((o1,o2)=>(
            o2.transform.position.y.CompareTo(o1.transform.position.y)
            ));

        for(int i = 0;i<sprites.Count;i++){
            sprites[i].sortingOrder = i+1;
        }
    }
}
