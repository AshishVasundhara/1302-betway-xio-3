using DigitalRuby.Tween;
using System;
using UnityEngine;

public class Pipe : MonoBehaviour
{
	public enum Kind
	{
		LINE,
		ANGLE
	}

	public enum Position
	{
		LEFT,
		RIGHT,
		UP,
		DOWN,
		ERROR
	}

	[Range(0f, 5f)]
	public int dx;

	[Range(0f, 7f)]
	public int dy;

	public Kind kind;

	public Sprite spr_line;

	public Sprite spr_angle;

	private Transform board;

	private float angle = 360f;

	private Party party;

	private bool first;

	private bool last;

	private Transform waterIn;

	private Vector2 startDragging;

	private bool dragging;

	private float angleWhenStartDragging;

	private float angleWithFingerWhenStartDragging = -1f;

	internal void SetFirst()
	{
		first = true;
	}

	internal void SetLast()
	{
		last = true;
	}

	internal static Pipe GetByDiscreteCoordinate(int dx, int dy)
	{
		Pipe[] array = UnityEngine.Object.FindObjectsOfType<Pipe>();
		foreach (Pipe pipe in array)
		{
			if (pipe.dx == dx && pipe.dy == dy)
			{
				return pipe;
			}
		}
		return null;
	}

	internal static Vector3 DiscreteToContinuous(int x, int y)
	{
		return new Vector3(GetContinuousX(x), GetContinuousY(y), 0f);
	}

	internal Position OutputDirection()
	{
		switch (kind)
		{
		case Kind.LINE:
			if (IsUpLinked())
			{
				return Position.DOWN;
			}
			if (IsDownLinked())
			{
				return Position.UP;
			}
			if (IsLeftLinked())
			{
				return Position.RIGHT;
			}
			if (IsRightLinked())
			{
				return Position.LEFT;
			}
			return Position.ERROR;
		case Kind.ANGLE:
			if (IsUpLinked())
			{
				if (IsLeftLinkable())
				{
					return Position.LEFT;
				}
				return Position.RIGHT;
			}
			if (IsDownLinked())
			{
				if (IsLeftLinkable())
				{
					return Position.LEFT;
				}
				return Position.RIGHT;
			}
			if (IsLeftLinked())
			{
				if (IsUpLinkable())
				{
					return Position.UP;
				}
				return Position.DOWN;
			}
			if (IsRightLinked())
			{
				if (IsUpLinkable())
				{
					return Position.UP;
				}
				return Position.DOWN;
			}
			return Position.ERROR;
		default:
			return Position.ERROR;
		}
	}

	internal bool IsLast()
	{
		return last;
	}

	internal bool IsLinkedWith(Pipe other)
	{
		return (IsDownLinkable() && other.IsUpLinkable()) || (IsUpLinkable() && other.IsDownLinkable()) || (IsLeftLinkable() && other.IsRightLinkable()) || (IsRightLinkable() && other.IsLeftLinkable());
	}

	internal void HideWater()
	{
		waterIn.gameObject.SetActive(value: false);
	}

	internal void ShowWater()
	{
		waterIn.gameObject.SetActive(value: true);
	}

	private void Start()
	{
		base.gameObject.Tween("RotatePipe" + base.gameObject.GetInstanceID(), 0f, angle, 1f, TweenScaleFunctions.CubicEaseInOut, delegate(ITween<float> t)
		{
			base.transform.rotation = Quaternion.identity;
			base.transform.Rotate(Camera.main.transform.forward, t.CurrentValue);
		}, delegate
		{
		});
	}

