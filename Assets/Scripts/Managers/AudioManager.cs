using System;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Class that handles the loading and playing of game audio.
/// </summary>
public sealed class AudioManager : Manager<AudioManager>
{
    [SerializeField] private SoundSettings[] sounds;
    [SerializeField] private AudioMixerGroup soundMixerGroup;

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        InitialiseGameAudio();
    }

    // Creates a unique AudioSource for each sound to allow for overlapping fx
    private void InitialiseGameAudio()
    {
        foreach (SoundSettings sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.outputAudioMixerGroup = soundMixerGroup;
            sound.source.volume = sound.volume;
        }
    }

    /// <summary>
    /// Plays an audio clip from id (if it exists) at a certain pitch (default 1)
    /// </summary>
    /// <param name="soundClip"></param>
    /// <param name="pitch"></param>
    public void PlayAudio(Sound soundClip, float pitch = 1)
    {
        SoundSettings sound = Array.Find(sounds, t => t.id == soundClip);

        if (sound == null)
        {
            Debug.LogError($"Sound {sound} not found!");
        }

        sound.source.pitch = pitch;
        sound.source.Play();
    }
}