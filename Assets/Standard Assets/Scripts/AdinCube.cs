using UnityEngine;

public class AdinCube
{
	public static class Interstitial
	{
		public static void Init()
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				adInCubeInterstitialBinding.Call("init");
			}
		}

		public static bool IsReady()
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				return adInCubeInterstitialBinding.Call<bool>("isReady", new object[0]);
			}
			return false;
		}

		public static void Show()
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				adInCubeInterstitialBinding.Call("show");
			}
		}
	}

	public static class Banner
	{
		public enum Size
		{
			BANNER_320x50,
			BANNER_728x90,
			BANNER_AUTO
		}

		public enum Position
		{
			TOP,
			TOP_LEFT,
			TOP_RIGHT,
			BOTTOM,
			BOTTOM_LEFT,
			BOTTOM_RIGHT
		}

		public static void Load(Size size, Position position)
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				adInCubeBannerBinding.Call("load", size.ToString(), position.ToString());
			}
		}

		public static void Show(Size size, Position position)
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				adInCubeBannerBinding.Call("show", size.ToString(), position.ToString());
			}
		}

		public static void Hide()
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				adInCubeBannerBinding.Call("hide");
			}
		}

		public static int GetWidth()
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				return adInCubeBannerBinding.Call<int>("getWidth", new object[0]);
			}
			return 0;
		}

		public static int GetHeight()
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				return adInCubeBannerBinding.Call<int>("getHeight", new object[0]);
			}
			return 0;
		}

		public static void SetVisible(bool visible)
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				adInCubeBannerBinding.Call("setVisible", visible);
			}
		}
	}

	public static class Rewarded
	{
		public static void Fetch()
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				adInCubeRewardedBinding.Call("fetch");
			}
		}

		public static bool IsReady()
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				return adInCubeRewardedBinding.Call<bool>("isReady", new object[0]);
			}
			return false;
		}

		public static void Show()
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				adInCubeRewardedBinding.Call("show");
			}
		}
	}

	private static AndroidJavaObject adInCubeBinding;

	private static AndroidJavaObject adInCubeInterstitialBinding;

	private static AndroidJavaObject adInCubeBannerBinding;

	private static AndroidJavaObject adInCubeRewardedBinding;

	static AdinCube()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			adInCubeBinding = new AndroidJavaObject("com.adincube.sdk.unity.AdinCubeBinding");
			adInCubeInterstitialBinding = new AndroidJavaObject("com.adincube.sdk.unity.AdinCubeInterstitialBinding");
			adInCubeBannerBinding = new AndroidJavaObject("com.adincube.sdk.unity.AdinCubeBannerBinding");
			adInCubeRewardedBinding = new AndroidJavaObject("com.adincube.sdk.unity.AdinCubeRewardedBinding");
		}
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			GameObject x = GameObject.Find("AdinCubeInterstitialEventManager");
			if (x == null)
			{
				new GameObject("AdinCubeInterstitialEventManager").AddComponent<AdinCubeInterstitialEventManager>();
			}
			x = GameObject.Find("AdinCubeBannerEventManager");
			if (x == null)
			{
				new GameObject("AdinCubeBannerEventManager").AddComponent<AdinCubeBannerEventManager>();
			}
			x = GameObject.Find("AdinCubeRewardedEventManager");
			if (x == null)
			{
				new GameObject("AdinCubeRewardedEventManager").AddComponent<AdinCubeRewardedEventManager>();
			}
		}
	}

	public static void SetAndroidAppKey(string androidAppKey)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			adInCubeBinding.Call("setAppKey", androidAppKey);
		}
	}

	public static void SetIOSAppKey(string iOSAppKey)
	{
		if (Application.platform != RuntimePlatform.IPhonePlayer)
		{
		}
	}

	public static void NativeLog(string text)
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			adInCubeBinding.Call("nativeLog", text);
		}
	}

	public static void NativeToast(string text)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			adInCubeBinding.Call("nativeToast", text);
		}
	}
}
