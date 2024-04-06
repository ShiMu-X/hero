using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "工具/获取属性")]
public class 获取属性 : ScriptableObject
{
    public UnityAction<属性> 事件调用;

    public void 获得属性(属性 属性)
    {
        事件调用?.Invoke(属性);
    }
}
