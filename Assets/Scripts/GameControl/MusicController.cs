using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    [SerializeField]
    private SerializedDictionary<string, AudioClip> clips = new SerializedDictionary<string, AudioClip>();

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Director.Instance.ElementReadEvent += OnElementRead;
    }

    private void OnElementRead(string name, ChapterElement element)
    {
        if (name == "Music")
        {
            Music music = element as Music;
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            if (clips.ContainsKey(music.name))
            {
                audioSource.clip = clips[music.name];
                audioSource.Play();
            }
        }
    }
}
