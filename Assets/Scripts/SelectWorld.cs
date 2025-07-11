using UnityEngine;

public class SelectWorld : MonoBehaviour
{
	public World.Enum world;

	private void Start()
	{
		World fromEnum = World.getFromEnum(world);
		GetComponent<TextMesh>().text = GetComponent<TextMesh>().text + " - " + 100 * fromEnum.bestPassed() / fromEnum.Count() + "%";
	}

	private void OnMouseUp()
	{
		Intent.world = World.getFromEnum(world);
		UnityEngine.SceneManagement.SceneManager.LoadScene("level_selection");
	}
}
