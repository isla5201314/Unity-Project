using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Bag", menuName = "背包系统/新背包")]
public class Inventory : ScriptableObject
{
    public List<Items> itemsList = new List<Items>();


}
