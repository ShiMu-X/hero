using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 蜗牛 : 敌人
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        蜗牛的巡逻状态 = new 蜗牛的巡逻();
        蜗牛的追击状态 = new 蜗牛的追击();
    }
    public void 修改缩壳的图层()
    {
        当前物体.layer = LayerMask.NameToLayer("地板");
    }
    public void 恢复图层()
    {
        当前物体.layer = LayerMask.NameToLayer("敌人");
    }

}
