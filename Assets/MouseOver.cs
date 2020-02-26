using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseOver : MonoBehaviour
{
    //When the mouse hovers over the GameObject, it turns to this color (red)
    public Color m_MouseOverColor;
    public GameObject textToShow;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        textToShow.GetComponent<Text>().text = this.GetComponent<Image>().fillAmount.ToString();
        textToShow.transform.position = Input.mousePosition;
    }

    void OnMouseOver()
    {
        textToShow.SetActive(true);
        Debug.Log("Mouse OVER");

    }

    void OnMouseExit()
    {
        textToShow.SetActive(false);
    }
}
