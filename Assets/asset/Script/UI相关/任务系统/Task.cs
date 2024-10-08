using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Task",menuName ="����/��������")]

public class Task : ScriptableObject
{
    public float TaskID;//����ID
    public string Name;//��������

    [TextArea]
    public string Description;//�������
    [TextArea]
    public string SkillDescription;//������ϸ����
    public bool IsFinish;//�����Ƿ����
}
