using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MovingCharacter
{
    [SerializeField] private string _charName;
    private Coroutine _coroutine;
    private Vector3 _placement;
    private string _map;

    private int _faceX;

    private int _faceY;

    private bool _moving;

    public bool moving {get{return _moving;} set{_moving = value;}}

    public int faceX {get{return _faceX;} set{_faceX = value;}}
    public int faceY {get{return _faceY;} set{_faceY = value;}}
    public Coroutine coroutine {get{return _coroutine;} set{_coroutine = value;}}
    public Vector3 placement {get{return _placement;} set{_placement = value;}}

    public string charName {get{return _charName;} set{_charName = value;}}
    public string map {get{return _map;} set{_map = value;}}
}
