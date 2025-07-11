
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Party : MonoBehaviour
{
	private bool fullyConnected;

	private GameObject water;

	private Pipe first;

	public bool shouldFlow;

	public bool over;

	private void Start()
	{
		LoadLevel();
		GameObject.Find("LevelText").GetComponent<Text>().text = string.Format("Level {0,3}", Intent.level_id);
		water = GameObject.Find("Water").gameObject;
		int start = Intent.GetLevel().start;
		int dy = 7;
		first = Pipe.GetByDiscreteCoordinate(start, dy);
		first.SetFirst();
		start = Intent.GetLevel().end;
		dy = 0;
		Pipe byDiscreteCoordinate = Pipe.GetByDiscreteCoordinate(start, dy);
		byDiscreteCoordinate.SetLast();
		Transform transform = GameObject.Find("lowerbarlink").transform;
		Transform transform2 = transform;
		float continuousX = Pipe.GetContinuousX(byDiscreteCoordinate.dx);
		Vector3 position = transform.position;
		transform2.position = new Vector2(continuousX, position.y);
		Transform transform3 = GameObject.Find("upperbarlink").transform;
		Transform transform4 = transform3;
		float continuousX2 = Pipe.GetContinuousX(first.dx);
		Vector3 position2 = transform3.position;
		transform4.position = new Vector2(continuousX2, position2.y);
		UpdateWater();
		GameObject.Find("valve").GetComponent<Valve>().Open();
		//FirebaseAnalytics.LogEvent("level_started_" + Intent.world.Key() + "_" + Intent.level_id);
	}

	private void LoadLevel()
	{
		for (int i = 0; i < Intent.GetLevel().grid.GetLength(1); i++)
		{
			for (int j = 0; j < Intent.GetLevel().grid.GetLength(0); j++)
			{
				switch (Intent.GetLevel().grid[j, i])
				{
				case 1:
				case 2:
					Pipe.Create(Pipe.Kind.LINE, i, j);
					break;
				case 3:
				case 4:
				case 5:
				case 6:
					Pipe.Create(Pipe.Kind.ANGLE, i, j);
					break;
				}
			}
		}
	}

	private void Update()
	{
	}

	public void UpdateWater()
	{
		Pipe pipe = GetLast(first, null);
		if (!first.IsUpLinked())
		{
			pipe = null;
		}
		if (pipe == null)
		{
			GameObject.Find("Water").transform.position = Pipe.DiscreteToContinuous(first.dx, first.dy + 1) + new Vector3(0f, -0.5f, 0f);
			water.transform.forward = Vector2.down;
			if (shouldFlow)
			{
				water.GetComponent<ParticleSystem>().Play();
			}
		}
		else if (pipe.IsLast() && pipe.IsDownLinked())
		{
			fullyConnected = true;
			water.GetComponent<ParticleSystem>().Stop();
		}
		else
		{
			fullyConnected = false;
			float num = 0.6f;
			switch (pipe.OutputDirection())
			{
			case Pipe.Position.DOWN:
				water.transform.position = pipe.transform.position + new Vector3(0f, 0f - num, 0f);
				water.transform.forward = Vector2.down;
				break;
			case Pipe.Position.UP:
				water.transform.position = pipe.transform.position + new Vector3(0f, num, 0f);
				water.transform.forward = Vector2.up;
				break;
			case Pipe.Position.LEFT:
				water.transform.position = pipe.transform.position + new Vector3(0f - num, 0f, 0f);
				water.transform.forward = Vector2.left;
				break;
			case Pipe.Position.RIGHT:
				water.transform.position = pipe.transform.position + new Vector3(num, 0f, 0f);
				water.transform.forward = Vector2.right;
				break;
			}
		}
		DrawWater();
		if (fullyConnected)
		{
			Intent.world.SetBestPassed(Intent.level_id);
			Pipe[] array = UnityEngine.Object.FindObjectsOfType<Pipe>();
			foreach (Pipe pipe2 in array)
			{
				pipe2.Highlight();
			}
			for (int j = 0; j < 3; j++)
			{
				Object.Instantiate(Resources.Load("Fireworks"), new Vector2(UnityEngine.Random.Range(-3.6f, 3.6f), UnityEngine.Random.Range(-6.4f, 6.4f)), Quaternion.identity);
			}
			StartCoroutine(_GameOver());
		}
	}

	private IEnumerator _GameOver()
	{
		if (!over)
		{
			over = true;
			Object.FindObjectOfType<MusicManager>().PlayCoin();
			yield return new WaitForSeconds(1f);
			GameOverPanel.Create();
			if (Intent.level_id == 5 && PlayerPrefs.GetInt("HasBeenAskedToRate", 0) == 0)
			{
				PlayerPrefs.SetInt("HasBeenAskedToRate", 1);
				GameObject godial = UnityEngine.Object.Instantiate(Resources.Load("YesNoDialog"), GameObject.Find("Canvas").transform) as GameObject;
				godial.transform.Find("Title").GetComponent<Text>().text = "Hello !";
				godial.transform.Find("Text").GetComponent<Text>().text = "Do you like the game ?";
				godial.transform.Find("Yes").GetComponent<Button>().onClick.AddListener(delegate
				{
					GameObject go2 = UnityEngine.Object.Instantiate(Resources.Load("YesNoDialog"), GameObject.Find("Canvas").transform) as GameObject;
					go2.transform.Find("Text").GetComponent<Text>().text = "Can you help us and rate the game with 5 stars ?";
					go2.transform.Find("Title").GetComponent<Text>().text = "Hello !";
					go2.transform.Find("Yes").GetComponent<Button>().onClick.AddListener(delegate
					{
						Application.OpenURL("https://play.google.com");
						UnityEngine.Object.Destroy(go2);
					});
					go2.transform.Find("No").GetComponent<Button>().onClick.AddListener(delegate
					{
						UnityEngine.Object.Destroy(go2);
					});
					UnityEngine.Object.Destroy(godial);
				});
				godial.transform.Find("No").GetComponent<Button>().onClick.AddListener(delegate
				{
					UnityEngine.Object.Destroy(godial);
				});
			}
			//FirebaseAnalytics.LogEvent("level_success_" + Intent.world.Key() + "_" + Intent.level_id);
		}
		else
		{
			yield return new WaitForSeconds(1f);
		}
	}

	public void DrawWater()
	{
		Pipe[] array = UnityEngine.Object.FindObjectsOfType<Pipe>();
		foreach (Pipe pipe in array)
		{
			pipe.HideWater();
		}
		if (!first.IsUpLinked() || !shouldFlow)
		{
			return;
		}
		Pipe pipe2 = first;
		Pipe pipe3 = null;
		while (pipe2 != null)
		{
			pipe2.ShowWater();
			Pipe pipe4 = pipe2.Next(pipe3);
			pipe3 = pipe2;
			pipe2 = pipe4;
			if (pipe2 != null && !pipe2.IsLinkedWith(pipe3))
			{
				pipe2 = null;
			}
		}
	}

	private Pipe GetLast(Pipe current, Pipe previous)
	{
		Pipe pipe = current.Next(previous);
		if (pipe == null)
		{
			return current;
		}
		if (pipe.IsLinkedWith(current))
		{
			return GetLast(pipe, current);
		}
		return current;
	}

	internal void StopFlowing()
	{
		water.GetComponent<ParticleSystem>().Stop();
	}

	internal void StartFlowing()
	{
		if (!fullyConnected && shouldFlow)
		{
			water.GetComponent<ParticleSystem>().Play();
		}
	}
}
