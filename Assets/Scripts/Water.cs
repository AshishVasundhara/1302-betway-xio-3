using UnityEngine;

public class Water : MonoBehaviour
{
	public Color[] colors;

	private float nextChange;

	private void Start()
	{
	}

	private void Update()
	{
		if (Time.time > nextChange)
		{
			GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
			nextChange = Time.time + UnityEngine.Random.Range(0.01f, 0.1f);
		}
	}
}
