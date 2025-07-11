using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DigitalRuby.Tween
{
	public class ColorTween : Tween<Color>
	{
		private static readonly Func<ITween<Color>, Color, Color, float, Color> LerpFunc = LerpColor;

		[CompilerGenerated]
		private static Func<ITween<Color>, Color, Color, float, Color> _003C_003Ef__mg_0024cache0;

		public ColorTween()
			: base(LerpFunc)
		{
		}

		private static Color LerpColor(ITween<Color> t, Color start, Color end, float progress)
		{
			return Color.Lerp(start, end, progress);
		}
	}
}
