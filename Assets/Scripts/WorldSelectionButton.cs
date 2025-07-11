using UnityEngine;

public class WorldSelectionButton : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnMouseUp()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("world_selection");
	}
}
