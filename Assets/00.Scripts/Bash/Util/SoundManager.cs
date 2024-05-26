using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _aud;

    public void AudioPlayer(AudioClip clip)
    {
        _aud.PlayOneShot(clip);
    }

    public void AudioLoopPlayer(AudioClip clip)
    {
        //루프가 켜져있는 오디오소스를 풀링하는 거 밍... 
    }
}
