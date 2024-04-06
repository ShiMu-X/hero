using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class 属性 : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("基本属性")]
    public float 最大hp;
    public float 当前hp;


    [Header("受伤无敌")]
    public float 无敌时间;
    float 无敌时间计时器;
    public bool 处于无敌;

    public UnityEvent<Transform> 受伤;
    public UnityEvent 处于死亡;
    public UnityEvent<属性> 获取属性;

    void Start()
    {
        当前hp = 最大hp;
        获取属性?.Invoke(this);

    }

    // Update is called once per frame
    void Update()
    {
        无敌();
    }
    private void OnTriggerStay2D(Collider2D 碰撞物体)
    {
        if(碰撞物体.CompareTag("水"))
        {
            当前hp = 0;
            获取属性?.Invoke(this);
            处于死亡?.Invoke();
        }
    }
    public void 无敌()
    {
        if (处于无敌)
        {
            无敌时间计时器 -= Time.deltaTime;
            if (无敌时间计时器 <= 0)
            {
                处于无敌 = false;
            }
        }

    }
    public void 受到伤害(攻击 伤害)
    {
        if (处于无敌)
        {
            return;
        }
        if (当前hp-伤害.攻击伤害>0)
        {
            当前hp -= 伤害.攻击伤害;
            触发无敌();
            受伤?.Invoke(伤害.transform);
        }
        else
        {
            当前hp = 0f;//死亡
            处于死亡?.Invoke();
        }
        获取属性?.Invoke(this);
    }

    void 触发无敌()
    {
        if (!处于无敌)
        {
            处于无敌=true;
            无敌时间计时器 = 无敌时间;
        }
    }
}
