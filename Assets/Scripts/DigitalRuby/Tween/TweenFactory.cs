using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DigitalRuby.Tween
{
	public class TweenFactory : MonoBehaviour
	{
		private static bool needsInitialize = true;

		private static GameObject root;

		private static readonly List<ITween> tweens = new List<ITween>();

		public static TweenStopBehavior AddKeyStopBehavior = TweenStopBehavior.DoNotModify;

		private static void EnsureCreated()
		{
			if (needsInitialize)
			{
				needsInitialize = false;
				root = new GameObject();
				root.name = "DigitalRubyTween";
				root.hideFlags = HideFlags.HideAndDontSave;
				root.AddComponent<TweenFactory>();
				UnityEngine.Object.DontDestroyOnLoad(root);
			}
		}

		private void Start()
		{
			SceneManager.sceneLoaded += SceneManagerSceneLoaded;
		}

		private void SceneManagerSceneLoaded(Scene s, LoadSceneMode m)
		{
			tweens.Clear();
		}

		private void Update()
		{
			for (int num = tweens.Count - 1; num >= 0; num--)
			{
				ITween tween = tweens[num];
				if (tween.Update(Time.deltaTime) && num < tweens.Count && tweens[num] == tween)
				{
					tweens.RemoveAt(num);
				}
			}
		}

		public static FloatTween Tween(object key, float start, float end, float duration, Func<float, float> scaleFunc, Action<ITween<float>> progress, Action<ITween<float>> completion)
		{
			FloatTween floatTween = new FloatTween();
			floatTween.Key = key;
			floatTween.Start(start, end, duration, scaleFunc, progress, completion);
			AddTween(floatTween);
			return floatTween;
		}

		public static Vector2Tween Tween(object key, Vector2 start, Vector2 end, float duration, Func<float, float> scaleFunc, Action<ITween<Vector2>> progress, Action<ITween<Vector2>> completion)
		{
			Vector2Tween vector2Tween = new Vector2Tween();
			vector2Tween.Key = key;
			vector2Tween.Start(start, end, duration, scaleFunc, progress, completion);
			AddTween(vector2Tween);
			return vector2Tween;
		}

		public static Vector3Tween Tween(object key, Vector3 start, Vector3 end, float duration, Func<float, float> scaleFunc, Action<ITween<Vector3>> progress, Action<ITween<Vector3>> completion)
		{
			Vector3Tween vector3Tween = new Vector3Tween();
			vector3Tween.Key = key;
			vector3Tween.Start(start, end, duration, scaleFunc, progress, completion);
			AddTween(vector3Tween);
			return vector3Tween;
		}

		public static Vector4Tween Tween(object key, Vector4 start, Vector4 end, float duration, Func<float, float> scaleFunc, Action<ITween<Vector4>> progress, Action<ITween<Vector4>> completion)
		{
			Vector4Tween vector4Tween = new Vector4Tween();
			vector4Tween.Key = key;
			vector4Tween.Start(start, end, duration, scaleFunc, progress, completion);
			AddTween(vector4Tween);
			return vector4Tween;
		}

		public static ColorTween Tween(object key, Color start, Color end, float duration, Func<float, float> scaleFunc, Action<ITween<Color>> progress, Action<ITween<Color>> completion)
		{
			ColorTween colorTween = new ColorTween();
			colorTween.Key = key;
			colorTween.Start(start, end, duration, scaleFunc, progress, completion);
			AddTween(colorTween);
			return colorTween;
		}

		public static QuaternionTween Tween(object key, Quaternion start, Quaternion end, float duration, Func<float, float> scaleFunc, Action<ITween<Quaternion>> progress, Action<ITween<Quaternion>> completion)
		{
			QuaternionTween quaternionTween = new QuaternionTween();
			quaternionTween.Key = key;
			quaternionTween.Start(start, end, duration, scaleFunc, progress, completion);
			AddTween(quaternionTween);
			return quaternionTween;
		}

		public static void AddTween(ITween tween)
		{
			EnsureCreated();
			if (tween.Key != null)
			{
				RemoveTweenKey(tween.Key, AddKeyStopBehavior);
			}
			tweens.Add(tween);
		}

		public static bool RemoveTween(ITween tween, TweenStopBehavior stopBehavior)
		{
			tween.Stop(stopBehavior);
			return tweens.Remove(tween);
		}

		public static bool RemoveTweenKey(object key, TweenStopBehavior stopBehavior)
		{
			if (key == null)
			{
				return false;
			}
			bool result = false;
			for (int num = tweens.Count - 1; num >= 0; num--)
			{
				ITween tween = tweens[num];
				if (key.Equals(tween.Key))
				{
					tween.Stop(stopBehavior);
					tweens.RemoveAt(num);
					result = true;
				}
			}
			return result;
		}
	}
}
