using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Task",menuName ="任务/主线任务")]

public class Task : ScriptableObject
{
    public float TaskID;//任务ID
    public string Name;//任务名称

    [TextArea]
    public string Description;//任务简述
    [TextArea]
    public string SkillDescription;//任务详细描述
    public bool IsFinish;//任务是否完成
}
