using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 猪的巡逻 : 抽象基类
{

    public override void 开启(敌人 敌人)
    {
        当前敌人 = 敌人;
        当前敌人.当前速度 = 当前敌人.移动速度;

    }

    public override void 状态监测()
    {
        if (当前敌人.玩家检测())
        {
            当前敌人.状态切换(敌人运行状态.猪的追击);
        }
        if (!当前敌人.物理检测.处于地面||(当前敌人.物理检测.处于右墙 && 当前敌人.面朝方向.x > 0) || (当前敌人.物理检测.处于左墙 && 当前敌人.面朝方向.x < 0))
        {
            当前敌人.待机 = true;
            当前敌人.动画控制.SetBool("走", false);
        }
        else
        {
            当前敌人.动画控制.SetBool("走", true);
        }

    }

    public override void 行为逻辑()
    {
    }
    public override void 关闭()
    {
        当前敌人.动画控制.SetBool("走", false);

    }

}
