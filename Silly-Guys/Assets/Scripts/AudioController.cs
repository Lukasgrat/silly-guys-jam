using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public static AudioController Instance;
    public AudioClip[] sounds, sfxSounds;
    public AudioSource soundSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        AudioClip a = Array.Find(sounds, x => x.name == name);

        if (a == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            soundSource.clip = a;
            soundSource.Play();
        }
    }

    public void PlaySfx(string name)
    {
        AudioClip a = Array.Find(sfxSounds, x => x.name == name);

        if (a == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            sfxSource.clip = a;
            sfxSource.Play();
        }
    }

    public void volume(float volume)
    {
        soundSource.volume = volume;
        sfxSource.volume = volume;
    }
}
