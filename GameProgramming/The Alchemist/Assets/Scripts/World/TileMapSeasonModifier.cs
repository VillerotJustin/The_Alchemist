using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileMapSeasonModifier : MonoBehaviour
{
    private static string[] seasons = {"spring","summer","autumn","winter"};
    [SerializeField] private Tilemap[] maps;

    void Start()
    {
        string season = seasons[GameManager.currentMonth];
        BoundsInt bounds;
        TileBase tile;


        foreach(Tilemap map in maps){
            bounds = map.cellBounds;

            for (int x = bounds.xMin; x < bounds.xMax; x++) {
                for (int y = bounds.yMin; y < bounds.yMax; y++) {
                    tile = map.GetTile(new Vector3Int(x,y,0));
                    if (tile != null) {
                        map.SetTile(new Vector3Int(x,y,0),Resources.Load<TileBase>("Tilesets/"+season+"/"+tile.name));
                    }
                }
            }
        }
    }
}
