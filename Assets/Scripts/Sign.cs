using UnityEngine;

public class Sign : MonoBehaviour
{
	public GameObject burstEffect;
	public bool isBroken = false;

	public void Break()
	{
		if (!isBroken)
		{
			GameObject burst = Instantiate(burstEffect, transform.position, transform.rotation);
			MessagingSystem.Publish(MessageType.BreakCrate);
			GetComponent<Animator>().SetTrigger("break");

			isBroken = true;
			Destroy(burst, 2f);
		}
	}
}
