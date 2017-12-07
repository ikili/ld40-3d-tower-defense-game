using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicController : MonoBehaviour
{
	public static MusicController Instance;
	public float fadeTime = 2f;
	public float fadeInDelay = 0.5f;
	[HideInInspector]
	public bool startedPlaying = false;
	[HideInInspector]
	public bool settingsChanged = false;
	public AudioSource bgMusicSource;
	public AudioSource soundEffectsSource;
	[Header("Background Music")]
	public AudioClip[] bgMusic;
	[Header("Sound Effects")]
	public SoundEffects[] soundEffects;

	private string lastSfxPlayed;
	[HideInInspector]
	public  bool isFading = true;

	void Update()
	{
		CheckVolumeSettings();
	}

	void CheckVolumeSettings()
	{
		if (soundEffectsSource.volume != GameSettings.MasterVolume)
		{
			soundEffectsSource.volume = GameSettings.MasterVolume;
		}
		if (settingsChanged == true)
		{
			bgMusicSource.volume = GameSettings.MasterVolume;
			settingsChanged = false;
		}
		if (startedPlaying == false)
		{
			GameManager.Instance.Load();
			if (SceneManager.GetActiveScene().name == "MainMenu")
			{
				bgMusicSource.clip = bgMusic[0];
			}
			else
			{
				bgMusicSource.clip = bgMusic[Random.Range(1, bgMusic.Length)];
			}
			startedPlaying = true;
			Invoke("PlayMusic", fadeInDelay);
		}
	}

	void Awake()
	{
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}

	public void PlaySoundEffects(string sfxName)
	{
		sfxName.ToLower();

		AudioClip sfxToPlay = null;

		if (lastSfxPlayed != sfxName && sfxName != "underattack")
		{
			StopSoundEffects();
		}

		if (sfxName == "build")
		{
			sfxToPlay = soundEffects[0].clips[Random.Range(0, soundEffects[0].clips.Length)];
		}
		else if (sfxName == "cantbuild")
		{
			sfxToPlay = soundEffects[1].clips[Random.Range(0, soundEffects[1].clips.Length)];
		}
		else if (sfxName == "underattack")
		{
			sfxToPlay = soundEffects[2].clips[Random.Range(0, soundEffects[2].clips.Length)];
		}
		else if (sfxName == "nomoney")
		{
			sfxToPlay = soundEffects[3].clips[Random.Range(0, soundEffects[3].clips.Length)];
		}
		else if (sfxName == "upgrade")
		{
			sfxToPlay = soundEffects[4].clips[Random.Range(0, soundEffects[4].clips.Length)];
		}
		else if (sfxName == "sell")
		{
			sfxToPlay = soundEffects[5].clips[Random.Range(0, soundEffects[5].clips.Length)];
		}
		else
		{
			sfxToPlay = null;
		}
		if (sfxToPlay != null && soundEffectsSource.isPlaying == false)
		{
			soundEffectsSource.clip = sfxToPlay;
			soundEffectsSource.Play();
			lastSfxPlayed = sfxName;
		}
	}

	public void StopSoundEffects()
	{
		soundEffectsSource.Stop();
	}

	public void PlayMusic()
	{
		bgMusicSource.volume = 0f;
		bgMusicSource.Play();
		StartCoroutine(FadeIn(bgMusicSource));
	}

	public void StopMusic()
	{
		StartCoroutine(FadeOut(bgMusicSource));
	}

	IEnumerator FadeIn(AudioSource audioSource)
	{
		isFading = true;
		while (audioSource.volume <= GameSettings.MasterVolume)
		{
			audioSource.volume += Time.deltaTime / fadeTime;
			yield return 0;
		}
		isFading = false;
	}

	IEnumerator FadeOut(AudioSource audioSource)
	{
		isFading = true;
		while (audioSource.volume >= 0.001f)
		{
			audioSource.volume -= Time.deltaTime / fadeTime;
			yield return 0;
		}
		if (audioSource.volume <= 0.001f)
		{
			audioSource.volume = 0;
		}
		isFading = false;
		bgMusicSource.Stop();
	}
}
