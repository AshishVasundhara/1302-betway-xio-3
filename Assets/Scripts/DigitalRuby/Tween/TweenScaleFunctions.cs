using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DigitalRuby.Tween
{
	public static class TweenScaleFunctions
	{
		private const float halfPi = (float)Math.PI / 2f;

		public static readonly Func<float, float> Linear = linear;

		public static readonly Func<float, float> QuadraticEaseIn = quadraticEaseIn;

		public static readonly Func<float, float> QuadraticEaseOut = quadraticEaseOut;

		public static readonly Func<float, float> QuadraticEaseInOut = quadraticEaseInOut;

		public static readonly Func<float, float> CubicEaseIn = cubicEaseIn;

		public static readonly Func<float, float> CubicEaseOut = cubicEaseOut;

		public static readonly Func<float, float> CubicEaseInOut = cubicEaseInOut;

		public static readonly Func<float, float> QuarticEaseIn = quarticEaseIn;

		public static readonly Func<float, float> QuarticEaseOut = quarticEaseOut;

		public static readonly Func<float, float> QuarticEaseInOut = quarticEaseInOut;

		public static readonly Func<float, float> QuinticEaseIn = quinticEaseIn;

		public static readonly Func<float, float> QuinticEaseOut = quinticEaseOut;

		public static readonly Func<float, float> QuinticEaseInOut = quinticEaseInOut;

		public static readonly Func<float, float> SineEaseIn = sineEaseIn;

		public static readonly Func<float, float> SineEaseOut = sineEaseOut;

		public static readonly Func<float, float> SineEaseInOut = sineEaseInOut;

		[CompilerGenerated]
		private static Func<float, float> _003C_003Ef__mg_0024cache0;

		[CompilerGenerated]
		private static Func<float, float> _003C_003Ef__mg_0024cache1;

		[CompilerGenerated]
		private static Func<float, float> _003C_003Ef__mg_0024cache2;

		[CompilerGenerated]
		private static Func<float, float> _003C_003Ef__mg_0024cache3;

		[CompilerGenerated]
		private static Func<float, float> _003C_003Ef__mg_0024cache4;

		[CompilerGenerated]
		private static Func<float, float> _003C_003Ef__mg_0024cache5;

		[CompilerGenerated]
		private static Func<float, float> _003C_003Ef__mg_0024cache6;

		[CompilerGenerated]
		private static Func<float, float> _003C_003Ef__mg_0024cache7;

		[CompilerGenerated]
		private static Func<float, float> _003C_003Ef__mg_0024cache8;

		[CompilerGenerated]
		private static Func<float, float> _003C_003Ef__mg_0024cache9;

		[CompilerGenerated]
		private static Func<float, float> _003C_003Ef__mg_0024cacheA;

		[CompilerGenerated]
		private static Func<float, float> _003C_003Ef__mg_0024cacheB;

		[CompilerGenerated]
		private static Func<float, float> _003C_003Ef__mg_0024cacheC;

		[CompilerGenerated]
		private static Func<float, float> _003C_003Ef__mg_0024cacheD;

		[CompilerGenerated]
		private static Func<float, float> _003C_003Ef__mg_0024cacheE;

		[CompilerGenerated]
		private static Func<float, float> _003C_003Ef__mg_0024cacheF;

		private static float linear(float progress)
		{
			return progress;
		}

		private static float quadraticEaseIn(float progress)
		{
			return EaseInPower(progress, 2);
		}

		private static float quadraticEaseOut(float progress)
		{
			return EaseOutPower(progress, 2);
		}

		private static float quadraticEaseInOut(float progress)
		{
			return EaseInOutPower(progress, 2);
		}

		private static float cubicEaseIn(float progress)
		{
			return EaseInPower(progress, 3);
		}

		private static float cubicEaseOut(float progress)
		{
			return EaseOutPower(progress, 3);
		}

		private static float cubicEaseInOut(float progress)
		{
			return EaseInOutPower(progress, 3);
		}

		private static float quarticEaseIn(float progress)
		{
			return EaseInPower(progress, 4);
		}

		private static float quarticEaseOut(float progress)
		{
			return EaseOutPower(progress, 4);
		}

		private static float quarticEaseInOut(float progress)
		{
			return EaseInOutPower(progress, 4);
		}

		private static float quinticEaseIn(float progress)
		{
			return EaseInPower(progress, 5);
		}

		private static float quinticEaseOut(float progress)
		{
			return EaseOutPower(progress, 5);
		}

		private static float quinticEaseInOut(float progress)
		{
			return EaseInOutPower(progress, 5);
		}

		private static float sineEaseIn(float progress)
		{
			return Mathf.Sin(progress * ((float)Math.PI / 2f) - (float)Math.PI / 2f) + 1f;
		}

		private static float sineEaseOut(float progress)
		{
			return Mathf.Sin(progress * ((float)Math.PI / 2f));
		}

		private static float sineEaseInOut(float progress)
		{
			return (Mathf.Sin(progress * (float)Math.PI - (float)Math.PI / 2f) + 1f) / 2f;
		}

		private static float EaseInPower(float progress, int power)
		{
			return Mathf.Pow(progress, power);
		}

		private static float EaseOutPower(float progress, int power)
		{
			int num = (power % 2 != 0) ? 1 : (-1);
			return (float)num * (Mathf.Pow(progress - 1f, power) + (float)num);
		}

		private static float EaseInOutPower(float progress, int power)
		{
			progress *= 2f;
			if (progress < 1f)
			{
				return Mathf.Pow(progress, power) / 2f;
			}
			int num = (power % 2 != 0) ? 1 : (-1);
			return (float)num / 2f * (Mathf.Pow(progress - 2f, power) + (float)(num * 2));
		}
	}
}
