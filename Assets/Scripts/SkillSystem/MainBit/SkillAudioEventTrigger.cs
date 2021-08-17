using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton handling the behaviour of two audio sources which allow to 
/// cast skills and use them simultaneously without any missing sound.
/// </summary>
public class SkillAudioEventTrigger : MonoBehaviour
{
    public AudioSource source;
    public AudioSource castingSource;

    public static SkillAudioEventTrigger Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Plays the specified non-casting sound.
    /// </summary>
    /// <param name="_clip">Sound to play</param>
    public void PlaySound(AudioClip _clip)
    {
        if (!source.isPlaying)
        {
            source.clip = _clip;
            source.Play();
        }
    }
    /// <summary>
    /// Plays the specified casting sound.
    /// </summary>
    /// <param name="_clip">Sound to play</param>
    public void PlayCastingSound(AudioClip _clip)
    {
        castingSource.clip = _clip;
        castingSource.Play();
    }
}
