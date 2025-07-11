using UnityEngine;

public class AdinCubeBannerEventManager : MonoBehaviour
{
	public delegate void OnAdLoadedDelegate();

	public delegate void OnAdShownDelegate();

	public delegate void OnErrorDelegate(string errorCode);

	public delegate void OnAdClickedDelegate();

	public static event OnAdLoadedDelegate OnAdLoaded;

	public static event OnAdShownDelegate OnAdShown;

	public static event OnErrorDelegate OnError;

	public static event OnAdClickedDelegate OnAdClicked;

	private void OnApplicationPause(bool pause)
	{
	}

	public void Awake()
	{
		Object.DontDestroyOnLoad(this);
	}

	public void OnAdLoadedCallback()
	{
		if (AdinCubeBannerEventManager.OnAdLoaded != null)
		{
			AdinCubeBannerEventManager.OnAdLoaded();
		}
	}

	public void OnAdShownCallback()
	{
		if (AdinCubeBannerEventManager.OnAdShown != null)
		{
			AdinCubeBannerEventManager.OnAdShown();
		}
	}

	public void OnErrorCallback(string errorCode)
	{
		if (AdinCubeBannerEventManager.OnError != null)
		{
			AdinCubeBannerEventManager.OnError(errorCode);
		}
	}

	public void OnAdClickedCallback()
	{
		if (AdinCubeBannerEventManager.OnAdClicked != null)
		{
			AdinCubeBannerEventManager.OnAdClicked();
		}
	}
}
