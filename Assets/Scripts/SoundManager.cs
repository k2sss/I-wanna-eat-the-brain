using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SoundManager : BaseMonoManager<SoundManager>
{
    public AudioSource audioSource;
    public AudioSource musicAudioSource;
    public void PlayRandomSound(AudioClip[] clips,float volume = 1f)
    {
        int rand = Random.Range(0, clips.Length);
        audioSource.PlayOneShot(clips[rand],volume);
    }
    public void PlaySound(AudioClip clip,float volume = 1f)
    {
        if (clip == null) return;
        audioSource.PlayOneShot(clip, volume);
    }
    public void PlaySound(string Path, float volume = 1f)
    {
        AudioClip clip = Resources.Load<AudioClip>(Path);
        audioSource.PlayOneShot(clip, volume);
    }
    public void PlayRandomMusic(AudioClip[] clips, float volume = 1f,bool isLoop = true)
    {
        PlayMusic(clips[Random.Range(0, clips.Length)], volume, isLoop);
    }
    public void PlayMusic(AudioClip clip, float volume = 1f, bool isLoop = true)
    {

        musicAudioSource.clip = clip;
        musicAudioSource.volume = volume;
        musicAudioSource.loop = isLoop;
        musicAudioSource.Play();
    }
    
}
