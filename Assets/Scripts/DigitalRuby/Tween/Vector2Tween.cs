using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DigitalRuby.Tween
{
	public class Vector2Tween : Tween<Vector2>
	{
		private static readonly Func<ITween<Vector2>, Vector2, Vector2, float, Vector2> LerpFunc = LerpVector2;

		[CompilerGenerated]
		private static Func<ITween<Vector2>, Vector2, Vector2, float, Vector2> _003C_003Ef__mg_0024cache0;

		public Vector2Tween()
			: base(LerpFunc)
		{
		}

		private static Vector2 LerpVector2(ITween<Vector2> t, Vector2 start, Vector2 end, float progress)
		{
			return Vector2.Lerp(start, end, progress);
		}
	}
}
