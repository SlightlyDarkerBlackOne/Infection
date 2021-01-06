﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    private Transform cameraTarget;

    public float cameraSpeed;

    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    public GameObject weatherEffect;
    private GameObject instantiatedEffect;

    //Defines an instance of the weather effect and changes its position in the FixedUpdate function the same as the camera
    private void Start() {
        cameraTarget = PlayerController.Instance.gameObject.transform;
        //instantiatedEffect = Instantiate(weatherEffect, transform.position, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        if(cameraTarget != null)
        {
            var newPos = Vector2.Lerp(transform.position, cameraTarget.position, 
                Time.deltaTime * cameraSpeed);

            var vect3 = new Vector3(newPos.x, newPos.y, -10f);

            var clampX = Mathf.Clamp(vect3.x, minX, maxX);
            var clampY = Mathf.Clamp(vect3.y, minY, maxY);

            transform.position = new Vector3(clampX, clampY, -10f);

            //instantiatedEffect.transform.position = transform.position;
        }
    }

    /*Changes level borders for the main camera
    Checks if the new values are 0 so we can change only maxY position of the 
    camera on a single level to simplify entering into houses*/
    public void ChangeLevelBorders(float minxNew, float minyNew, float maxxNew, float maxyNew){
            if(minxNew != 0)
                minX = minxNew;
            if(minyNew != 0)
                minY = minyNew;

            if(maxxNew != 0)
                maxX = maxxNew;
            if(maxyNew != 0)
                maxY = maxyNew;
        }
}
