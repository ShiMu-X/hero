using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 音乐播放 : MonoBehaviour
{
    public 音效 音效;
    public AudioClip 音乐;
    public bool 播放音乐;
    private void OnEnable()
    {
        if(播放音乐)
        {
            播放音效();
        }
    }
    public void 播放音效()
    {
        音效.事件调用(音乐);
    }
}
