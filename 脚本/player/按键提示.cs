using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 按键提示 : MonoBehaviour
{
    public Transform 玩家物体;
    public GameObject 场景互动;
    public Vector2 按键大小;

    private bool 能互动;
    private void Update()
    {
        场景互动.SetActive(能互动);
        场景互动.transform.localScale = 玩家物体.transform.localScale* 按键大小;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("场景互动"))
        {
            能互动=true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
            能互动 = false;
    }
}
