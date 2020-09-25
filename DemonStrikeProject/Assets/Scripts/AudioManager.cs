using System;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

    public Sound[] sounds;

	private void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound sound in sounds) {
			sound.source = gameObject.AddComponent<AudioSource>();
			sound.source.clip = sound.clip;

			sound.source.volume = sound.volume;
			sound.source.pitch = sound.pitch;
		}
	}

	public void Play(string sound) {
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null) {
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		s.source.volume = s.volume * (1f + UnityEngine.Random.RandomRange(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.RandomRange(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}
}
