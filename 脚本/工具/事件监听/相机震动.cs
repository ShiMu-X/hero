using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "����/�����")]
public class ����� : ScriptableObject
{
    public UnityAction �¼�����;
    public void ��������()
    {
        �¼�����?.Invoke();
    }

}
