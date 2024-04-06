using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 蜗牛的巡逻 : 抽象基类
{
    public override void 开启(敌人 敌人)
    {
        当前敌人 = 敌人;
        当前敌人.当前速度 = 当前敌人.移动速度;
        当前敌人.待机时间 = 0.1f;
        当前敌人.待机时间计时器 = 当前敌人.待机时间;
        当前敌人.动画控制.SetBool("缩壳", false);
        当前敌人.动画控制.SetBool("走", true);

    }

    public override void 状态监测()
    {
        if (当前敌人.玩家检测())
        {
            当前敌人.状态切换(敌人运行状态.蜗牛的追击);
        }
        if (!当前敌人.物理检测.处于地面 || (当前敌人.物理检测.处于右墙 && 当前敌人.面朝方向.x > 0) || (当前敌人.物理检测.处于左墙 && 当前敌人.面朝方向.x < 0))
        {
            当前敌人.待机 = true;
        }

    }

    public override void 行为逻辑()
    {
    }
    public override void 关闭()
    {
        当前敌人.动画控制.SetBool("出壳", false);

    }

}
