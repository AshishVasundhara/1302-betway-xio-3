using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class Neiko : ScriptableObject
{
	public static Transform LoadUI(string name)
	{
		return (UnityEngine.Object.Instantiate(Resources.Load(name), GameObject.Find("Canvas").transform, instantiateInWorldSpace: false) as GameObject).transform;
	}

	public static bool IsPointerOverUIObject()
	{
		PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
		PointerEventData pointerEventData2 = pointerEventData;
		Vector3 mousePosition = UnityEngine.Input.mousePosition;
		float x = mousePosition.x;
		Vector3 mousePosition2 = UnityEngine.Input.mousePosition;
		pointerEventData2.position = new Vector2(x, mousePosition2.y);
		List<RaycastResult> list = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerEventData, list);
		return list.Count > 0;
	}

	public static string SecondsToText(long seconds)
	{
		int num = (int)(seconds / 3600);
		seconds %= 3600;
		int num2 = (int)(seconds / 60);
		seconds %= 60;
		return num.ToString("00") + ":" + num2.ToString("00") + ":" + seconds.ToString("00");
	}

	public static void ShareText(string _text)
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.content.Intent");
		androidJavaObject.Call<AndroidJavaObject>("setAction", new object[1]
		{
			androidJavaClass.GetStatic<string>("ACTION_SEND")
		});
		androidJavaObject.Call<AndroidJavaObject>("setType", new object[1]
		{
			"text/plain"
		});
		androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[2]
		{
			androidJavaClass.GetStatic<string>("EXTRA_TEXT"),
			_text
		});
		AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject @static = androidJavaClass2.GetStatic<AndroidJavaObject>("currentActivity");
		@static.Call("startActivity", androidJavaObject);
	}

	public static void ShareImage(string path, string text)
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.content.Intent");
		androidJavaObject.Call<AndroidJavaObject>("setAction", new object[1]
		{
			androidJavaClass.GetStatic<string>("ACTION_SEND")
		});
		AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("android.net.Uri");
		AndroidJavaObject androidJavaObject2 = androidJavaClass2.CallStatic<AndroidJavaObject>("parse", new object[1]
		{
			"file://" + path
		});
		androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[2]
		{
			androidJavaClass.GetStatic<string>("EXTRA_STREAM"),
			androidJavaObject2
		});
		androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[2]
		{
			androidJavaClass.GetStatic<string>("EXTRA_TEXT"),
			text
		});
		androidJavaObject.Call<AndroidJavaObject>("setType", new object[1]
		{
			"image/jpeg"
		});
		AndroidJavaClass androidJavaClass3 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject @static = androidJavaClass3.GetStatic<AndroidJavaObject>("currentActivity");
		@static.Call("startActivity", androidJavaObject);
	}

	public static IEnumerator ShareScreenshot(string _text)
	{
		yield return new WaitForEndOfFrame();
		Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, mipChain: true);
		screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
		screenTexture.Apply();
		byte[] dataToSave = screenTexture.EncodeToPNG();
		string destination = Path.Combine(Application.persistentDataPath, DateTime.Now.ToString("yyyy - MM - dd - HHmmss") + ".png");
		File.WriteAllBytes(destination, dataToSave);
		ShareImage(destination, _text);
	}
}
