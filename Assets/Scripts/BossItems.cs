using UnityEngine;

public class BossItems : MonoBehaviour
{
	public GameObject itemToDrop;

	private EnemyHealthManager m_boss;

	private void Start()
	{
		m_boss = gameObject.GetComponent<EnemyHealthManager>();
		m_boss.OnEnemyDeath += EnemyHealthManager_OnDeath;
	}

	private void EnemyHealthManager_OnDeath(object sender, System.EventArgs e)
	{
		DropItems();
	}

	private void DropItems()
	{
		Vector3 position = gameObject.transform.position;
		
		if (itemToDrop != null)
			Instantiate(itemToDrop, position, Quaternion.identity);

		MessagingSystem.Publish(MessageType.BossDied);
	}
}
