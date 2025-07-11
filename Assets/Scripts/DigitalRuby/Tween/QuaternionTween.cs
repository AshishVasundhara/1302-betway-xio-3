using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DigitalRuby.Tween
{
	public class QuaternionTween : Tween<Quaternion>
	{
		private static readonly Func<ITween<Quaternion>, Quaternion, Quaternion, float, Quaternion> LerpFunc = LerpQuaternion;

		[CompilerGenerated]
		private static Func<ITween<Quaternion>, Quaternion, Quaternion, float, Quaternion> _003C_003Ef__mg_0024cache0;

		public QuaternionTween()
			: base(LerpFunc)
		{
		}

		private static Quaternion LerpQuaternion(ITween<Quaternion> t, Quaternion start, Quaternion end, float progress)
		{
			return Quaternion.Lerp(start, end, progress);
		}
	}
}
