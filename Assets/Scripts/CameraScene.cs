using DigitalRuby.Tween;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScene : MonoBehaviour
{

	[SerializeField]private  GameObject _exitPanel;//exit Panel
	private float _quickCount; 

    private void Awake()
    {
		_exitPanel.SetActive(false);
		_quickCount = 0;
    }

    private void Start()
	{
		base.gameObject.Tween("CameraMove" + base.gameObject.GetInstanceID(), 12.8f, 0f, 1f, TweenScaleFunctions.CubicEaseInOut, delegate (ITween<float> t)
		{
			Transform transform4 = base.gameObject.transform;
			float currentValue4 = t.CurrentValue;
			Vector3 localPosition13 = base.gameObject.transform.localPosition;
			transform4.localPosition = new Vector3(0f, currentValue4, localPosition13.z);
		}, delegate
		{
		});
		TweenRightComer[] array = UnityEngine.Object.FindObjectsOfType<TweenRightComer>();
		foreach (TweenRightComer t3 in array)
		{
			GameObject gameObject = t3.gameObject;
			string key = "come" + t3.GetInstanceID();
			Vector3 localPosition = t3.transform.localPosition;
			float start = localPosition.x + 720f;
			Vector3 localPosition2 = t3.transform.localPosition;
			gameObject.Tween(key, start, localPosition2.x, 0.5f, TweenScaleFunctions.SineEaseInOut, delegate (ITween<float> t2)
			{
				Transform transform3 = t3.transform;
				float currentValue3 = t2.CurrentValue;
				Vector3 localPosition11 = t3.transform.localPosition;
				float y = localPosition11.y;
				Vector3 localPosition12 = t3.transform.localPosition;
				transform3.localPosition = new Vector3(currentValue3, y, localPosition12.z);
			}, delegate
			{
			});
		}
		TweenUpComer[] array2 = UnityEngine.Object.FindObjectsOfType<TweenUpComer>();
		foreach (TweenUpComer t4 in array2)
		{
			GameObject gameObject2 = t4.gameObject;
			string key2 = "come" + t4.GetInstanceID();
			Vector3 localPosition3 = t4.transform.localPosition;
			float start2 = localPosition3.y + 720f;
			Vector3 localPosition4 = t4.transform.localPosition;
			gameObject2.Tween(key2, start2, localPosition4.y, 0.5f, TweenScaleFunctions.SineEaseInOut, delegate (ITween<float> t2)
			{
				Transform transform2 = t4.transform;
				Vector3 localPosition9 = t4.transform.localPosition;
				float x2 = localPosition9.x;
				float currentValue2 = t2.CurrentValue;
				Vector3 localPosition10 = t4.transform.localPosition;
				transform2.localPosition = new Vector3(x2, currentValue2, localPosition10.z);
			}, delegate
			{
			});
		}
		TweenDownComer[] array3 = UnityEngine.Object.FindObjectsOfType<TweenDownComer>();
		foreach (TweenDownComer t5 in array3)
		{
			GameObject gameObject3 = t5.gameObject;
			string key3 = "come" + t5.GetInstanceID();
			Vector3 localPosition5 = t5.transform.localPosition;
			float start3 = localPosition5.y - 720f;
			Vector3 localPosition6 = t5.transform.localPosition;
			gameObject3.Tween(key3, start3, localPosition6.y, 0.5f, TweenScaleFunctions.SineEaseInOut, delegate (ITween<float> t2)
			{
				Transform transform = t5.transform;
				Vector3 localPosition7 = t5.transform.localPosition;
				float x = localPosition7.x;
				float currentValue = t2.CurrentValue;
				Vector3 localPosition8 = t5.transform.localPosition;
				transform.localPosition = new Vector3(x, currentValue, localPosition8.z);
			}, delegate
			{
			});
		}
	}

	public static void ChangeScene(string sceneName)
	{
		Transform camera = UnityEngine.Object.FindObjectOfType<CameraScene>().transform;
		camera.gameObject.Tween("CameraMove" + camera.gameObject.GetInstanceID(), 0f, -12.8f, 0.5f, TweenScaleFunctions.CubicEaseInOut, delegate (ITween<float> t)
		{
			Transform transform4 = camera.gameObject.transform;
			float currentValue4 = t.CurrentValue;
			Vector3 localPosition13 = camera.gameObject.transform.localPosition;
			transform4.localPosition = new Vector3(0f, currentValue4, localPosition13.z);
		}, delegate
		{
			SceneManager.LoadScene(sceneName);
		});
		TweenRightComer[] array = UnityEngine.Object.FindObjectsOfType<TweenRightComer>();
		foreach (TweenRightComer t3 in array)
		{
			GameObject gameObject = t3.gameObject;
			string key = "come" + t3.GetInstanceID();
			Vector3 localPosition = t3.transform.localPosition;
			float x = localPosition.x;
			Vector3 localPosition2 = t3.transform.localPosition;
			gameObject.Tween(key, x, localPosition2.x - 720f, 0.5f, TweenScaleFunctions.SineEaseInOut, delegate (ITween<float> t2)
			{
				Transform transform3 = t3.transform;
				float currentValue3 = t2.CurrentValue;
				Vector3 localPosition11 = t3.transform.localPosition;
				float y3 = localPosition11.y;
				Vector3 localPosition12 = t3.transform.localPosition;
				transform3.localPosition = new Vector3(currentValue3, y3, localPosition12.z);
			}, delegate
			{
			});
		}
		TweenUpComer[] array2 = UnityEngine.Object.FindObjectsOfType<TweenUpComer>();
		foreach (TweenUpComer t4 in array2)
		{
			GameObject gameObject2 = t4.gameObject;
			string key2 = "come" + t4.GetInstanceID();
			Vector3 localPosition3 = t4.transform.localPosition;
			float y = localPosition3.y;
			Vector3 localPosition4 = t4.transform.localPosition;
			gameObject2.Tween(key2, y, localPosition4.y + 720f, 0.5f, TweenScaleFunctions.SineEaseInOut, delegate (ITween<float> t2)
			{
				Transform transform2 = t4.transform;
				Vector3 localPosition9 = t4.transform.localPosition;
				float x3 = localPosition9.x;
				float currentValue2 = t2.CurrentValue;
				Vector3 localPosition10 = t4.transform.localPosition;
				transform2.localPosition = new Vector3(x3, currentValue2, localPosition10.z);
			}, delegate
			{
			});
		}
		TweenDownComer[] array3 = UnityEngine.Object.FindObjectsOfType<TweenDownComer>();
		foreach (TweenDownComer t5 in array3)
		{
			GameObject gameObject3 = t5.gameObject;
			string key3 = "come" + t5.GetInstanceID();
			Vector3 localPosition5 = t5.transform.localPosition;
			float y2 = localPosition5.y;
			Vector3 localPosition6 = t5.transform.localPosition;
			gameObject3.Tween(key3, y2, localPosition6.y - 720f, 0.5f, TweenScaleFunctions.SineEaseInOut, delegate (ITween<float> t2)
			{
				Transform transform = t5.transform;
				Vector3 localPosition7 = t5.transform.localPosition;
				float x2 = localPosition7.x;
				float currentValue = t2.CurrentValue;
				Vector3 localPosition8 = t5.transform.localPosition;
				transform.localPosition = new Vector3(x2, currentValue, localPosition8.z);
			}, delegate
			{
			});
		}
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			_quickCount++;
			if (_quickCount >= 2)
            {
				Debug.Log("Application Quit Call");
				Application.Quit();
            }
            else
			{
				GoBackScene();
				_ = StartCoroutine(Reset());
			}
		}
	}

	public void GoBackScene()
	{
		//FirebaseAnalytics.LogEvent("gobackscene");
		switch (SceneManager.GetActiveScene().name)
		{
			case "Home":
				OpenExitPanel();
				break;
			case "Game":
				ChangeScene("LevelSelection");
				break;
			case "LevelSelection":
				ChangeScene("WorldSelection");
				break;
			default:
				ChangeScene("Home");
				break;
		}
	}

	private IEnumerator Reset()
    {
		yield return new WaitForSeconds(0.6f);
		_quickCount = 0;
    }

	private void OpenExitPanel()
	{
		_exitPanel.SetActive(true);
	}

	public void OnExitYesBtnClick()
	{
		Application.Quit();
	}

}
