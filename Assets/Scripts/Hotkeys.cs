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
            GameObject skillTreePanel = SkillTree.instance.skillTreePanel;

            if (!skillTreePanel.activeSelf){
                skillTreePanel.SetActive(true);
            } else {
                skillTreePanel.SetActive(false);
            }
        }
        //CharacterScreen
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameObject characterPanel = CharacterScreenUI.instance.characterScreenPanel;

            if (!characterPanel.activeSelf)
            {
                characterPanel.SetActive(true);
            }
            else
            {
                characterPanel.SetActive(false);
            }
        }
    }
}
