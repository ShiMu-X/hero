using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class 蜜蜂的巡逻 : 抽象基类
{


    public override void 开启(敌人 敌人)
    {
        当前敌人 = 敌人;
        当前敌人.导航.updateRotation = false;
        当前敌人.导航.updateUpAxis = false;
        当前敌人.待机时间计时器=当前敌人.待机时间;
    }

    public override void 状态监测()
    {
        if (当前敌人.检测到目标)
        {
            当前敌人.状态切换(敌人运行状态.蜜蜂的追击);
        }
    }

    public override void 行为逻辑()
    {
        
    }
    public override void 关闭()
    {
    }
}
