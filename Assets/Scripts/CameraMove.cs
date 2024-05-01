using UnityEngine;

public class CameraMove : MessagingBehaviour
{
	[SerializeField] private float m_cameraSpeed = 5;
	[SerializeField] private GameObject m_weatherEffect;
	[SerializeField] private Transform m_cameraTarget;

	private float m_minX = 8.4f;
	private float m_minY = -57.5f;
	private float m_maxX = 47.5f;
	private float m_maxY = -24f;

	private bool m_isClampingCamera = true;

	private void Start()
	{
		Subscribe(MessageType.RestrictCamera, OnRestrictCamera);
		Subscribe(MessageType.UnRestrictCamera, OnUnRestrictCamera);
		Subscribe(MessageType.TeleportPlayer, OnTeleportPlayer);
	}

	private void OnTeleportPlayer(object obj)
	{
		if (m_cameraTarget != null)
		{
			transform.position = m_cameraTarget.position;
		}
	}

	private void OnUnRestrictCamera(object obj)
	{
		m_isClampingCamera = false;
	}

	private void OnRestrictCamera(object obj)
	{
		m_isClampingCamera = true;
	}

	private void FixedUpdate()
	{
		if (m_cameraTarget != null)
		{
			var newPos = Vector2.Lerp(transform.position, m_cameraTarget.position,
				Time.deltaTime * m_cameraSpeed);

			var newPositionVector = new Vector3(newPos.x, newPos.y, -10f);

			if (m_isClampingCamera)
			{
				var clampX = Mathf.Clamp(newPositionVector.x, m_minX, m_maxX);
				var clampY = Mathf.Clamp(newPositionVector.y, m_minY, m_maxY);
				transform.position = new Vector3(clampX, clampY, -10f);
			}
			else
			{
				transform.position = newPositionVector;
			}
		}
	}

	/*Changes level borders for the main camera
    Checks if the new values are 0 so we can change only maxY position of the 
    camera on a single level to simplify entering into houses*/
	public void ChangeLevelBorders(float minxNew, float minyNew, float maxxNew, float maxyNew)
	{
		if (minxNew != 0)
			m_minX = minxNew;
		if (minyNew != 0)
			m_minY = minyNew;
		if (maxxNew != 0)
			m_maxX = maxxNew;
		if (maxyNew != 0)
			m_maxY = maxyNew;
	}
}
