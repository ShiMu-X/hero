using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class 音乐 : MonoBehaviour
{
    public GameObject 开启背景音乐;
    public 音效 bgm;
    public 音效 攻击;
    public AudioSource 背景音乐;
    public AudioSource 攻击音效;
    private void OnEnable()
    {
        攻击.事件调用 += 播放攻击音效;
        bgm.事件调用 += 播放背景音乐;
    }
    private void OnDisable()
    {
        攻击.事件调用 -= 播放攻击音效;
        bgm.事件调用 -= 播放背景音乐;

    }

    private void 播放背景音乐(AudioClip bgm)
    {
        背景音乐.clip= bgm;
        背景音乐.Play();

    }

    private void 播放攻击音效(AudioClip 音效)
    {
        攻击音效.clip = 音效;
        攻击音效.Play();
    }
}
