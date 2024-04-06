using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class 猪的追击 : 抽象基类
{

    public override void 开启(敌人 敌人)
    {
        当前敌人 = 敌人;
        当前敌人.当前速度=当前敌人.奔跑速度;
        当前敌人.动画控制.SetBool("跑", true);

    }

    public override void 状态监测()
    {
        //if (当前敌人.丢失目标时间计时器<=0)
        //{
        //    当前敌人.状态切换(敌人运行状态.猪的巡逻);
        //}
        if (!当前敌人.物理检测.处于地面 || (当前敌人.物理检测.处于右墙 && 当前敌人.面朝方向.x > 0) || (当前敌人.物理检测.处于左墙 && 当前敌人.面朝方向.x < 0))
        {
            当前敌人.transform.localScale = new Vector3(当前敌人.面朝方向.x, 1, 1);
        }
    }

    public override void 行为逻辑()
    {
        计时器();
    }
    public override void 关闭()
    {
        当前敌人.动画控制.SetBool("跑", false);
    }
    public void 计时器()
    {
        if (!当前敌人.玩家检测() && 当前敌人.丢失目标时间计时器 > 0)
        {
            当前敌人.丢失目标时间计时器 -= Time.deltaTime;
        }
        else if (当前敌人.丢失目标时间计时器 <= 0)
        {
            当前敌人.状态切换(敌人运行状态.猪的巡逻);
            当前敌人.丢失目标时间计时器 = 当前敌人.丢失目标时间;
        }

    }

}
