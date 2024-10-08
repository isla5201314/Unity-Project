using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GMcommand
{
    [MenuItem("CMcmd/读取表格")]
    
    public static void ReadTable()
    {
        PackageTable packageTable = Resources.Load<PackageTable>("TableData/PackageTable");
        if (packageTable == null)
            Debug.Log("没找到捏");
        if (packageTable != null)
            foreach (PackageTableItem packageItem in packageTable.DataList)
        {
            Debug.Log(string.Format("【id】 : {0}, 【name】 : {1}", packageItem.id, packageItem.name));
        }
    }
}
