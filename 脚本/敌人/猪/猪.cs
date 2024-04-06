using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 猪 : 敌人
{
    protected override void Awake()
    {
        base.Awake();
        猪的巡逻状态=new 猪的巡逻();
        猪的追击状态 = new 猪的追击();
    }
}
