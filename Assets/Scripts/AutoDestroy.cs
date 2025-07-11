using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
	public float delay;

	private void Start()
	{
		UnityEngine.Object.Destroy(base.gameObject, delay);
	}

	private void Update()
	{
	}
}
