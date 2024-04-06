using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class 玩家控制器 : MonoBehaviour
{
    //控制设置
    public 角色控制器 控制设置;
    public Vector2 角色控制;
    public GameObject 当前物体;

    [Header("基本参数")]
    //移动数值
    public float 移动速度;
    //跳跃
    public float 跳跃的力;
    public float 受伤的力;
    //滑墙
    //public bool 处于墙;
    public float 滑墙速度;
    public float 滑铲速度;
    public Vector3 滑铲距离;
    public bool 蹬墙跳;
    //滑铲
    public bool 处于滑铲;
    public float 滑铲冷却;
    protected bool 处于滑铲冷却;
    //受伤
    public bool 处于受伤;
    public bool 处于死亡;
    //攻击
    public bool 处于攻击;
    //物理材质
    public PhysicsMaterial2D 地面;
    public PhysicsMaterial2D 贴墙;

    //获取组件
    public UnityEvent<Transform> 受伤;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    CapsuleCollider2D cc;
    物理检测 物理检测;
    动画控制 动画控制;
    //蹲
    public bool 处于下蹲;


    private void Awake()
    {
        控制设置 = new 角色控制器();
        控制设置.控制器.jump.started += 跳跃;
        控制设置.控制器.静步.started += 静步;
        控制设置.控制器.静步.performed += 退出静步;
        控制设置.控制器.攻击.started += 攻击;
        控制设置.控制器.滑铲.started += 滑铲;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        物理检测=GetComponent<物理检测>();
        cc = GetComponent<CapsuleCollider2D>();
        动画控制 = GetComponent<动画控制>();
    }

    private void OnEnable()
    {
        控制设置.Enable();
    }
    private void OnDisable()
    {
        控制设置.Disable();
    }
    void Start()
    {
        
    }

    void Update()
    {
        材质切换();
        if (!处于滑铲)
        {
            角色控制 = 控制设置.控制器.Move.ReadValue<Vector2>();
        }
    }
    private void FixedUpdate()
    {
        if (!处于受伤&&!处于攻击)
        {
            移动();
        }
        滑墙();
    }
    void 移动()
    {
        //左右移动
        //蹲
        if(角色控制.y < 0&& 物理检测.处于地面)
        {
            cc.offset = new Vector2(cc.offset.x, (float)0.85);
            cc.size = new Vector2(cc.size.x, (float)1.7);
            处于下蹲 = true;
        }
        else
        {
            cc.offset = new Vector2(cc.offset.x, (float)0.95);
            cc.size = new Vector2(cc.size.x, (float)1.9);
            处于下蹲 = false;
        }
        //左右移动
        if (处于下蹲)
        {
            rb.velocity =new Vector2(0f,rb.velocity.y);
        }
        else if (!蹬墙跳&&!处于下蹲)
        {
            int 转向=(int)transform.localScale.x;
            if (角色控制.x>0)
            {
                转向 = 1;
            }
            else if(角色控制.x<0)
            {
                转向 = -1;
            }
            rb.velocity = new Vector2(角色控制.x * 移动速度, rb.velocity.y);
            transform.localScale=new Vector3(转向, 1,1);
        }

        //转向
        //if (角色控制.x<0)
        //{
        //    sr.flipX = true;
        //}
        //else if(角色控制.x >0)
        //{
        //    sr.flipX = false;

        //}
    }
    private void 跳跃(InputAction.CallbackContext context)
    {
        处于滑铲=false;
        if(物理检测.处于地面&&!处于攻击&&!处于受伤)
        {
            rb.AddForce(transform.up * 跳跃的力, ForceMode2D.Impulse);
        }
        if(!处于攻击 && !处于受伤&& 物理检测.处于墙)
        {
            rb.AddForce(new Vector2(-角色控制.x,2f).normalized* 跳跃的力, ForceMode2D.Impulse);
            int 转向 = (int)transform.localScale.x;
            if (角色控制.x > 0)
            {
                转向 = -1;
            }
            else if (角色控制.x < 0)
            {
                转向 = 1;
            }
            transform.localScale = new Vector3(转向, 1, 1);
            蹬墙跳 = true;
        }
    }
    private void 静步(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
        移动速度 /=3;
    }
    private void 退出静步(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
        移动速度 *= 3;
    }
    public void 受伤状态(Transform 攻击来源)
    {
        处于受伤 = true;
        rb.velocity=Vector2.zero;
        if (攻击来源.tag != "荆棘")
        {
            Vector2 受伤方向 = new Vector2(transform.position.x - 攻击来源.transform.position.x, 0).normalized;
            rb.AddForce(受伤方向 * 受伤的力, ForceMode2D.Impulse);
        }
        if (攻击来源.tag == "荆棘")
        {
            Vector2 受伤方向 = new Vector2(transform.localScale.x,0).normalized;
            rb.AddForce(-受伤方向 * 受伤的力, ForceMode2D.Impulse);
        }
    }
    private void 攻击(InputAction.CallbackContext context)
    {
        if (!处于受伤&&!物理检测.处于墙)
        {
            动画控制.攻击();
            处于攻击 = true;
        }
    }
    public void 滑墙()
    {
        //if (当前物体.tag == "玩家")
        //{
        //    处于墙 = (物理检测.处于左墙&&角色控制.x<0 ||物理检测.处于右墙&&角色控制.x > 0) && !物理检测.处于地面&&rb.velocity.y<0;
        //}
        if (物理检测.处于墙)
        {
            rb.velocity=new Vector2(rb.velocity.x,rb.velocity.y/滑墙速度);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
        if ((蹬墙跳 && rb.velocity.y < 0) || 物理检测.处于地面)
        {
            蹬墙跳 = false;
        }
    }
    private void 滑铲(InputAction.CallbackContext context)
    {
        if (!处于滑铲&&!处于滑铲冷却&&!处于受伤&&物理检测.处于地面&&!物理检测.处于右墙&&!物理检测.处于左墙)
        {
            处于滑铲 = true;
            当前物体.layer =LayerMask.NameToLayer("敌人");
            StartCoroutine(滑铲(滑铲距离));
        }
    }

    private IEnumerator 滑铲(Vector3 滑铲距离)
    {
        处于滑铲冷却 = true;
        for (滑铲距离.x= transform.position.x+ 滑铲距离.x* (角色控制.x= (角色控制.x!=0) ? 角色控制.x : transform.localScale.x); Math.Abs(滑铲距离.x - transform.position.x) > 0.1f&&处于滑铲;)
        {
            yield return null;
            if (!物理检测.处于地面)
            {
                break;
            }
            if (物理检测.处于左墙 || 物理检测.处于右墙|| rb.velocity.y>0.1)
            {
                处于滑铲 = false;
                break;
            }
            rb.MovePosition(new Vector2(transform.position.x + (角色控制.x = (角色控制.x != 0) ? 角色控制.x : transform.localScale.x) * 滑铲速度, transform.position.y));
        }
        处于滑铲 = false;
        当前物体.layer = LayerMask.NameToLayer("玩家");
        yield return new WaitForSeconds(滑铲冷却);
        处于滑铲冷却 = false;

    }
    public void 玩家死亡()
    {
        处于死亡=true;
        控制设置.控制器.Disable();
    }
    private void 材质切换()
    {
        cc.sharedMaterial = (物理检测.处于左墙 || 物理检测.处于右墙) ? 贴墙 : 地面;
        if (!物理检测.处于墙&&!(物理检测.处于左墙 || 物理检测.处于右墙))
        {
            cc.sharedMaterial = 物理检测.处于地面 ? 地面 : 贴墙;
        }
    }
}
