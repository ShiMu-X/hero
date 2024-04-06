using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class 蜜蜂的追击 : 抽象基类
{
    public override void 开启(敌人 敌人)
    {
        当前敌人 = 敌人;
    }

    public override void 状态监测()
    {
    }

    public override void 行为逻辑()
    {
        if (!当前敌人.玩家检测())
        {
            当前敌人.待机时间计时器 -= Time.deltaTime;
            if (当前敌人.待机时间计时器 < 0)
            {
                当前敌人.状态切换(敌人运行状态.蜜蜂的巡逻);
                当前敌人.待机时间计时器 = 当前敌人.待机时间;
                当前敌人.导航.SetDestination(当前敌人.蜜蜂位置);
                if (当前敌人.蜜蜂位置.x-当前敌人.transform.position.x>0)
                {
                    当前敌人.transform.localScale = new Vector3(-1, 1, 1);
                }
                else if(当前敌人.蜜蜂位置.x - 当前敌人.transform.position.x < 0)
                {
                    当前敌人.transform.localScale = new Vector3(1, 1, 1);

                }
                当前敌人.检测到目标=false;
                return;
            }
        }
        当前敌人.导航.SetDestination(new Vector3(当前敌人.玩家位置.transform.position.x, 当前敌人.玩家位置.transform.position.y+当前敌人.cc.size.y,0));
        if (Mathf.Abs(当前敌人.玩家位置.transform.position.x- 当前敌人.transform.position.x)<1.5&& Mathf.Abs(当前敌人.玩家位置.transform.position.y - 当前敌人.transform.position.y) < 3)
        {
            当前敌人.动画控制.SetBool("攻击",true);
        }
        if (当前敌人.玩家位置.transform.position.x- 当前敌人.transform.position.x>0)
        {
            当前敌人.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(当前敌人.玩家位置.transform.position.x - 当前敌人.transform.position.x < 0)
        {
            当前敌人.transform.localScale = new Vector3(1, 1, 1);

        }
    }
    public override void 关闭()
    {
    }

}
