using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public int CurrentLevel { get; private set; } = 0;
	public Level[] levels;

	[SerializeField] private PlayerController2D m_playerController2D;

	public void LevelForward()
	{

		//Fade out level change
		/*Camera.main.GetComponent<CameraMove>()
            .ChangeLevelBorders(levels[currentLevel].minX, levels[currentLevel].minY, 
                    levels[currentLevel].maxX, levels[currentLevel].maxY);*/
		CurrentLevel++;
		PutPlayerOnStartingPosition(m_playerController2D.gameObject);
	}
	public void LevelBackward()
	{
		//Fade out level change
		/*Camera.main.GetComponent<CameraMove>().ChangeLevelBorders(levels[currentLevel].minX, levels[currentLevel].minY, 
                    levels[currentLevel].maxX, levels[currentLevel].maxY);*/
		CurrentLevel--;
		PutPlayerOnEndPosition(m_playerController2D.gameObject);
	}

	public void PutPlayerOnStartingPosition(GameObject player)
	{
		player.transform.position = new Vector3(levels[CurrentLevel].startingPosition.transform.position.x,
			levels[CurrentLevel].startingPosition.transform.position.y, player.transform.position.z);
	}
	public void PutPlayerOnEndPosition(GameObject player)
	{
		player.transform.position = new Vector3(levels[CurrentLevel].endPosition.transform.position.x,
		 levels[CurrentLevel].endPosition.transform.position.y, player.transform.position.z);
	}

	public void SetToLevelOne()
	{
		CurrentLevel = 0;
		SceneManager.LoadScene(levels[0].levelName);
		PutPlayerOnStartingPosition(m_playerController2D.gameObject);
	}
}
