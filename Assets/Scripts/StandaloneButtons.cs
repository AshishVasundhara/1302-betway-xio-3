using UnityEngine;

public class StandaloneButtons : MonoBehaviour
{
	public void OnClickBackButton()
	{
		Object.FindObjectOfType<CameraScene>().GoBackScene();
	}
}
