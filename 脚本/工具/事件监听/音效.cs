using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "工具/音效")]
public class 音效 : ScriptableObject
{
    public UnityAction<AudioClip> 事件调用;
    public void 获得攻击音效(AudioClip AudioClip)
    {
        事件调用?.Invoke(AudioClip);
    }

}
