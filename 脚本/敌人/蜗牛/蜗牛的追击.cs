using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 蜗牛的追击 : 抽象基类
{

    public override void 开启(敌人 敌人)
    {
        当前敌人 = 敌人;
        当前敌人.当前速度 = 当前敌人.奔跑速度;
        当前敌人.待机时间 = 3f;
        当前敌人.待机时间计时器 = 当前敌人.待机时间;
        当前敌人.动画控制.SetBool("缩壳", true);
        当前敌人.动画控制.SetBool("走", false);

    }

    public override void 状态监测()
    {
        if (!当前敌人.玩家检测())
        {
            当前敌人.待机 = true;
            if (当前敌人.玩家检测())
            {
                当前敌人.待机 = false;
                当前敌人.待机时间计时器 = 当前敌人.待机时间;
            }
            if (当前敌人.待机时间计时器 <= 0)
            {
                当前敌人.待机 = false;
                当前敌人.动画控制.SetBool("出壳", true);
                当前敌人.状态切换(敌人运行状态.蜗牛的巡逻);
            }

        }
    }

    public override void 行为逻辑()
    {
    }
    public override void 关闭()
    {
        当前敌人.动画控制.SetBool("缩壳", false);
    }

}
