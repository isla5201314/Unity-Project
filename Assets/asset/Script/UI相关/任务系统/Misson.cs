using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Misson Manager", menuName = "����/���������")]

public class Misson : ScriptableObject
{
    public List<Task> TaskList = new List<Task>();


}
