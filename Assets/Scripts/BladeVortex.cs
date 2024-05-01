using UnityEngine;

public class BladeVortex : MonoBehaviour
{
	public float rotationSpeed = 50f;

	private Transform m_rotationPoint;
	private Vector3 m_player;

	private void Start()
	{
		m_player = FindObjectOfType<Player>().transform.position;
	}

	private void Update()
	{
		transform.RotateAround(m_player, Vector3.forward, Time.deltaTime * rotationSpeed);
	}
}
