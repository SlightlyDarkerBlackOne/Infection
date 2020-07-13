using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public enum AllItems{
        HealthPotion,
        ManaPotion,
        SpeedScroll
    }

    public GameObject[] items;

    public void DropItem(AllItems itemEnum, Vector3 position){
        foreach(GameObject item in items){
            if(itemEnum.ToString() == item.gameObject.name.ToString()){
                Instantiate(item, position, Quaternion.identity);
            }
        }
    }
}
