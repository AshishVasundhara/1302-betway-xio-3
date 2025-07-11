using UnityEngine;
using UnityEngine.UI;

public class WorldSelection : MonoBehaviour
{
	private void Start()
	{
		World world = new WorldBeginner();
		GameObject.Find("beginner/Count").GetComponent<Text>().text = world.bestPassed() + "/" + world.Count();
		world = new WorldMedium();
		GameObject.Find("medium/Count").GetComponent<Text>().text = world.bestPassed() + "/" + world.Count();
		world = new WorldHard();
		GameObject.Find("hard/Count").GetComponent<Text>().text = world.bestPassed() + "/" + world.Count();
		world = new WorldExtra();
		//if (Purchaser.HasExtraLevels())
		//{
		//	GameObject.Find("extra/Count").GetComponent<Text>().text = world.bestPassed() + "/" + world.Count();
		//}
		//else
		//{
		//	GameObject.Find("extra/Count").GetComponent<Text>().text = "Buy";
		//}
	}

	private void Update()
	{
	}

	public void GoBeginner()
	{
		Intent.world = new WorldBeginner();
		CameraScene.ChangeScene("LevelSelection");
	}

	public void GoMedium()
	{
		Intent.world = new WorldMedium();
		CameraScene.ChangeScene("LevelSelection");
	}

	public void GoHard()
	{
		Intent.world = new WorldHard();
		CameraScene.ChangeScene("LevelSelection");
	}

	public void GoExtra()
	{
		//if (Purchaser.HasExtraLevels())
		//{
		//	Intent.world = new WorldExtra();
		//	CameraScene.ChangeScene("LevelSelection");
		//}
		//else
		//{
		//	//Object.FindObjectOfType<Purchaser>().BuyRemoveAdsAndExtraLevels();
		//}
	}
}
