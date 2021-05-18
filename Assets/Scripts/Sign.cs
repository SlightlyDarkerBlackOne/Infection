using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public GameObject burstEffect;
    public bool broken = false;

    public void Break() {
        if (!broken) {
            GameObject burst = Instantiate(burstEffect, transform.position, transform.rotation);
            SFXManager.Instance.PlaySound(SFXManager.Instance.breakCrate);
            GetComponent<Animator>().SetTrigger("break");

            broken = true;
            Destroy(burst, 2f);
        } 
    }
}
