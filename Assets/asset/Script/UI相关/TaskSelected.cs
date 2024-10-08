using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSelected : ObjectSelected
{
     public override void SelectButton()
    {
        if (IsSelected)
            return; // 如果已经是选中状态，不执行任何操作，避免重复触发

        // 遍历所有具有ObjectSelected脚本的游戏对象，将它们的IsSelected设为false
        TaskSelected[] allSelectors = FindObjectsOfType<TaskSelected>();
        foreach (TaskSelected selector in allSelectors)
        {
            if (selector != this)
            {
                selector.IsSelected = false;
                selector.OnSelectionChanged.Invoke();
            }
        }

        // 设置当前按钮为选中状态
        IsSelected = true;
        OnSelectionChanged.Invoke();
    }


}
