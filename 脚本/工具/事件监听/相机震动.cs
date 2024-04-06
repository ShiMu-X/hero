using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "工具/相机震动")]
public class 相机震动 : ScriptableObject
{
    public UnityAction 事件调用;
    public void 获得相机震动()
    {
        事件调用?.Invoke();
    }

}
