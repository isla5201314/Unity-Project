using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissonManager : MonoBehaviour
{
    [Header("����ϵͳ����")]
    static public MissonManager instance;
    public Misson misson;

    [Header("�����б�")]
    public GameObject MissonUI;
    public GameObject[] TakeGrid;

    [Header("��ѡ�е�������ʾ������")]
    public TaskTake task;
    public TMP_Text Name;//��������
    public TMP_Text SkillDescription;//������ϸ����

    [Header("UI��ʾ��������Ϣ")]
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
                    // ������������Ѵ򿪣����ڿ��԰�ȫ��ִ�и���ѡ������Ĳ���
                    Task taskToSelect = misson.TaskList[misson.TaskList.Count - 1]; // �������������б��е����һ��
                    UpdateTaskSelection(taskToSelect);
                }
            }
        }


    }

    void Update()
    {
        WaitToLoad();
    }

    public void CreateNewTask()//ˢ�������б���ʾ������
    {
        int i = 0;
        foreach (Task task in instance.misson.TaskList)// ����������б�����������Ŀ�λ,��ʵʱ����
        {

            while (i < instance.TakeGrid.Length) //�������п�λ�����ÿ�λ��Ϊ��Ӧ������Ϣ
            {
                TaskTake taskTake = instance.TakeGrid[i].GetComponent<TaskTake>();//��ȡ��Ӧ������Ϣ
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

    public void UpdateTheObjectInformation() //��������ѡ�е�������Ϣ
    {

        for (int i = 0; i < instance.TakeGrid.Length; i++)
        {
            TaskSelected[] os = instance.TakeGrid[i].GetComponentsInChildren<TaskSelected>(); // ��ȡ��Ӧ������Ϣ

            // ����os���鼴���и��ӣ�����IsSelectedΪtrue��ObjectSelected
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
                    else // ���ѡ�е���ƷΪ��
                    {
                        Name.text = "";
                        SkillDescription.text = "";
                    }

                    // һ���ҵ���������ѡ�еĶ��󣬿�������ѭ�������ظ�����
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

        // �����������Ƿ��
        if (instance.MissonUI.activeSelf)
        {
            // �����������Ǵ򿪵ģ�������������ѡ��״̬
            UpdateTaskSelection(newTask);
            IsUpdated = true;
        }
        else
        {
            // �����������ǹرյģ�����һ����־���Ժ���
            IsUpdated = false; // ���ñ�־
        }
    }

    private void UpdateTaskSelection(Task selectedTask)
    {
        // ������������UI����������TaskSelected��IsSelectedΪfalse
        foreach (GameObject grid in TakeGrid)
        {
            TaskSelected[] os = grid.GetComponentsInChildren<TaskSelected>();
            foreach (TaskSelected objSel in os)
            {
                objSel.IsSelected = false;
            }

            // ��ȡ��ǰGrid����������
            TaskTake taskTake = grid.GetComponent<TaskTake>();
            if (taskTake != null && taskTake.task == selectedTask)
            {
                // �ҵ�����ӵ������Ӧ��TaskSelected����������Ϊѡ��
                TaskSelected[] selectedOs = taskTake.gameObject.GetComponentsInChildren<TaskSelected>();
                foreach (TaskSelected sel in selectedOs)
                {
                    sel.IsSelected = true;
                }

                // ������ʾ����Ϣ����Ϊ�����Ѿ���һ������ѡ����
                UpdateTheObjectInformation();
                break; // �ҵ���������˳�ѭ��
            }
        }
    }


}
