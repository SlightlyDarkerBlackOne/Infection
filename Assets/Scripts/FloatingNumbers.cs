﻿using UnityEngine;
using UnityEngine.UI;

public class FloatingNumbers : MonoBehaviour {

    public float moveSpeed;
    public int damageNumber;

    public Text displayNumber;
	
	// Update is called once per frame
	void Update () {
        displayNumber.text = "" + damageNumber;
        transform.position = new Vector3(transform.position.x, transform.position.y 
			+ (moveSpeed * Time.deltaTime), transform.position.z);
	}


}
