using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI管理 : MonoBehaviour
{
    public 状态栏 状态栏;
    [Header("事件监听")]
    public 获取属性 监听变化;
    private void OnEnable()
    {
        监听变化.事件调用 += 事件调用;
    }


    private void OnDisable()
    {
        监听变化.事件调用 -= 事件调用;
    }
    private void 事件调用(属性 属性)
    {
        var 血量=属性.当前hp/属性.最大hp;
        状态栏.血量更新(血量);
    }

}
