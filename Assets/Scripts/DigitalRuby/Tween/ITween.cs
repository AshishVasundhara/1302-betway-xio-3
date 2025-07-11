using System;

namespace DigitalRuby.Tween
{
	public interface ITween
	{
		object Key
		{
			get;
		}

		TweenState State
		{
			get;
		}

		void Pause();

		void Resume();

		void Stop(TweenStopBehavior stopBehavior);

		bool Update(float elapsedTime);
	}
	public interface ITween<T> : ITween where T : struct
	{
		T CurrentValue
		{
			get;
		}

		float CurrentProgress
		{
			get;
		}

		void Start(T start, T end, float duration, Func<float, float> scaleFunc, Action<ITween<T>> progress, Action<ITween<T>> completion);
	}
}
