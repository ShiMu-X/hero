using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 动画控制 : MonoBehaviour
{
    Animator 动画;
    Rigidbody2D rb;
    物理检测 物理检测;
    CapsuleCollider2D cc;
    玩家控制器 玩家控制器;



    private void Awake()
    {
        动画=GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        物理检测=GetComponent <物理检测>();
        cc = GetComponent<CapsuleCollider2D>();
        玩家控制器= GetComponent<玩家控制器>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        跑();
    }
    void 跑()
    {
        动画.SetFloat("跑", Mathf.Abs(rb.velocity.x));
        动画.SetFloat("跳", rb.velocity.y);
        动画.SetBool("处于地面", 物理检测.处于地面);
        动画.SetBool("蹲", 玩家控制器.角色控制.y < 0);
        动画.SetBool("处于死亡", 玩家控制器.处于死亡);
        动画.SetBool("攻击",玩家控制器.处于攻击);
        动画.SetBool("处于墙", 物理检测.处于墙);
        动画.SetBool("滑铲", 玩家控制器.处于滑铲);
    }
    public void 受伤()
    {
        动画.SetTrigger("受伤");
    }
    public void 攻击()
    {
        动画.SetTrigger("攻击状态");
    }
}
