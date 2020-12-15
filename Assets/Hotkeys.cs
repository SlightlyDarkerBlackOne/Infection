using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotkeys : MonoBehaviour
{
    void Update()
    {
        Menu();
    }
    void Menu(){
        //Inventory
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
        {
            GameObject inventoryPanel = Inventory.instance.InventoryPanel;

            if (!inventoryPanel.activeSelf){
                inventoryPanel.SetActive(true);
            } else {
                inventoryPanel.SetActive(false);
            }
        }
        //Skilltree
        if(Input.GetKeyDown(KeyCode.J)){
            GameObject skillTreePanel = SkillTree.instance.gameObject;

            if (!skillTreePanel.activeSelf){
                skillTreePanel.SetActive(true);
            } else {
                skillTreePanel.SetActive(false);
            }
        }
    }
}
