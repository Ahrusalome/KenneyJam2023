using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip startMusic;
    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        PlayMusic(startMusic);
    }

    public void PlayMusic(AudioClip clip) {
        musicSource.Stop();
        musicSource.loop = true;
        musicSource.PlayOneShot(clip);
    }
}
