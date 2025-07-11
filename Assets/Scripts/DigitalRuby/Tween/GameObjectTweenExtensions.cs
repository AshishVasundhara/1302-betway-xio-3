using System;
using UnityEngine;

namespace DigitalRuby.Tween
{
	public static class GameObjectTweenExtensions
	{
		public static FloatTween Tween(this GameObject obj, object key, float start, float end, float duration, Func<float, float> scaleFunc, Action<ITween<float>> progress, Action<ITween<float>> completion)
		{
			FloatTween floatTween = TweenFactory.Tween(key, start, end, duration, scaleFunc, progress, completion);
			floatTween.GameObject = obj;
			floatTween.Renderer = obj.GetComponent<Renderer>();
			return floatTween;
		}

		public static Vector2Tween Tween(this GameObject obj, object key, Vector2 start, Vector2 end, float duration, Func<float, float> scaleFunc, Action<ITween<Vector2>> progress, Action<ITween<Vector2>> completion)
		{
			Vector2Tween vector2Tween = TweenFactory.Tween(key, start, end, duration, scaleFunc, progress, completion);
			vector2Tween.GameObject = obj;
			vector2Tween.Renderer = obj.GetComponent<Renderer>();
			return vector2Tween;
		}

		public static Vector3Tween Tween(this GameObject obj, object key, Vector3 start, Vector3 end, float duration, Func<float, float> scaleFunc, Action<ITween<Vector3>> progress, Action<ITween<Vector3>> completion)
		{
			Vector3Tween vector3Tween = TweenFactory.Tween(key, start, end, duration, scaleFunc, progress, completion);
			vector3Tween.GameObject = obj;
			vector3Tween.Renderer = obj.GetComponent<Renderer>();
			return vector3Tween;
		}

		public static Vector4Tween Tween(this GameObject obj, object key, Vector4 start, Vector4 end, float duration, Func<float, float> scaleFunc, Action<ITween<Vector4>> progress, Action<ITween<Vector4>> completion)
		{
			Vector4Tween vector4Tween = TweenFactory.Tween(key, start, end, duration, scaleFunc, progress, completion);
			vector4Tween.GameObject = obj;
			vector4Tween.Renderer = obj.GetComponent<Renderer>();
			return vector4Tween;
		}

		public static ColorTween Tween(this GameObject obj, object key, Color start, Color end, float duration, Func<float, float> scaleFunc, Action<ITween<Color>> progress, Action<ITween<Color>> completion)
		{
			ColorTween colorTween = TweenFactory.Tween(key, start, end, duration, scaleFunc, progress, completion);
			colorTween.GameObject = obj;
			colorTween.Renderer = obj.GetComponent<Renderer>();
			return colorTween;
		}

		public static QuaternionTween Tween(this GameObject obj, object key, Quaternion start, Quaternion end, float duration, Func<float, float> scaleFunc, Action<ITween<Quaternion>> progress, Action<ITween<Quaternion>> completion)
		{
			QuaternionTween quaternionTween = TweenFactory.Tween(key, start, end, duration, scaleFunc, progress, completion);
			quaternionTween.GameObject = obj;
			quaternionTween.Renderer = obj.GetComponent<Renderer>();
			return quaternionTween;
		}
	}
}
