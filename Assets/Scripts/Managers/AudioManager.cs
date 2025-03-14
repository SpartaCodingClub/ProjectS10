using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum Clip
{
    Music_Game,
    Music_Battle,
    Music_Boss,

    SoundFX_FootStep,
}

public class AudioManager
{
    private static readonly float MASTER_VOLUME = 0.2f;
    private static readonly float[] VOLUMES =
    {
        0.4f * MASTER_VOLUME,   // Music
        0.4f * MASTER_VOLUME,   // MusicFX
        0.6f * MASTER_VOLUME,   // SoundFX
    };

    public enum Type
    {
        Music,
        MusicFX,
        SoundFX,
        Count
    }

    private Transform transform;

    private readonly AudioSource[] audioSources = new AudioSource[(int)Type.Count];
    private readonly HashSet<AudioClip> soundClips = new();

    public void Initialize()
    {
        transform = new GameObject(nameof(AudioManager), typeof(AudioListener)).transform;
        transform.SetParent(Managers.Instance.transform);

        var names = Enum.GetNames(typeof(Type));
        for (int i = 0; i < audioSources.Length; i++)
        {
            Transform child = new GameObject(names[i]).transform;
            child.SetParent(transform);

            AudioSource audioSource = child.gameObject.AddComponent<AudioSource>();
            audioSource.loop = (Type)i != Type.SoundFX;
            audioSource.playOnAwake = false;
            audioSource.volume = VOLUMES[i];
            audioSources[i] = audioSource;
        }
    }

    public void Play(Clip key, float volumeScale = 1.0f, Transform transform = null)
    {
        Type type;
        try
        {
            type = GetType(key.ToString());
        }
        catch
        {
            Debug.LogWarning($"Failed to GetType({key})");
            return;
        }

        string path = $"{Define.PATH_AUDIO}/{key}";
        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip == null)
        {
            Debug.LogWarning($"Failed to Load<AudioClip>({key})");
            return;
        }

        AudioSource audioSource = audioSources[(int)type];
        switch (type)
        {
            case Type.Music:
                Play_Music(audioSource, clip, volumeScale);
                break;
            case Type.MusicFX:
                Debug.LogWarning($"Failed to Play({key})");
                break;
            case Type.SoundFX:
                Play_SoundFX(audioSource, clip, volumeScale, transform);
                break;
        }
    }

    private void Play_Music(AudioSource audioSource, AudioClip clip, float volumeScale)
    {
        if (audioSource.clip != null)
        {
            Play_MusicFX(audioSource.clip, audioSource.time, audioSource.volume);
        }

        audioSource.clip = clip;
        audioSource.DOFade(VOLUMES[(int)Type.Music] * volumeScale, 2.0f).From(0.0f);
        audioSource.Play();
    }

    private void Play_MusicFX(AudioClip clip, float time, float volume)
    {
        AudioSource audioSource = audioSources[(int)Type.MusicFX];
        audioSource.clip = clip;
        audioSource.time = time;
        audioSource.volume = volume;
        audioSource.DOFade(0.0f, 1.0f).OnComplete(() => audioSource.Stop());
        audioSource.Play();
    }

    private void Play_SoundFX(AudioSource audioSource, AudioClip clip, float volumeScale, Transform transform = null)
    {
        if (soundClips.Add(clip) == false)
        {
            return;
        }

        if (transform == null)
        {
            audioSource.PlayOneShot(clip, volumeScale);
        }
        else
        {
            Managers.Resource.Instantiate("AudioSource3D", transform.position).GetComponent<AudioSourceHandler>().PlayOneShot(clip, volumeScale);
        }

        DOVirtual.DelayedCall(0.1f, () => soundClips.Remove(clip));
    }

    private Type GetType(string key)
    {
        string type = key[..key.IndexOf('_')];
        return (Type)Enum.Parse(typeof(Type), type);
    }
}