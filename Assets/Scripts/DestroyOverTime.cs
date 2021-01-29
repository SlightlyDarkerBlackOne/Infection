using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float timeToDestroy = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this, timeToDestroy);
    }
}
