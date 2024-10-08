using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissonManager : MonoBehaviour
{
    [Header("任务系统管理")]
    static public MissonManager instance;
    public Misson misson;

    [Header("任务列表")]
    public GameObject MissonUI;
    public GameObject[] TakeGrid;

    [Header("被选中的任务显示的属性")]
    public TaskTake task;
    public TMP_Text Name;//任务名称
    public TMP_Text SkillDescription;//任务详细描述

    [Header("UI显示的任务信息")]
    public TMP_Text TaskName;
    public TMP_Text TaskDescription;

    public bool IsUpdated = false;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    public void WaitToLoad()
    {
        if (instance.MissonUI.activeSelf)
        {
            CreateNewTask();
            UpdateTheObjectInformation();
            if (IsUpdated)
            {
                if (misson.TaskList.Count > 0)
                {
                    IsUpdated = false;
                    // 由于任务界面已打开，现在可以安全地执行更新选中任务的操作
                    Task taskToSelect = misson.TaskList[misson.TaskList.Count - 1]; // 假设新任务是列表中的最后一个
                    UpdateTaskSelection(taskToSelect);
                }
            }
        }


    }

    void Update()
    {
        WaitToLoad();
    }

    public void CreateNewTask()//刷新任务列表显示的任务
    {
        int i = 0;
        foreach (Task task in instance.misson.TaskList)// 用任务里的列表遍历任务界面的空位,并实时更新
        {

            while (i < instance.TakeGrid.Length) //遍历所有空位并将该空位变为相应任务信息
            {
                TaskTake taskTake = instance.TakeGrid[i].GetComponent<TaskTake>();//获取对应任务信息
                taskTake.task = task;
                taskTake.Name.text = task.Name;
                if (task.IsFinish)
                    taskTake.Finish.sprite = taskTake.ISFinishSprite;
                else taskTake.Finish.sprite = taskTake.NotFinishSprite;


                i++;
                break;
            }
        }
    }

    public void UpdateTheObjectInformation() //遍历更新选中的任务信息
    {

        for (int i = 0; i < instance.TakeGrid.Length; i++)
        {
            TaskSelected[] os = instance.TakeGrid[i].GetComponentsInChildren<TaskSelected>(); // 获取对应格子信息

            // 遍历os数组即所有格子，查找IsSelected为true的ObjectSelected
            foreach (TaskSelected objSel in os)
            {
                if (objSel != null && objSel.IsSelected == true)
                {
                    TaskTake taskTake = instance.TakeGrid[i].GetComponent<TaskTake>();

                    if (taskTake.task != null)
                    {
                        Name.text = taskTake.task.Name;
                        SkillDescription.text = taskTake.task.SkillDescription;

                        TaskName.text = taskTake.task.Name;
                        TaskDescription.text = taskTake.task.Description;
                    }
                    else // 如果选中的物品为空
                    {
                        Name.text = "";
                        SkillDescription.text = "";
                    }

                    // 一旦找到并处理了选中的对象，可以跳出循环避免重复处理
                    break;
                }
            }
        }

    }

    public void AddNewMission(Task newTask)
    {
        misson.TaskList.Add(newTask);
        TaskName.text = newTask.Name;
        TaskDescription.text = newTask.Description;

        // 检查任务界面是否打开
        if (instance.MissonUI.activeSelf)
        {
            // 如果任务界面是打开的，立即更新任务选中状态
            UpdateTaskSelection(newTask);
            IsUpdated = true;
        }
        else
        {
            // 如果任务界面是关闭的，设置一个标志，稍后处理
            IsUpdated = false; // 设置标志
        }
    }

    private void UpdateTaskSelection(Task selectedTask)
    {
        // 遍历所有任务UI，重置所有TaskSelected的IsSelected为false
        foreach (GameObject grid in TakeGrid)
        {
            TaskSelected[] os = grid.GetComponentsInChildren<TaskSelected>();
            foreach (TaskSelected objSel in os)
            {
                objSel.IsSelected = false;
            }

            // 获取当前Grid关联的任务
            TaskTake taskTake = grid.GetComponent<TaskTake>();
            if (taskTake != null && taskTake.task == selectedTask)
            {
                // 找到新添加的任务对应的TaskSelected，将其设置为选中
                TaskSelected[] selectedOs = taskTake.gameObject.GetComponentsInChildren<TaskSelected>();
                foreach (TaskSelected sel in selectedOs)
                {
                    sel.IsSelected = true;
                }

                // 更新显示的信息，因为可能已经有一个任务被选中了
                UpdateTheObjectInformation();
                break; // 找到并处理后退出循环
            }
        }
    }


}
