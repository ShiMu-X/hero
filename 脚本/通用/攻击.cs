using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 攻击 : MonoBehaviour
{
    // Start is called before the first frame update
    public float 攻击伤害;
    public float 攻击范围;
    public float 攻击频率;
    public bool 攻击命中=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<属性>()?.受到伤害(this);
    }
}
