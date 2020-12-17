using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class PlayerAimWeapon : MonoBehaviour
{
    private Transform aimTransform;
    private Animator aimAnimator;
    Vector3 aimDirection;
    public GameObject arrowPrefab;
    public Transform endPointPosition;
    private float bowAttackCooldown;
    public float bowAttackTime;
    private float angle;

    private void Awake() {
        aimTransform = transform.Find("Weapons").Find("Aim");
        aimAnimator = aimTransform.GetComponentInChildren<Animator>();
        aimDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        HandleAiming();
        HandleShooting();

        if (bowAttackCooldown >= 0){
            bowAttackCooldown -= Time.deltaTime;
            //Da daje dmg dok napada
            //hitPoint.SetActive(true);
        }
    }

    private void HandleAiming(){
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        
        Vector3 playerCenterPosition = new Vector3(transform.position.x - 0.05f, 
                transform.position.y + 0.3f, transform.position.z);

        aimDirection = (mousePosition - playerCenterPosition).normalized;
        angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0,0,angle);
    }
    private void HandleShooting(){
        if(Input.GetButtonDown("Fire1")){
            aimAnimator.SetBool("Drawing", true);
        } else if (Input.GetButtonUp("Fire1") && bowAttackCooldown > 0){
            aimAnimator.SetBool("Drawing", false);
        }
        if (Input.GetButtonUp("Fire1") && bowAttackCooldown <= 0) {
            aimAnimator.SetTrigger("Shoot");
            aimAnimator.SetBool("Drawing", false);
            bowAttackCooldown = bowAttackTime;

            //SingleArrow();
            FireMultipleArrows(6);
        }
    }
    private void SingleArrow(){
        GameObject arrow = Instantiate(arrowPrefab, endPointPosition.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = aimDirection * 15.0f;
        arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg);
        Destroy(arrow, 2.0f);
    }
    private void FireMultipleArrows(int numOfArrows){
        float offset = 30f;
        for (int i = 0; i < numOfArrows; i++)
        {
            Quaternion newAngle = Quaternion.AngleAxis ((offset * (i - (numOfArrows / 2))), transform.up);
            if(i%2 == 0)
                aimTransform.eulerAngles = new Vector3(0,0,angle+i*offset);
            else
                aimTransform.eulerAngles = new Vector3(0,0,angle-i*offset);
            SingleArrow();
        }
    }
}
