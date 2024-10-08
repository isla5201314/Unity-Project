using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSelected : ObjectSelected
{
     public override void SelectButton()
    {
        if (IsSelected)
            return; // ����Ѿ���ѡ��״̬����ִ���κβ����������ظ�����

        // �������о���ObjectSelected�ű�����Ϸ���󣬽����ǵ�IsSelected��Ϊfalse
        TaskSelected[] allSelectors = FindObjectsOfType<TaskSelected>();
        foreach (TaskSelected selector in allSelectors)
        {
            if (selector != this)
            {
                selector.IsSelected = false;
                selector.OnSelectionChanged.Invoke();
            }
        }

        // ���õ�ǰ��ťΪѡ��״̬
        IsSelected = true;
        OnSelectionChanged.Invoke();
    }


}
