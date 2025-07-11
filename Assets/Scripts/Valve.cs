using DigitalRuby.Tween;

using UnityEngine;

public class Valve : MonoBehaviour
{
	private Party party;

	private bool opened;

	private void Start()
	{
		party = UnityEngine.Object.FindObjectOfType<Party>();
	}

	private void Update()
	{
	}

	public void Open()
	{
		opened = true;
		base.gameObject.Tween("RotateValve" + base.gameObject.GetInstanceID(), 0f, 360f, 3f, TweenScaleFunctions.CubicEaseInOut, delegate(ITween<float> t)
		{
			base.gameObject.transform.rotation = Quaternion.identity;
			base.gameObject.transform.Rotate(Camera.main.transform.forward, t.CurrentValue);
			if (t.CurrentProgress > 0.5f && !party.shouldFlow)
			{
				party.shouldFlow = true;
				party.StartFlowing();
				party.DrawWater();
			}
		}, delegate
		{
		});
		//FirebaseAnalytics.LogEvent("startvalve_" + Intent.world.Key() + "_" + Intent.level_id);
	}

	public void Close()
	{
		opened = false;
		base.gameObject.Tween("RotateValve" + base.gameObject.GetInstanceID(), 0f, 360f, 3f, TweenScaleFunctions.CubicEaseInOut, delegate(ITween<float> t)
		{
			base.gameObject.transform.rotation = Quaternion.identity;
			base.gameObject.transform.Rotate(Camera.main.transform.forward, t.CurrentValue);
			if (t.CurrentProgress > 0.5f && party.shouldFlow)
			{
				party.shouldFlow = false;
				party.StopFlowing();
				party.DrawWater();
			}
		}, delegate
		{
		});
		//FirebaseAnalytics.LogEvent("stopvalve_" + Intent.world.Key() + "_" + Intent.level_id);
	}

	private void OnMouseDown()
	{
		if (opened)
		{
			Close();
		}
		else
		{
			Open();
		}
	}
}
