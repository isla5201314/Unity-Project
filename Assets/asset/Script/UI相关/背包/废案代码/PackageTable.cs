using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "����ϵͳ/PackageTable",fileName = "PackageTable")]

public class PackageTable : ScriptableObject
{
    public List<PackageTableItem> DataList = new();
}

[System.Serializable]
public class PackageTableItem
{
    public int id; //��ƷID����

    public int Type;//��Ʒ����

    public string name;//��Ʒ����

    public string description; //��Ʒ����

    public string skillDescription; //��Ʒ��ϸ����

    public string imagePath; //ͼƬ·��

    public int Num = 0; //����
}