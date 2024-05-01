using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToNewScene : MonoBehaviour
{
	public bool hasKey = false;
	public bool ishouse;
	public bool portalToPreviousScene;

	[SerializeField] private LevelManager m_levelManager;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			if (hasKey)
			{
				if (portalToPreviousScene)
				{
					Debug.Log("bump");
					SceneManager.LoadScene(m_levelManager.levels[m_levelManager.CurrentLevel - 1].levelName);
					m_levelManager.LevelBackward();
				}
				else
				{
					Debug.Log("bum");
					SceneManager.LoadScene(m_levelManager.levels[m_levelManager.CurrentLevel + 1].levelName);
					m_levelManager.LevelForward();
				}
			}
		}
	}
}

