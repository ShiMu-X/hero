using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 状态栏 : MonoBehaviour
{
    public Image 血条;
    public Image 血条渐变;
    public Image 蓝条;
    public float 血条渐变速度;

    private void Update()
    {
        if (血条渐变.fillAmount> 血条.fillAmount)
        {
            血条渐变.fillAmount-=Time.deltaTime*血条渐变速度;
        }
    }
    public void 血量更新(float 数值)
    {
        血条.fillAmount = 数值;
    }
}
