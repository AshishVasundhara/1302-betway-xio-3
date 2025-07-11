using UnityEngine;

public class AdinCubeInterstitialEventManager : MonoBehaviour
{
	public delegate void OnAdCachedDelegate();

	public delegate void OnAdShownDelegate();

	public delegate void OnErrorDelegate(string errorCode);

	public delegate void OnAdClickedDelegate();

	public delegate void OnAdHiddenDelegate();

	public static event OnAdCachedDelegate OnAdCached;

	public static event OnAdShownDelegate OnAdShown;

	public static event OnErrorDelegate OnError;

	public static event OnAdClickedDelegate OnAdClicked;

	public static event OnAdHiddenDelegate OnAdHidden;

	private void OnApplicationPause(bool pause)
	{
		if (!pause)
		{
			AdinCube.Interstitial.Init();
		}
	}

	public void Awake()
	{
		Object.DontDestroyOnLoad(this);
	}

	public void OnAdCachedCallback()
	{
		if (AdinCubeInterstitialEventManager.OnAdCached != null)
		{
			AdinCubeInterstitialEventManager.OnAdCached();
		}
	}

	public void OnAdShownCallback()
	{
		if (AdinCubeInterstitialEventManager.OnAdShown != null)
		{
			AdinCubeInterstitialEventManager.OnAdShown();
		}
	}

	public void OnErrorCallback(string errorCode)
	{
		if (AdinCubeInterstitialEventManager.OnError != null)
		{
			AdinCubeInterstitialEventManager.OnError(errorCode);
		}
	}

	public void OnAdClickedCallback()
	{
		if (AdinCubeInterstitialEventManager.OnAdClicked != null)
		{
			AdinCubeInterstitialEventManager.OnAdClicked();
		}
	}

	public void OnAdHiddenCallback()
	{
		if (AdinCubeInterstitialEventManager.OnAdHidden != null)
		{
			AdinCubeInterstitialEventManager.OnAdHidden();
		}
	}
}
