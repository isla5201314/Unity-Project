using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "背包系统/新物品")]

public class Items : ScriptableObject   
{
    public int id; //物品ID索引

    public int Type;//物品类型

    public string name;//物品名称

    public Sprite sprite;

    [TextArea] // 让文本变成文本框
    public string description; //物品描述
    [TextArea] // 让文本变成文本框
    public string skillDescription; //物品详细描述

    public bool IsUse;

    public int Num = 0; //数量
}
