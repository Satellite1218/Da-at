using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip; // ���
    public float bgmVolume; //�Ҹ�ũ��
    AudioSource bgmPlayer; // �Ҹ��� ��ü

    [Header("#SFX")]
    public AudioClip[] sfxClips; // ȿ������
    public float sfxVolume; // ȿ���� ũ��
    public int channels; // �ѹ��� ����Ǵ� ȿ������ ��
    AudioSource[] sfxPlayers; 
    int channelIndex;

    public enum Sfx { Click1, Click2, Click3, error, genButton1, genButton2, genButton3, genButton4, success1, success2, money }
    //���� enum �迭�� ����� ȿ�������� �߰��ؼ� ���� ��

    private void Awake()
    {
        instance = this;
        Init();
    }

    void Init() // �̰� �ʱ�ȭ�Լ��� �ǵ��� ���� ���� �Ƹ�
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
