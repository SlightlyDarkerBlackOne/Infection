using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class PlayerAimWeapon : MonoBehaviour
{
    private Transform aimTransform;
    private Animator aimAnimator;

    private void Awake() {
        aimTransform = transform.Find("Aim");
        aimAnimator = aimTransform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleAiming();
        HandleShooting();
    }

    private void HandleAiming(){
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        
        Vector3 playerCenterPosition = new Vector3(transform.position.x - 0.05f, 
                transform.position.y + 0.3f, transform.position.z);

        Vector3 aimDirection = (mousePosition - playerCenterPosition).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0,0,angle);
    }
    private void HandleShooting(){
        if (Input.GetButtonUp("Fire1")) {
            //aimAnimator.SetTrigger("Shoot");
        }
    }
}
