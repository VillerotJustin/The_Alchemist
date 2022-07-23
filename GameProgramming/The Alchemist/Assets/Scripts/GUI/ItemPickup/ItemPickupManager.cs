using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupManager : MonoBehaviour
{
    public static ItemPickupManager instance;

    [SerializeField] private int maxInfos;

    private ItemPickUpSlot[] slots;

    private Coroutine[] slotsCoroutines;

    [SerializeField] private GameObject prefabSlot;

    void Awake()
    {
        instance = this;
        slots = new ItemPickUpSlot[maxInfos];
        slotsCoroutines = new Coroutine[maxInfos];
    }


    public void AddItem(Item item, int number){
        for(int i = 0;i < maxInfos;i++){
            if(slots[i] != null && slots[i].IsItemSameHas(item)){
                slots[i].AddToItem(number);
                StopCoroutine(slotsCoroutines[i]);
                slotsCoroutines[i] = StartCoroutine(WaitUntilDestruction(i));
                return;
            }
        }
        for(int i = 0;i < maxInfos;i++){
            if(slots[i] == null){
                slots[i] = Instantiate(prefabSlot,transform).GetComponent<ItemPickUpSlot>();
                slots[i].Reload(item,number);
                slotsCoroutines[i] = StartCoroutine(WaitUntilDestruction(i));
                return;
            }
        }

    }


    IEnumerator WaitUntilDestruction(int slotIndex){
        yield return new WaitForSeconds(3);
        Destroy(slots[slotIndex].gameObject);
        slots[slotIndex] = null;
        slotsCoroutines[slotIndex] = null;
    }
}
