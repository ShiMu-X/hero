using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class 相机跟随 : MonoBehaviour
{
    CinemachineConfiner2D cc2;
    public CinemachineImpulseSource ccsi;
    public 相机震动 相机震动;
    private void Awake()
    {
        cc2=GetComponent<CinemachineConfiner2D>();
    }
    private void OnEnable()
    {
        相机震动.事件调用 += 执行相机震动;
    }
    private void OnDisable()
    {
        相机震动.事件调用 -= 执行相机震动;

    }


    void Start()
    {
        获取相机边界();
    }
    public void 获取相机边界()
    {
        var 相机边界 = GameObject.FindGameObjectWithTag("相机边界");
        if(相机边界==null)
        {
            return;
        }
        cc2.m_BoundingShape2D= 相机边界.GetComponent<PolygonCollider2D>();
        cc2.InvalidateCache();
    }
    private void 执行相机震动()
    {
        ccsi.GenerateImpulse();
    }

}
