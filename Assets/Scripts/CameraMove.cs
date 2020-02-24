using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public Transform cameraTarget;

    public float cameraSpeed;

    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    public GameObject weatherEffect;
    private GameObject instantiatedEffect;

    //Defines an instance of the weather effect and changes its position in the FixedUpdate function the same as the camera
    private void Start() {
        instantiatedEffect = Instantiate(weatherEffect, transform.position, Quaternion.identity);
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

            instantiatedEffect.transform.position = transform.position;
        }
    }

    public void ChangeLevelBorders(float minx, float miny, float maxx, float maxy){
            minX = minx;
            minY = miny;
            maxX = maxx;
            maxY = maxy;
        }
}
