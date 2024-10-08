using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "����ϵͳ/����Ʒ")]

public class Items : ScriptableObject   
{
    public int id; //��ƷID����

    public int Type;//��Ʒ����

    public string name;//��Ʒ����

    public Sprite sprite;

    [TextArea] // ���ı�����ı���
    public string description; //��Ʒ����
    [TextArea] // ���ı�����ı���
    public string skillDescription; //��Ʒ��ϸ����

    public bool IsUse;

    public int Num = 0; //����
}
