using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    

    public static AudioManager instance = null;

    public AudioSource audio;
    private bool stop = false;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        audio = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }
    
    private void Update()
    {
        if (stop)
        {
            audio.volume -= Time.deltaTime * 0.05f;
        }
    }
    public void ChangeMusic(AudioClip clip, float volumen)
    {
        stop = false;
        audio.Stop();
        audio.clip = clip;
        audio.volume = volumen;
        audio.Play();
    }

    public void EndMusic()
    {
        stop = true;
    }
}
