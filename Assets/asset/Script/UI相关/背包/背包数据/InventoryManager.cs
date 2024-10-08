using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    [Header("背包后台数据")]
    public Inventory myBag;
    public Slot slotPrefab;
    public Inventory ObjectDataBag;


    [Header("背包UI")]
    public GameObject Bag;
    public GameObject[] GridObject;

    [Header("物品属性")]
    public TMP_Text Name;
    public Image Image;
    public TMP_Text Description;
    public TMP_Text SkillDescription;
    public int Num;

    [Header("被选中的物品展示")]
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

    public void UpdateTheObjectInformation() //遍历更新选中的物体信息
    {
        for (int i = 0; i < instance.GridObject.Length; i++)
        {
            ObjectSelected[] os = instance.GridObject[i].GetComponentsInChildren<ObjectSelected>(); // 获取对应格子信息

            // 遍历os数组即所有格子，查找IsSelected为true的ObjectSelected
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
                    else // 如果选中的物品为空
                    {
                        ObjectName.text = "";
                        ObjectImage.sprite = null;
                        Introduce.text = "";
                        SkillDescription.text = "";
                        Use.SetActive(false);
                    }

                    // 一旦找到并处理了选中的对象，可以跳出循环避免重复处理
                    break;
                }
            }
        }
    }

    public  void WaitToLoad()//等待背包打开时更新
    {
        if (instance.Bag.activeSelf)
        {
                CreateNewItem();
            UpdateTheObjectInformation();
        }


    }

    public  void CreateNewItem()//开始更新UI背包
    {
        List<Items> itemsToRemove = new List<Items>();//待删除的列表物件
        foreach (Items items in instance.myBag.itemsList)
        {
            if (items.Num == 0)
            {
                itemsToRemove.Add(items);
            }
        }
        // 从集合中移除所有标记为需要移除的项,不能直接删除
        foreach (Items item in itemsToRemove)
        {
            instance.myBag.itemsList.Remove(item);
        }



        int j = 0;
            while (j < instance.GridObject.Length)//每次遍历前先清空一次背包
            {
                Slot slots = instance.GridObject[j].GetComponent<Slot>();
                slots.slotItems = null;
                slots.Name.text = "";
                slots.slotImage.sprite = null;
                slots.slotNum.text = "";
                j++;
            }
        
        



        foreach (Items Object in instance.ObjectDataBag.itemsList)//实时更新物品，如果获得物品自动入库
        {
            if(Object.Num > 0 && !myBag.itemsList.Contains(Object))
            {
                instance.myBag.itemsList.Add(Object);
            }
        }




        int i = 0;
     foreach (Items items in instance.myBag.itemsList)// 用背包里的物品遍历背包界面的空格,并实时更新
        {
           
            while ( i < instance.GridObject.Length ) //遍历所有空位并将该空位变为相应物品信息
            {
               

                Slot slot = instance.GridObject[i].GetComponent<Slot>();//获取对应格子信息
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
