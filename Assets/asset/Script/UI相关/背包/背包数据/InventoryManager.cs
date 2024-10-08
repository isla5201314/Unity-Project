using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    [Header("������̨����")]
    public Inventory myBag;
    public Slot slotPrefab;
    public Inventory ObjectDataBag;


    [Header("����UI")]
    public GameObject Bag;
    public GameObject[] GridObject;

    [Header("��Ʒ����")]
    public TMP_Text Name;
    public Image Image;
    public TMP_Text Description;
    public TMP_Text SkillDescription;
    public int Num;

    [Header("��ѡ�е���Ʒչʾ")]
    public GameObject ObjectInspector;
    public TMP_Text ObjectName;
    public Image ObjectImage;
    public TMP_Text Introduce;
    public TMP_Text SkillIntroduce;
    public GameObject Use;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void Update()
    {
        WaitToLoad();
    }

    public void UpdateTheObjectInformation() //��������ѡ�е�������Ϣ
    {
        for (int i = 0; i < instance.GridObject.Length; i++)
        {
            ObjectSelected[] os = instance.GridObject[i].GetComponentsInChildren<ObjectSelected>(); // ��ȡ��Ӧ������Ϣ

            // ����os���鼴���и��ӣ�����IsSelectedΪtrue��ObjectSelected
            foreach (ObjectSelected objSel in os)
            {
                if (objSel != null && objSel.IsSelected == true)
                {
                    Slot slot = instance.GridObject[i].GetComponent<Slot>();

                    if (slot.slotItems != null)
                    {
                        ObjectName.text = slot.slotItems.name;
                        ObjectImage.sprite = slot.slotItems.sprite;
                        Introduce.text = slot.slotItems.description;
                        SkillDescription.text = slot.slotItems.skillDescription;
                        if (slot.slotItems.IsUse == true) 
                        {
                            UseObject UO = Use.GetComponent<UseObject>();
                            UO.slot = slot;
                            Use.SetActive(true); 

                        }
                        else Use.SetActive(false);
                    }
                    else // ���ѡ�е���ƷΪ��
                    {
                        ObjectName.text = "";
                        ObjectImage.sprite = null;
                        Introduce.text = "";
                        SkillDescription.text = "";
                        Use.SetActive(false);
                    }

                    // һ���ҵ���������ѡ�еĶ��󣬿�������ѭ�������ظ�����
                    break;
                }
            }
        }
    }

    public  void WaitToLoad()//�ȴ�������ʱ����
    {
        if (instance.Bag.activeSelf)
        {
                CreateNewItem();
            UpdateTheObjectInformation();
        }


    }

    public  void CreateNewItem()//��ʼ����UI����
    {
        List<Items> itemsToRemove = new List<Items>();//��ɾ�����б����
        foreach (Items items in instance.myBag.itemsList)
        {
            if (items.Num == 0)
            {
                itemsToRemove.Add(items);
            }
        }
        // �Ӽ������Ƴ����б��Ϊ��Ҫ�Ƴ�����,����ֱ��ɾ��
        foreach (Items item in itemsToRemove)
        {
            instance.myBag.itemsList.Remove(item);
        }



        int j = 0;
            while (j < instance.GridObject.Length)//ÿ�α���ǰ�����һ�α���
            {
                Slot slots = instance.GridObject[j].GetComponent<Slot>();
                slots.slotItems = null;
                slots.Name.text = "";
                slots.slotImage.sprite = null;
                slots.slotNum.text = "";
                j++;
            }
        
        



        foreach (Items Object in instance.ObjectDataBag.itemsList)//ʵʱ������Ʒ����������Ʒ�Զ����
        {
            if(Object.Num > 0 && !myBag.itemsList.Contains(Object))
            {
                instance.myBag.itemsList.Add(Object);
            }
        }




        int i = 0;
     foreach (Items items in instance.myBag.itemsList)// �ñ��������Ʒ������������Ŀո�,��ʵʱ����
        {
           
            while ( i < instance.GridObject.Length ) //�������п�λ�����ÿ�λ��Ϊ��Ӧ��Ʒ��Ϣ
            {
               

                Slot slot = instance.GridObject[i].GetComponent<Slot>();//��ȡ��Ӧ������Ϣ
                    slot.slotItems = items;
                    slot.Name.text = items.name;
                    slot.slotImage.sprite = items.sprite;
                    slot.slotNum.text = items.Num.ToString();
                i++;                
                break;
            }
        } 
    }
}
