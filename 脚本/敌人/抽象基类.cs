using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class 抽象基类
{

    protected 敌人 当前敌人;
    public abstract void 开启(敌人 敌人);
    public abstract void 状态监测();

    public abstract void 行为逻辑();
    public abstract void 关闭();

}
