using UnityEngine;

public class AdinCubeRewardedEventManager : MonoBehaviour
{
	public delegate void OnAdCompletedDelegate();

	public delegate void OnAdFetchedDelegate();

	public delegate void OnFetchErrorDelegate(string errorCode);

	public delegate void OnAdShownDelegate();

	public delegate void OnErrorDelegate(string errorCode);

	public delegate void OnAdClickedDelegate();

	public delegate void OnAdHiddenDelegate();

	public static event OnAdCompletedDelegate OnAdCompleted;

	public static event OnAdFetchedDelegate OnAdFetched;

	public static event OnFetchErrorDelegate OnFetchError;

	public static event OnAdShownDelegate OnAdShown;

	public static event OnErrorDelegate OnError;

	public static event OnAdClickedDelegate OnAdClicked;

	public static event OnAdHiddenDelegate OnAdHidden;

	public void Awake()
	{
		Object.DontDestroyOnLoad(this);
	}

	public void OnAdCompletedCallback()
	{
		if (AdinCubeRewardedEventManager.OnAdCompleted != null)
		{
			AdinCubeRewardedEventManager.OnAdCompleted();
		}
	}

	public void OnFetchErrorCallback(string errorCode)
	{
		if (AdinCubeRewardedEventManager.OnFetchError != null)
		{
			AdinCubeRewardedEventManager.OnFetchError(errorCode);
		}
	}

	public void OnAdFetchedCallback()
	{
		if (AdinCubeRewardedEventManager.OnAdFetched != null)
		{
			AdinCubeRewardedEventManager.OnAdFetched();
		}
	}

	public void OnAdShownCallback()
	{
		if (AdinCubeRewardedEventManager.OnAdShown != null)
		{
			AdinCubeRewardedEventManager.OnAdShown();
		}
	}

	public void OnErrorCallback(string errorCode)
	{
		if (AdinCubeRewardedEventManager.OnError != null)
		{
			AdinCubeRewardedEventManager.OnError(errorCode);
		}
	}

	public void OnAdClickedCallback()
	{
		if (AdinCubeRewardedEventManager.OnAdClicked != null)
		{
			AdinCubeRewardedEventManager.OnAdClicked();
		}
	}

	public void OnAdHiddenCallback()
	{
		if (AdinCubeRewardedEventManager.OnAdHidden != null)
		{
			AdinCubeRewardedEventManager.OnAdHidden();
		}
	}
}
