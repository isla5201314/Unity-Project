using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "背包系统/PackageTable",fileName = "PackageTable")]

public class PackageTable : ScriptableObject
{
    public List<PackageTableItem> DataList = new();
}

[System.Serializable]
public class PackageTableItem
{
    public int id; //物品ID索引

    public int Type;//物品类型

    public string name;//物品名称

    public string description; //物品描述

    public string skillDescription; //物品详细描述

    public string imagePath; //图片路径

    public int Num = 0; //数量
}