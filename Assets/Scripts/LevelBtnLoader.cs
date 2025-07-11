using UnityEngine;
using UnityEngine.UI;

public class LevelBtnLoader : MonoBehaviour
{
	private bool mousePressed;

	private Vector3 mouseStart = Vector3.zero;

	private Vector3 realMouseStart = Vector3.zero;

	private float speed = 0.3f;

	private float releaseTime;

	private bool busy;

	private void Start()
	{
		GameObject original = Resources.Load<GameObject>("LevelButton");
		for (int i = 0; i < Intent.world.Count(); i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(original);
			gameObject.transform.SetParent(base.transform.Find("Panel/Scroll View/Viewport/Content"), worldPositionStays: false);
			gameObject.transform.Find("Text").GetComponent<Text>().text = string.Empty + (i + 1);
			int lvl = i + 1;
			gameObject.GetComponent<Button>().onClick.AddListener(delegate
			{
				Intent.level_id = lvl;
				CameraScene.ChangeScene("Game");
			});
			if (Intent.world.NotYetUnlocked(lvl))
			{
				gameObject.GetComponent<Button>().interactable = false;
			}
			else if (Intent.world.AlreadyPassed(lvl))
			{
				ColorBlock colors = gameObject.GetComponent<Button>().colors;
				colors.normalColor = colors.highlightedColor;
				gameObject.GetComponent<Button>().colors = colors;
			}
		}
	}

	public bool IsBusy()
	{
		Vector3 vector = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
		busy = (mousePressed && Mathf.Abs(vector.x - realMouseStart.x) > 0.1f);
		return busy || Time.time - releaseTime < 0.2f;
	}
}
