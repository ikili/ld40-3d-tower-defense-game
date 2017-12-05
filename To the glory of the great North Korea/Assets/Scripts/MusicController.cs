using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
	public static MusicController Instance;
	public float fadeSpeed = 0.1f;
	public AudioSource bgMusicSource;
	public AudioSource soundEffectsSource;
	[Header("Background Music")]
	public AudioClip[] bgMusic;
	[Header("Sound Effects")]
	public SoundEffects[] soundEffects;

	void Start()
	{
		bgMusicSource.volume = 0f;
		PlayMusic();
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
		}
	}

	public void StopSoundEffects()
	{
		soundEffectsSource.Stop();
	}

	public void PlayMusic()
	{
		bgMusicSource.clip = bgMusic[Random.Range(0, bgMusic.Length)];
		bgMusicSource.Play();
		StartCoroutine(FadeIn(bgMusicSource));
	}

	public void StopMusic()
	{
		StartCoroutine(FadeOut(bgMusicSource));
		bgMusicSource.Stop();
	}

	IEnumerator FadeIn(AudioSource audioSource)
	{
		while (audioSource.volume <= 1f)
		{
			audioSource.volume += Time.deltaTime;
			yield return new WaitForSeconds(fadeSpeed);
		}
	}

	IEnumerator FadeOut(AudioSource audioSource)
	{
		while (audioSource.volume >= 1f)
		{
			audioSource.volume -= Time.deltaTime;
			yield return new WaitForSeconds(fadeSpeed);
		}
	}
}
