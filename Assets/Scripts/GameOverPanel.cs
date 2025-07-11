using DigitalRuby.Tween;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
	private static GameObject blackOverlay;

    //public Admobs admob;

	private void Start()
	{
		base.gameObject.Tween("Appear" + base.gameObject.GetInstanceID(), 1280f, 0f, 1f, TweenScaleFunctions.CubicEaseInOut, delegate(ITween<float> t)
		{
			base.transform.localPosition = new Vector3(0f, t.CurrentValue, 0f);
		}, delegate
		{
			DisplayStars();
		});
        //admob = GameObject.Find("Admob").GetComponent<Admobs>();
	}

	private void DisplayStars()
	{
		GameObject star4 = base.transform.Find("Star/Image").gameObject;
		star4.Tween("AppearStar" + star4.GetInstanceID(), 0f, 1f, 0.5f, TweenScaleFunctions.CubicEaseInOut, delegate(ITween<float> t)
		{
			star4.transform.localScale = new Vector3(t.CurrentValue, t.CurrentValue, 1f);
		}, delegate
		{
		});
		GameObject star3 = base.transform.Find("Star (1)/Image").gameObject;
		star3.Tween("AppearStar" + star3.GetInstanceID(), 0f, 1f, 1f, TweenScaleFunctions.CubicEaseInOut, delegate(ITween<float> t2)
		{
			star3.transform.localScale = new Vector3(t2.CurrentValue, t2.CurrentValue, 1f);
		}, delegate
		{
		});
		GameObject star2 = base.transform.Find("Star (2)/Image").gameObject;
		star2.Tween("AppearStar" + star2.GetInstanceID(), 0f, 1f, 1.5f, TweenScaleFunctions.CubicEaseInOut, delegate(ITween<float> t3)
		{
			star2.transform.localScale = new Vector3(t3.CurrentValue, t3.CurrentValue, 1f);
		}, delegate
		{
		});
	}

	private void Update()
	{
	}

	internal static void Create()
	{
		if (blackOverlay == null)
		{
			blackOverlay = (UnityEngine.Object.Instantiate(Resources.Load("BlackOverlay"), GameObject.Find("Canvas").transform, instantiateInWorldSpace: false) as GameObject);
		}
		Object.Instantiate(Resources.Load("GameOverPanel"), GameObject.Find("Canvas").transform, instantiateInWorldSpace: false);
	}

	public void OnClickContinue()
	{
        //AdbuddizLoader.ProposeAdDisplay();
        //admob.ShowAds();
		Intent.level_id++;
		UnityEngine.Object.Destroy(blackOverlay);
		if (Intent.GetLevel() != null)
		{
			CameraScene.ChangeScene(SceneManager.GetActiveScene().name);
		}
		else
		{
			CameraScene.ChangeScene("WorldSelection");
		}
	}
}
