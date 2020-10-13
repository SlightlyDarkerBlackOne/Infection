using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed;
    public Text displayText;

    public string textToShow;
	
	// Update is called once per frame
	void Update () {
        displayText.text = textToShow;
        transform.position = new Vector3(transform.position.x, transform.position.y 
			+ (moveSpeed * Time.deltaTime), transform.position.z);
	}
}
