using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCooldown : MonoBehaviour
{
    public GameObject floatingText;

    public void ShowFloatingText(string itemName, Color color){
        var clone = (GameObject)Instantiate(floatingText,
            PlayerController2D.Instance.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<FloatingText>().displayText.color = color;
        clone.GetComponent<FloatingText>().textToShow = itemName + " is on Cooldown! " 
            + PlayerController2D.Instance.MoveBonusCooldown;
        clone.transform.position = new Vector2(PlayerController2D.Instance.transform.position.x,
            PlayerController2D.Instance.transform.position.y);
    }
}
