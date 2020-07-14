using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Inventory : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();
    
    public GameObject InventoryPanel;

    public static Inventory instance;

    public float itemSlots;

    void Start()
    {
        instance = this;
        updatePanelSlots();
    }

    void Update()
    {
        HealWithHotkey();
    }

    //Heals the player with hotkey 'H' removing last potion in the inventory
    void HealWithHotkey(){
        if(Input.GetKeyDown(KeyCode.H)){
            Transform lastChild;
            Item lastSlotItem = null;
            for (int i = 1; i <= itemSlots; i++)
            {
                lastChild = InventoryPanel.transform.GetChild(InventoryPanel.transform.childCount - i);

                InventorySlotController lastSlot = lastChild.GetComponent<InventorySlotController>();
                lastSlotItem = lastSlot.item;
                if(lastSlotItem != null && lastSlotItem.itemName == "Health Potion"){
                    lastSlot.Use();
                    break;
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.M)){
            Transform lastChild;
            Item lastSlotItem = null;
            for (int i = 1; i <= itemSlots; i++)
            {
                lastChild = InventoryPanel.transform.GetChild(InventoryPanel.transform.childCount - i);

                lastSlotItem = lastChild.GetComponent<InventorySlotController>().item;
                if(lastSlotItem != null && lastSlotItem.itemName == "Mana Potion"){
                    lastSlotItem.Use();
                    break;
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.C)){
            Transform lastChild;
            Item lastSlotItem = null;
            for (int i = 1; i <= itemSlots; i++)
            {
                lastChild = InventoryPanel.transform.GetChild(InventoryPanel.transform.childCount - i);
                
                lastSlotItem = lastChild.GetComponent<InventorySlotController>().item;
                if(lastSlotItem != null && lastSlotItem.itemName == "Speed Scroll"){
                    lastSlotItem.Use();
                    break;
                }
            }
        }

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
        if(InventoryNotFull()){
            itemList.Add(item);
        }
        updatePanelSlots();
    }

    public void Remove(Item item){
        itemList.Remove(item);
        updatePanelSlots();
    }

    public bool InventoryNotFull(){
        if(itemList.Count < itemSlots)
            return true;
        else return false;
    }
}
