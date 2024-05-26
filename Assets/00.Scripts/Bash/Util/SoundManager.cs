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
        //������ �����ִ� ������ҽ��� Ǯ���ϴ� �� ��... 
    }
}
