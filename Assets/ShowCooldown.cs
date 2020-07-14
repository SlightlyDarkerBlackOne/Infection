using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCooldown : MonoBehaviour
{
    public GameObject floatingText;

    public void ShowFloatingText(string itemName){
        var clone = (GameObject)Instantiate(floatingText,
            PlayerController.Instance.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<FloatingText>().textToShow = itemName + " is on Cooldown! " 
            + PlayerController.Instance.moveBonusCooldown;
        clone.transform.position = new Vector2(PlayerController.Instance.transform.position.x,
            PlayerController.Instance.transform.position.y);
    }
}
