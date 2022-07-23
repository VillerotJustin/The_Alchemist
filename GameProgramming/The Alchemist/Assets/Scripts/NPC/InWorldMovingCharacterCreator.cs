using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWorldMovingCharacterCreator : MonoBehaviour
{
    [SerializeField] private GameObject prefabNPC;
    void Start()
    {
        string[] names = GameManager.movingCharacters.GetNPCNames();
        foreach(string character in names){
            Instantiate(prefabNPC,Vector3.zero,new Quaternion()).GetComponent<InWorldMovingNPC>().Initialize(character);
        }
    }
}
