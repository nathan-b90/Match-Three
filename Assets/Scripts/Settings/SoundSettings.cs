using UnityEngine;
using System;

/// <summary>
/// Settings for a game sound.
/// </summary>
[Serializable]
public sealed class SoundSettings
{
    [Tooltip("The sound identifier")]
    public Sound id;

    [Tooltip("Sound clip")]
    public AudioClip clip;

    [Tooltip("Source that plays clip"), HideInInspector]
    public AudioSource source;

    [Tooltip("Volume of the sound"), Range(0.1f, 3f)]
    public float volume = 1f;
}