using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Misson Manager", menuName = "任务/任务管理器")]

public class Misson : ScriptableObject
{
    public List<Task> TaskList = new List<Task>();


}
