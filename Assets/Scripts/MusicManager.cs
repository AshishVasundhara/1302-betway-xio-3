using UnityEngine;

public class MusicManager : MonoBehaviour
{
	public AudioClip music;

	public AudioClip coin;

	public AudioClip touched;

	public AudioClip ouWheep;

	public AudioClip psiou;

	private static MusicManager instance;

	public static MusicManager Instance => instance;

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		instance = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Start()
	{
		GetComponent<AudioSource>().clip = music;
		GetComponent<AudioSource>().volume = 1f;
		toggleMusic(!IsMusicMuted());
		toggleFX(!IsFXMuted());
	}

	public void PlayCoin()
	{
		if (!IsFXMuted())
		{
			GetComponent<AudioSource>().PlayOneShot(coin, 4f);
		}
	}

	public void PlayTouched()
	{
		if (!IsFXMuted())
		{
			GetComponent<AudioSource>().PlayOneShot(touched, 4f);
		}
	}

	public void PlayOuWheep()
	{
		if (!IsFXMuted())
		{
			GetComponent<AudioSource>().PlayOneShot(ouWheep, 4f);
		}
	}

	public void PlayPsiou()
	{
		if (!IsFXMuted())
		{
			GetComponent<AudioSource>().PlayOneShot(psiou, 4f);
		}
	}

	public static bool IsFXMuted()
	{
		return PlayerPrefs.GetInt("fx_mute", 0) == 1;
	}

	public static bool IsMusicMuted()
	{
		return PlayerPrefs.GetInt("mute", 0) == 1;
	}

	public static void toggleMusic(bool on)
	{
		AudioSource component = GameObject.Find("MusicManager").GetComponent<AudioSource>();
		if (!on)
		{
			component.Stop();
			PlayerPrefs.SetInt("mute", 1);
		}
		else
		{
			component.Play();
			PlayerPrefs.SetInt("mute", 0);
		}
	}

	public static void toggleFX(bool on)
	{
		AudioSource component = GameObject.Find("MusicManager").GetComponent<AudioSource>();
		if (!on)
		{
			PlayerPrefs.SetInt("fx_mute", 1);
		}
		else
		{
			PlayerPrefs.SetInt("fx_mute", 0);
		}
	}

	private void Update()
	{
	}
}
