using UnityEngine;

public class HousePortal : MonoBehaviour
{
	[SerializeField] private Transform m_positionToTeleport;
	[SerializeField] private bool m_isPortalBack;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			MessagingSystem.Publish(MessageType.TeleportPlayer, m_positionToTeleport.position);

			if (m_isPortalBack)
			{
				MessagingSystem.Publish(MessageType.RestrictCamera);
			}
			else
			{
				MessagingSystem.Publish(MessageType.UnRestrictCamera);
			}
		}
	}
}
