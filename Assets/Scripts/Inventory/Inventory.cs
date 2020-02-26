using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Inventory : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();

    public GameObject player;
    public GameObject InventoryPanel;

    public static Inventory instance;

    public float itemSlots;

    void Start()
    {
        instance = this;
        updatePanelSlots();
    }

    void updatePanelSlots(){
        int index = 0;
        foreach(Transform child in InventoryPanel.transform){
            
            InventorySlotController slot = child.GetComponent<InventorySlotController>();

            if(index < itemList.Count){
                slot.item = itemList[index];
            } else{
                slot.item = null;
            }

            slot.updateInfo();
            index++;
        }
    }

    public void Add(Item item){
        if(itemList.Count < itemSlots){
            itemList.Add(item);
        }
        updatePanelSlots();
    }

    public void Remove(Item item){
        itemList.Remove(item);
        updatePanelSlots();
    }
}
