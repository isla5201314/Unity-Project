using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GMcommand
{
    [MenuItem("CMcmd/��ȡ���")]
    
    public static void ReadTable()
    {
        PackageTable packageTable = Resources.Load<PackageTable>("TableData/PackageTable");
        if (packageTable == null)
            Debug.Log("û�ҵ���");
        if (packageTable != null)
            foreach (PackageTableItem packageItem in packageTable.DataList)
        {
            Debug.Log(string.Format("��id�� : {0}, ��name�� : {1}", packageItem.id, packageItem.name));
        }
    }
}
