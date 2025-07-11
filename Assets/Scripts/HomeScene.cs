

using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class HomeScene : MonoBehaviour
{
	private void Start()
	{
		//GameObject.Find("BuyButton").SetActive(!Purchaser.HasRemovedAds() || !Purchaser.HasExtraLevels());
		GameObject.Find("MusicToggle").GetComponent<Toggle>().isOn = !MusicManager.IsMusicMuted();
		GameObject.Find("FXToggle").GetComponent<Toggle>().isOn = !MusicManager.IsFXMuted();
	}

	private void Update()
	{
	}

	public void OnClickPlay()
	{
		CameraScene.ChangeScene("WorldSelection");
	}

	public void OnClickToggleMusic()
	{
		MusicManager.toggleMusic(GameObject.Find("MusicToggle").GetComponent<Toggle>().isOn);
	}

	public void OnClickToggleFX()
	{
		MusicManager.toggleFX(GameObject.Find("FXToggle").GetComponent<Toggle>().isOn);
	}

	public void OnClickInvite()
	{
		//Invite invite = new Invite();
		//invite.TitleText = "Invite to play Plumber";
		//invite.MessageText = "Please try my new game";
		//invite.CallToActionText = "Download it for FREE";
		//invite.DeepLinkUrl = new Uri("https://play.google.com");
		//Invite invite2 = invite;
		
	}

	public void OnClickFruitSalad()
	{
		Application.OpenURL("https://play.google.com");
	}
}
