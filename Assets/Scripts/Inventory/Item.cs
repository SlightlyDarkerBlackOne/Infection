using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 0)]
public class Item : ScriptableObject {
    public string itemName;
    public Sprite icon;

    public virtual void Use(){

    }
}

