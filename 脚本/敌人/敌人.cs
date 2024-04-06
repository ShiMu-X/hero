using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class 敌人 : MonoBehaviour
{
    [Header("获取组件")]
    public Animator 动画控制;
    public 物理检测 物理检测;
    public NavMeshAgent 导航;
    public Transform 玩家位置;
    public Transform 攻击来源;
    public Rigidbody2D rb;
    public CapsuleCollider2D cc;
    public GameObject 当前物体;
    public GameObject 玩家物体;
    [Header("基本数值")]
    public float 移动速度;
    public float 奔跑速度;
    public float 当前速度;
    public float 受伤的力;
    public Vector3 面朝方向;

    [Header("计时器")]
    public float 待机时间;
    public float 待机时间计时器;
    public float 丢失目标时间;
    public float 丢失目标时间计时器;


    public bool 待机, 处于受伤, 处于死亡,处于移动;



    private 抽象基类 当前状态;
    protected 抽象基类 猪的巡逻状态;
    protected 抽象基类 猪的追击状态;
    protected 抽象基类 蜜蜂的追击状态;
    protected 抽象基类 蜜蜂的巡逻状态;
    protected 抽象基类 蜗牛的巡逻状态;
    protected 抽象基类 蜗牛的追击状态;


    [Header("检测")]
    public Vector2 检测位置偏移;
    public Vector2 检测尺寸;
    public float 检测距离;
    public LayerMask 检测图层;

    [Header("蜜蜂")]
    public Vector3 蜜蜂位置;

    [Header("射线检测")]
    public Vector3 射线位置偏移;
    public Vector3 射线检测玩家位置;
    public float 视野距离;
    public int 射线密度;
    public float 射线最大距离;
    public LayerMask 射线检测图层;
    public bool 检测到目标;

    [Range(0, 360)]
    public float 视野角度;

    protected virtual void Awake()
    {
        玩家物体 =GameObject.FindGameObjectWithTag("玩家");
        玩家位置= 玩家物体.transform;
        攻击来源= 玩家物体.transform;
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        动画控制 = GetComponent<Animator>();
        物理检测 = GetComponent<物理检测>();
        
    }
    private void OnEnable()
    {
        if (当前物体.tag == "猪")
        {
            当前状态 = 猪的巡逻状态;
        }
        if (当前物体.tag == "蜜蜂")
        {
            当前状态 = 蜜蜂的巡逻状态;
        }
        if (当前物体.tag == "蜗牛")
        {
            当前状态 = 蜗牛的巡逻状态;
        }
        //当前状态 = 猪的巡逻状态;
        当前状态.开启(this);
    }
    private void Start()
    {
        if (当前物体.tag == "蜜蜂")
        {
            Physics2D.queriesStartInColliders = false;
            蜜蜂位置 = new Vector3(transform.position.x, transform.position.y, 0);
            检测位置偏移.x = 检测距离 / 2;
        }

    }
    //Update is called once per frame
    private void Update()
    {
        面朝方向 = new Vector3(-transform.localScale.x, 0, 0);
        当前状态.状态监测();
        计时器();
        if (当前物体.tag == "蜜蜂")
        {
            射线检测();
        }
    }
    private void FixedUpdate()
    {
        当前状态.行为逻辑();
        if (!处于受伤 && !处于死亡 && 物理检测.处于地面 && (当前物体.tag == "猪"|| 当前状态 ==蜗牛的巡逻状态))
        {
            猪的移动();
        }
        else if (当前物体.tag == "蜜蜂")
        {
            蜜蜂的移动();
        }
        当前状态.行为逻辑();
    }
    private void OnDisable()
    {
        当前状态.关闭();
    }
    public virtual void 猪的移动()
    {
        rb.velocity = new Vector2(当前速度 * 面朝方向.x, rb.velocity.y);
    }
    public virtual void 蜜蜂的移动()
    {
        if (当前状态==蜜蜂的追击状态)
        {
            待机时间计时器-=Time.deltaTime;
            if (待机时间计时器<0)
            {
                待机时间计时器 = 待机时间;
                检测到目标=false;
                当前状态 = 蜜蜂的巡逻状态;
            }
            return;
        }
        Vector3 巡逻 = 蜜蜂位置;
        if (处于移动)
        {
            return;
        }
        if(!处于移动)
        {
            float x = UnityEngine.Random.Range((蜜蜂位置.x - 检测位置偏移.x/2), 蜜蜂位置.x + 检测位置偏移.x/2);
            float y = UnityEngine.Random.Range(蜜蜂位置.y - 检测尺寸.y / 3, 蜜蜂位置.y + 检测尺寸.y / 3);
            if (x- 巡逻.x>0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(x - 巡逻.x <0)
            {
                transform.localScale = new Vector3(1, 1, 1);

            }
            巡逻 = new Vector3(x, y, 0);
        }
        StartCoroutine(导航寻路(巡逻));
    }
    IEnumerator 导航寻路(Vector3 巡逻)
    {
        处于移动 = true;
        导航.SetDestination(巡逻);
        yield return new WaitForSeconds(5f);
        处于移动 = false;
    }

    public void 计时器()
    {
        if (待机)
        {
            待机时间计时器 -= Time.deltaTime;
            if (待机时间计时器 <= 0)
            {
                待机 = false;
                if (当前状态==蜗牛的追击状态)
                {
                    return;
                }
                待机时间计时器 = 待机时间;
                transform.localScale = new Vector3(面朝方向.x, 1, 1);
            }
        }
    }

    public void 受到攻击(Transform 攻击来源)
    {
        攻击来源 = this.攻击来源;
        rb.velocity = new Vector2(0f, rb.velocity.y);
        Vector2 受伤方向 = new Vector2(攻击来源.transform.position.x - transform.position.x, 0).normalized;
        if (受伤方向.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (受伤方向.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        处于受伤 = true;
        待机 = false;
        待机时间计时器 = 待机时间;
        动画控制.SetTrigger("受伤");
        StartCoroutine(受到攻击(受伤方向));

    }
    IEnumerator 受到攻击(Vector2 受伤方向)
    {
        rb.AddForce(-受伤方向 * 受伤的力, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.8f);
        处于受伤 = false;
        if (当前物体.tag=="蜜蜂")
        {
            rb.velocity=Vector3.zero;
        }
    }
    public void 死亡()
    {
        处于死亡 = true;
        动画控制.SetBool("死亡", true);
    }
    public void 死亡关闭组件()
    {
        cc.enabled = false;
        rb.velocity = Vector2.zero;
    }

    public void 死亡销毁物体()
    {
        Destroy(this.gameObject);
    }
    public void 状态切换(敌人运行状态 敌人运行状态)
    {
        var 新的状态 = 敌人运行状态 switch
        {
            敌人运行状态.猪的巡逻 => 猪的巡逻状态,
            敌人运行状态.猪的追击 => 猪的追击状态,
            敌人运行状态.蜜蜂的巡逻 => 蜜蜂的巡逻状态,
            敌人运行状态.蜜蜂的追击 => 蜜蜂的追击状态,
            敌人运行状态.蜗牛的巡逻 => 蜗牛的巡逻状态,
            敌人运行状态.蜗牛的追击 => 蜗牛的追击状态,

            _ => null
        };
        当前状态.关闭();
        当前状态 = 新的状态;
        当前状态.开启(this);
    }
    public bool 玩家检测()
    {
        if (当前物体.tag == "猪"|| 当前物体.tag == "蜗牛")
        {
            return Physics2D.BoxCast(transform.position + (Vector3)检测位置偏移, 检测尺寸, 0, 面朝方向, 检测距离, 检测图层);
        }
        if (当前物体.tag == "蜜蜂")
        {
            return Physics2D.BoxCast(蜜蜂位置+ (Vector3)检测位置偏移, 检测尺寸, 0, new Vector2(-1,0), 检测距离, 检测图层);

        }
        else
        {
            return false;
        }

    }
    public void 射线检测()
    {
        Vector2 初始方向 = Quaternion.Euler(0, 0, -(视野角度 / 2f)) * 面朝方向;
        for (int i = 0; i <= 射线密度; i++)
        {
            Vector2 全部方向 = Quaternion.Euler(0, 0, (视野角度 / 射线密度) * i) * 初始方向;
            RaycastHit2D 射线物理 = Physics2D.Raycast(transform.position, 全部方向, 射线最大距离, 射线检测图层);

            if (射线物理.collider != null)
            {
                if (射线物理.collider.tag == "玩家")
                {
                    射线检测玩家位置 = 射线物理.point;
                    Debug.DrawLine(transform.position + 射线位置偏移, 射线物理.point, Color.blue);
                    // 添加自己想要的逻辑
                    检测到目标=true;
                }
            }
            else
            {
                Debug.DrawLine(transform.position + 射线位置偏移, 全部方向 * 射线最大距离, Color.red);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (当前物体.tag == "猪"|| 当前物体.tag == "蜗牛")
        {
            Gizmos.DrawWireSphere(transform.position + (Vector3)检测位置偏移 + new Vector3(面朝方向.x * 检测距离, 0f), 检测尺寸.y);
        }
        if (当前物体.tag == "蜜蜂")
        {
            Gizmos.DrawWireCube(蜜蜂位置 + (Vector3)检测位置偏移, 检测尺寸);
        }
    }

}


