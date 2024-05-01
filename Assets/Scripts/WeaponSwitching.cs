using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
	[SerializeField] private PlayerAimWeapon m_playerAimWeapon;

	public int selectedWeapon = 0;

	void Start()
	{
		SelectWeapon();
		SetActiveWeaponParameters();
	}

	void Update()
	{
		int previousSelectedWeapon = selectedWeapon;

		HandleScrolling();
		HandleWeaponHotkeys();

		if (previousSelectedWeapon != selectedWeapon)
		{
			SelectWeapon();
			SetActiveWeaponParameters();
		}
	}

	private void HandleWeaponHotkeys()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			selectedWeapon = 0;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
		{
			selectedWeapon = 1;
		}
		if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
		{
			selectedWeapon = 2;
		}
		if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
		{
			selectedWeapon = 3;
		}
		if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5)
		{
			selectedWeapon = 4;
		}
	}

	private void HandleScrolling()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0f)
		{
			if (selectedWeapon >= transform.childCount)
			{
				selectedWeapon = 0;
			}
			else
			{
				selectedWeapon++;
			}
		}

		if (Input.GetAxis("Mouse ScrollWheel") < 0f)
		{
			if (selectedWeapon <= 0)
			{
				selectedWeapon = transform.childCount - 1;
			}
			else selectedWeapon--;
		}
	}

	void SetActiveWeaponParameters()
	{
		foreach (Transform weapon in transform)
		{
			if (weapon.gameObject.activeSelf == true && weapon.gameObject.CompareTag("Aim"))
			{
				m_playerAimWeapon.SetActiveAimWeaponTransform(weapon.transform);
			}
			else if (weapon.gameObject.activeSelf == true && weapon.gameObject.CompareTag("Aim") != true)
			{
				m_playerAimWeapon.RangedWeaponNotEquipped();
			}
		}
	}

	private void SelectWeapon()
	{
		int i = 0;

		foreach (Transform weapon in transform)
		{
			weapon.gameObject.SetActive(i == selectedWeapon);
			i++;
		}
	}
}
