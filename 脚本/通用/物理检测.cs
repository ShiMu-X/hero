using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 物理检测 : MonoBehaviour
{
    CapsuleCollider2D cc;
    玩家控制器 玩家控制器;
    public GameObject 当前物体;
    public bool 自动模式;

    public Vector2 地面检测偏移;
    public Vector2 左墙检测偏移;
    public Vector2 右墙检测偏移;

    public bool 处于地面, 处于跳跃,处于左墙,处于右墙,处于墙;
    public float 检测范围;
    public LayerMask 地面检测;
    private void Awake()
    {
        cc = GetComponent<CapsuleCollider2D>();
        玩家控制器=GetComponent<玩家控制器>();
        if (自动模式)
        {
            右墙检测偏移 = new Vector2(cc.bounds.size.x / 2 + cc.offset.x, cc.size.y / 2);
            左墙检测偏移 = new Vector2(-右墙检测偏移.x, 右墙检测偏移.y);
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        检测();
    }
    void 检测()
    {
        处于地面=Physics2D.OverlapCircle((Vector2)transform.position +地面检测偏移* transform.localScale.x, 检测范围, 地面检测);
        处于左墙 = Physics2D.OverlapCircle((Vector2)transform.position + 左墙检测偏移, 检测范围, 地面检测);
        处于右墙 = Physics2D.OverlapCircle((Vector2)transform.position + 右墙检测偏移, 检测范围, 地面检测);
        if (当前物体.tag == "玩家")
        {
            处于墙 = (处于左墙 &&玩家控制器.角色控制.x < 0 || 处于右墙 &&玩家控制器.角色控制.x > 0) && !处于地面 &&玩家控制器.rb.velocity.y < 0;
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + 地面检测偏移* transform.localScale.x, 检测范围);
        Gizmos.DrawWireSphere((Vector2)transform.position + 左墙检测偏移, 检测范围);
        Gizmos.DrawWireSphere((Vector2)transform.position + 右墙检测偏移, 检测范围);

    }
}
