using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeVortex : MonoBehaviour
{
    public float rotationSpeed = 50f;

    private Transform rotationPoint;
    private Vector3 player;

    void Update()
    {
        player = PlayerController.Instance.gameObject.transform.position;
        transform.RotateAround(player, Vector3.forward, Time.deltaTime * rotationSpeed);
    }
}
