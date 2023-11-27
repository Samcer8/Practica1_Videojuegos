using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudiosFX
{
    pistol_shot,
    pistol_reload
}

public class OldSoundManager : MonoBehaviour
{
    public static OldSoundManager Instance { get; private set; }

    [SerializeField] private List<AudioClip> clipList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayAudioClip(AudioClip audioClip, AudioSource audioSource)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayAudioSource(AudioSource audioSource)
    {
        audioSource.Play();
    }

    public void PlayFx(AudiosFX audioFX, AudioSource audioSource)
    {
        audioSource.PlayOneShot(clipList[(int)audioFX]);
    }

    public void MuteAudioSource(AudioSource audioSource)
    {
        audioSource.mute = true;
    }

    public void UnmuteAudioSource(AudioSource audioSource)
    {
        audioSource.mute = false;
    }

    public void ToggleAudioSource(AudioSource audioSource)
    {
        audioSource.mute = !audioSource.mute;
    }
}
