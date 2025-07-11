using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DigitalRuby.Tween
{
	public class Vector3Tween : Tween<Vector3>
	{
		private static readonly Func<ITween<Vector3>, Vector3, Vector3, float, Vector3> LerpFunc = LerpVector3;

		[CompilerGenerated]
		private static Func<ITween<Vector3>, Vector3, Vector3, float, Vector3> _003C_003Ef__mg_0024cache0;

		public Vector3Tween()
			: base(LerpFunc)
		{
		}

		private static Vector3 LerpVector3(ITween<Vector3> t, Vector3 start, Vector3 end, float progress)
		{
			return Vector3.Lerp(start, end, progress);
		}
	}
}
