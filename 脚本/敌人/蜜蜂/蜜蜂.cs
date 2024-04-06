using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 蜜蜂 : 敌人
{
    protected override void Awake()
    {
        base.Awake();
        蜜蜂的巡逻状态 = new 蜜蜂的巡逻();
        蜜蜂的追击状态 = new 蜜蜂的追击();
    }
}
