using System;
using UnityEngine;

namespace DigitalRuby.Tween
{
	public class Tween<T> : ITween<T>, ITween where T : struct
	{
		private readonly Func<ITween<T>, T, T, float, T> lerpFunc;

		private float currentTime;

		private float duration;

		private Func<float, float> scaleFunc;

		private Action<ITween<T>> progressCallback;

		private Action<ITween<T>> completionCallback;

		private TweenState state;

		private T start;

		private T end;

		private T value;

		public GameObject GameObject;

		public Renderer Renderer;

		public object Key
		{
			get;
			set;
		}

		public float CurrentTime => currentTime;

		public float Duration => duration;

		public TweenState State => state;

		public T StartValue => start;

		public T EndValue => end;

		public T CurrentValue => value;

		public float CurrentProgress
		{
			get;
			private set;
		}

		public Tween(Func<ITween<T>, T, T, float, T> lerpFunc)
		{
			this.lerpFunc = lerpFunc;
			state = TweenState.Stopped;
		}

		public void Start(T start, T end, float duration, Func<float, float> scaleFunc, Action<ITween<T>> progress, Action<ITween<T>> completion)
		{
			if (duration <= 0f)
			{
				throw new ArgumentException("duration must be greater than 0");
			}
			if (scaleFunc == null)
			{
				throw new ArgumentNullException("scaleFunc");
			}
			currentTime = 0f;
			this.duration = duration;
			this.scaleFunc = scaleFunc;
			progressCallback = progress;
			completionCallback = completion;
			state = TweenState.Running;
			this.start = start;
			this.end = end;
			UpdateValue();
		}

		public void Pause()
		{
			if (state == TweenState.Running)
			{
				state = TweenState.Paused;
			}
		}

		public void Resume()
		{
			if (state == TweenState.Paused)
			{
				state = TweenState.Running;
			}
		}

		public void Stop(TweenStopBehavior stopBehavior)
		{
			if (state == TweenState.Stopped)
			{
				return;
			}
			state = TweenState.Stopped;
			if (stopBehavior == TweenStopBehavior.Complete)
			{
				currentTime = duration;
				UpdateValue();
				if (completionCallback != null)
				{
					completionCallback(this);
					completionCallback = null;
				}
			}
		}

		public bool Update(float elapsedTime)
		{
			if (state == TweenState.Running)
			{
				currentTime += elapsedTime;
				if (currentTime >= duration)
				{
					Stop(TweenStopBehavior.Complete);
					return true;
				}
				UpdateValue();
				return false;
			}
			return state == TweenState.Stopped;
		}

		private void UpdateValue()
		{
			if (Renderer == null || Renderer.isVisible)
			{
				CurrentProgress = scaleFunc(currentTime / duration);
				value = lerpFunc(this, start, end, CurrentProgress);
				if (progressCallback != null)
				{
					progressCallback(this);
				}
			}
		}
	}
}
