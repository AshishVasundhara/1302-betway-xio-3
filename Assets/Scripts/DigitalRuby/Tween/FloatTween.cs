using System;
using System.Runtime.CompilerServices;

namespace DigitalRuby.Tween
{
	public class FloatTween : Tween<float>
	{
		private static readonly Func<ITween<float>, float, float, float, float> LerpFunc = LerpFloat;

		[CompilerGenerated]
		private static Func<ITween<float>, float, float, float, float> _003C_003Ef__mg_0024cache0;

		public FloatTween()
			: base(LerpFunc)
		{
		}

		private static float LerpFloat(ITween<float> t, float start, float end, float progress)
		{
			return start + (end - start) * progress;
		}
	}
}
