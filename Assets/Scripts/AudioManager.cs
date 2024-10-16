using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip; // 브금
    public float bgmVolume; //소리크기
    AudioSource bgmPlayer; // 소리의 주체

    [Header("#SFX")]
    public AudioClip[] sfxClips; // 효과음들
    public float sfxVolume; // 효과음 크기
    public int channels; // 한번에 실행되는 효과음의 수
    AudioSource[] sfxPlayers; 
    int channelIndex;

    public enum Sfx { Click1, Click2, Click3, error, genButton1, genButton2, genButton3, genButton4, success1, success2, money }
    //위의 enum 배열에 사용할 효과음들을 추가해서 쓰면 됨

    private void Awake()
    {
        instance = this;
        Init();
    }

    void Init() // 이건 초기화함수라 건들일 없을 거임 아마
    {
        //bgm
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = true;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        //sfx
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;

            sfxPlayers[i].volume = sfxVolume;
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
            bgmPlayer.Play();
        else
            bgmPlayer.Stop();   
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
