using CodeMonkey.Utils;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
	[SerializeField] private PlayerController2D m_playerController2D;
	[SerializeField] private GameObject m_arrowPrefab;
	[SerializeField] private List<Animator> m_bowAnimators;

	private Transform m_aimTransform;
	private Animator m_aimAnimator;
	private Vector3 m_aimDirection;
	private float m_bowAttackCooldown = 0;
	private float m_topDownWeaponAngle;

	public Transform endPointPosition;
	public float bowAttackTime;

	public bool rangedWeaponEquiped;

	private void Awake()
	{
		m_aimTransform = m_bowAnimators[0].transform.parent;
		m_aimAnimator = m_bowAnimators[0];
		m_aimDirection = Vector3.zero;
	}

	// Update is called once per frame
	void Update()
	{
		HandleAiming();
		HandleShooting();
	}

	private void HandleAiming()
	{
		Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

		Vector3 playerCenterPosition = new Vector3(transform.position.x - 0.05f,
				transform.position.y + 0.3f, transform.position.z);

		m_aimDirection = (mousePosition - playerCenterPosition).normalized;
		m_topDownWeaponAngle = Mathf.Atan2(m_aimDirection.y, m_aimDirection.x) * Mathf.Rad2Deg;
		m_aimTransform.eulerAngles = new Vector3(0, 0, m_topDownWeaponAngle);
	}

	private void HandleShooting()
	{
		if (m_playerController2D.PlayerState != PlayerState.Frozen && rangedWeaponEquiped)
		{
			if (Input.GetButtonDown("Fire1"))
			{
				//if (EventSystem.current.IsPointerOverGameObject()) return;
				m_aimAnimator.SetBool("Drawing", true);
				m_aimTransform.GetChild(0).GetChild(0).gameObject.SetActive(true);
			}
			else if (Input.GetButtonUp("Fire1") && m_bowAttackCooldown > 0)
			{
				m_aimAnimator.SetBool("Drawing", false);
				m_aimTransform.GetChild(0).GetChild(0).gameObject.SetActive(false);
			}
			else if (Input.GetButtonUp("Fire1") && m_bowAttackCooldown <= 0)
			{
				//if (EventSystem.current.IsPointerOverGameObject()) return;
				MessagingSystem.Publish(MessageType.BowFired);

				m_aimAnimator.SetTrigger("Shoot");
				m_aimAnimator.SetBool("Drawing", false);
				m_aimTransform.GetChild(0).GetChild(0).gameObject.SetActive(false);
				m_bowAttackCooldown = bowAttackTime;

				SingleArrow();
				//FireMultipleArrows(6);
			}
			m_bowAttackCooldown -= Time.deltaTime;
		}
	}
	private void SingleArrow()
	{
		GameObject arrow = Instantiate(m_arrowPrefab, endPointPosition.position, Quaternion.identity);
		arrow.GetComponent<Rigidbody2D>().velocity = m_aimDirection * 15.0f;
		arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(m_aimDirection.y, m_aimDirection.x) * Mathf.Rad2Deg);
		//Range for now
		Destroy(arrow, 0.3f);
	}
	private void FireMultipleArrows(int numOfArrows)
	{
		float offset = 30f;
		for (int i = 0; i < numOfArrows; i++)
		{
			Quaternion newAngle = Quaternion.AngleAxis((offset * (i - (numOfArrows / 2))), transform.up);
			if (i % 2 == 0)
				m_aimTransform.eulerAngles = new Vector3(0, 0, m_topDownWeaponAngle + i * offset);
			else
				m_aimTransform.eulerAngles = new Vector3(0, 0, m_topDownWeaponAngle - i * offset);
			SingleArrow();
		}
	}

	public void SetActiveAimWeaponTransform(Transform newAimTransform)
	{
		m_aimTransform = newAimTransform;
		m_aimAnimator = m_aimTransform.GetComponentInChildren<Animator>();
		endPointPosition = m_aimTransform.Find("EndPointPosition").transform;
		bowAttackTime = m_aimTransform.Find("Bow").GetComponent<RangedWeapon>().baseAttackTime;

		rangedWeaponEquiped = true;
	}
	public void RangedWeaponNotEquipped()
	{
		rangedWeaponEquiped = false;
	}
}