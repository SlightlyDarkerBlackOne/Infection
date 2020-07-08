using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject textToShow;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        textToShow.transform.position = Input.mousePosition;
    }

    public void OnPointerEnter(PointerEventData data){
        textToShow.SetActive(true);
    }
    
    public void OnPointerExit(PointerEventData data){
        textToShow.SetActive(false);
    }
}