	private void Update()
	{
		if (dragging && !HasDraggedFew())
		{
			Vector2 normalized = ((Vector2)Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition) - (Vector2)base.transform.position).normalized;
			if (angleWithFingerWhenStartDragging == -1f)
			{
				angleWithFingerWhenStartDragging = Vector2.Angle(base.transform.up, normalized);
			}
			float num = 57.29578f * Mathf.Atan2(normalized.y, normalized.x) - angleWithFingerWhenStartDragging;
			base.transform.up = new Vector2(Mathf.Cos((float)Math.PI / 180f * num), Mathf.Sin((float)Math.PI / 180f * num));
		}
	}

	public static Pipe Create(Kind kind, int _dx, int _dy)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Pipe")) as GameObject;
		Pipe component = gameObject.GetComponent<Pipe>();
		component.dx = _dx;
		component.dy = _dy;
		component.kind = kind;
		component.angle = UnityEngine.Random.Range(0, 4) * 90;
		gameObject.transform.parent = GameObject.Find("Board").transform;
		component.OnValidate();
		return component;
	}

	private void Awake()
	{
		OnValidate();
	}

	private void OnValidate()
	{
		board = base.transform.parent;
		party = UnityEngine.Object.FindObjectOfType<Party>();
		base.transform.localPosition = new Vector2(GetContinuousX(dx), GetContinuousY(dy));
		Sprite sprite = null;
		switch (kind)
		{
		case Kind.LINE:
			sprite = spr_line;
			break;
		case Kind.ANGLE:
			sprite = spr_angle;
			break;
		}
		GetComponent<SpriteRenderer>().sprite = sprite;
		switch (kind)
		{
		case Kind.ANGLE:
			waterIn = base.transform.Find("anglew2");
			base.transform.Find("linew2").gameObject.SetActive(value: false);
			break;
		case Kind.LINE:
			waterIn = base.transform.Find("linew2");
			base.transform.Find("anglew2").gameObject.SetActive(value: false);
			break;
		}
	}

	public static float GetContinuousX(float _dx)
	{
		return _dx * 1.2f - 3.6f + 0.6f;
	}

	public static float GetContinuousY(float _dy)
	{
		return _dy * 1.2f - 4.8f + 0.6f;
	}

	private void OnMouseUp()
	{
		if (!party.over)
		{
			if (HasDraggedFew())
			{
				TurnCounterClock();
			}
			else
			{
				MatchClosestAngle();
			}
			dragging = false;
			angleWithFingerWhenStartDragging = -1f;
		}
	}

	private void MatchClosestAngle()
	{
		Vector3 eulerAngles = base.transform.eulerAngles;
		UnityEngine.Debug.Log(eulerAngles.z);
		Vector3 eulerAngles2 = base.transform.eulerAngles;
		angle = Mathf.Round(eulerAngles2.z / 90f) * 90f;
		MoveToAngle(angle);
	}

	private bool HasDraggedFew()
	{
		return Vector2.Distance(startDragging, Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition)) < 0.2f;
	}

	private void OnMouseDown()
	{
		startDragging = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
		dragging = true;
		party.StopFlowing();
		Vector3 eulerAngles = base.transform.eulerAngles;
		angleWhenStartDragging = eulerAngles.z;
	}

	private void TurnCounterClock()
	{
		angle += 90f;
		while (angle > 360f)
		{
			angle -= 360f;
		}
		MoveToAngle(angle);
	}

	private void MoveToAngle(float angle)
	{
		float time = 0.5f;
		Vector3 eulerAngles = base.transform.rotation.eulerAngles;
		float z = eulerAngles.z;
		base.gameObject.Tween("RotatePipe" + base.gameObject.GetInstanceID(), z, angle, time, TweenScaleFunctions.CubicEaseInOut, delegate(ITween<float> t)
		{
			base.transform.rotation = Quaternion.identity;
			base.transform.Rotate(Camera.main.transform.forward, t.CurrentValue);
		}, delegate
		{
			CheckLinks();
			party.UpdateWater();
			party.StartFlowing();
		});
		base.gameObject.Tween("ScaleUp" + base.gameObject.GetInstanceID(), 1f, 1.1f, time / 2f, TweenScaleFunctions.CubicEaseInOut, delegate(ITween<float> t)
		{
			base.transform.localScale = new Vector2(t.CurrentValue, t.CurrentValue);
		}, delegate
		{
			base.gameObject.Tween("ScaleDown" + base.gameObject.GetInstanceID(), 1.1f, 1f, time / 2f, TweenScaleFunctions.CubicEaseInOut, delegate(ITween<float> t3)
			{
				base.transform.localScale = new Vector2(t3.CurrentValue, t3.CurrentValue);
			}, delegate
			{
			});
		});
		UnityEngine.Object.FindObjectOfType<MusicManager>().PlayOuWheep();
	}

	public void Highlight()
	{
		base.gameObject.Tween("aScaleUp" + base.gameObject.GetInstanceID(), 1f, 1.1f, 0.5f, TweenScaleFunctions.CubicEaseInOut, delegate(ITween<float> t)
		{
			base.transform.localScale = new Vector2(t.CurrentValue, t.CurrentValue);
		}, delegate
		{
			base.gameObject.Tween("aScaleDown" + base.gameObject.GetInstanceID(), 1.1f, 1f, 0.5f, TweenScaleFunctions.CubicEaseInOut, delegate(ITween<float> t3)
			{
				base.transform.localScale = new Vector2(t3.CurrentValue, t3.CurrentValue);
			}, delegate
			{
			});
		});
	}

	private void CheckLinks()
	{
		if (IsLeftLinked())
		{
			LinkParticle(-1f, 0f);
		}
		if (IsRightLinked())
		{
			LinkParticle(1f, 0f);
		}
		if (IsDownLinked())
		{
			LinkParticle(0f, -1f);
		}
		if (IsUpLinked())
		{
			LinkParticle(0f, 1f);
		}
	}

	private void LinkParticle(float v1, float v2)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("LinkParticle"), new Vector3(GetContinuousX((float)dx + v1 / 2f), GetContinuousY((float)dy + v2 / 2f), -5f), Quaternion.identity) as GameObject;
		if (v1 == 0f)
		{
			gameObject.transform.eulerAngles = new Vector3(0f, 0f, 90f);
		}
		gameObject.transform.SetParent(board.transform, worldPositionStays: false);
	}

	private bool IsLeftLinked()
	{
		return IsLeftLinkable() && IsLeftNeighborLinkable();
	}

	private bool IsLeftNeighborLinkable()
	{
		Pipe pipe = Neighbor(-1, 0);
		if (pipe == null)
		{
			return false;
		}
		return pipe.IsRightLinkable();
	}

	private Pipe Neighbor(int x, int y)
	{
		Pipe[] array = UnityEngine.Object.FindObjectsOfType<Pipe>();
		foreach (Pipe pipe in array)
		{
			if (pipe.dx == dx + x && pipe.dy == dy + y)
			{
				return pipe;
			}
		}
		return null;
	}

	private bool IsRightNeighborLinkable()
	{
		Pipe pipe = Neighbor(1, 0);
		if (pipe == null)
		{
			return false;
		}
		return pipe.IsLeftLinkable();
	}

	private bool IsDownNeighborLinkable()
	{
		Pipe pipe = Neighbor(0, -1);
		if (pipe == null)
		{
			return false;
		}
		return pipe.IsUpLinkable();
	}

	private bool IsUpNeighborLinkable()
	{
		Pipe pipe = Neighbor(0, 1);
		if (pipe == null)
		{
			return false;
		}
		return pipe.IsDownLinkable();
	}

	private bool IsRightLinked()
	{
		return IsRightLinkable() && IsRightNeighborLinkable();
	}

	public bool IsDownLinked()
	{
		if (last)
		{
			return IsDownLinkable();
		}
		return IsDownLinkable() && IsDownNeighborLinkable();
	}

	public bool IsUpLinked()
	{
		if (first)
		{
			return IsUpLinkable();
		}
		return IsUpLinkable() && IsUpNeighborLinkable();
	}

	public bool IsUpLinkable()
	{
		switch (kind)
		{
		case Kind.LINE:
			return IsDownLinkable();
		case Kind.ANGLE:
			return !IsDownLinkable();
		default:
			throw new Exception("Invalid kind");
		}
	}

	private bool IsDownLinkable()
	{
		switch (kind)
		{
		case Kind.LINE:
			return !IsLeftLinkable();
		case Kind.ANGLE:
			return Mathf.Abs(Mathf.DeltaAngle(angle, 0f)) < 1f || Mathf.Abs(Mathf.DeltaAngle(angle, 90f)) < 1f;
		default:
			throw new Exception("Invalid kind");
		}
	}

	private bool IsRightLinkable()
	{
		switch (kind)
		{
		case Kind.LINE:
			return IsLeftLinkable();
		case Kind.ANGLE:
			return !IsLeftLinkable();
		default:
			throw new Exception("Invalid kind");
		}
	}

	private bool IsLeftLinkable()
	{
		switch (kind)
		{
		case Kind.LINE:
			return angle % 180f == 0f;
		case Kind.ANGLE:
			return Mathf.Abs(Mathf.DeltaAngle(angle, 0f)) < 1f || Mathf.Abs(Mathf.DeltaAngle(angle, 270f)) < 1f;
		default:
			throw new Exception("Invalid kind");
		}
	}

	public Pipe Next(Pipe previous)
	{
		if (IsLeftLinked() && Neighbor(-1, 0) != previous)
		{
			return Neighbor(-1, 0);
		}
		if (IsRightLinked() && Neighbor(1, 0) != previous)
		{
			return Neighbor(1, 0);
		}
		if (IsUpLinked() && Neighbor(0, 1) != previous)
		{
			return Neighbor(0, 1);
		}
		if (IsDownLinked() && Neighbor(0, -1) != previous)
		{
			return Neighbor(0, -1);
		}
		return null;
	}

	private Position RelativePosition(Pipe other)
	{
		if (other == null)
		{
			return Position.UP;
		}
		if (other.dx < dx)
		{
			return Position.LEFT;
		}
		if (other.dx > dx)
		{
			return Position.RIGHT;
		}
		if (other.dy < dy)
		{
			return Position.DOWN;
		}
		if (other.dy > dy)
		{
			return Position.UP;
		}
		return Position.UP;
	}

	public string ToString()
	{
		return "[" + dx + "," + dy + "]";
	}
}
