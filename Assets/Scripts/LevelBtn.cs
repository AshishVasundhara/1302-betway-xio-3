using UnityEngine;

public class LevelBtn : MonoBehaviour
{
	public int count;

	private Level l;

	private float startMousePressed;

	private void Start()
	{
		l = Intent.world.Get(count);
		base.transform.Find("level_nbr").GetComponent<TextMesh>().text = count + string.Empty;
		if (Intent.world.bestPassed() + 1 != count)
		{
			if (Intent.world.bestPassed() + 1 < count)
			{
				Color color = GetComponent<SpriteRenderer>().color;
				color.a = 0.2f;
				GetComponent<SpriteRenderer>().color = color;
			}
			else
			{
				Color color2 = GetComponent<SpriteRenderer>().color;
				color2.r = 0.9f;
				color2.g = 0.9f;
				color2.b = 0.9f;
				GetComponent<SpriteRenderer>().color = color2;
			}
		}
	}
}
